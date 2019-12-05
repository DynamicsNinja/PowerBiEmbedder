using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Fic.XTB.PowerBiEmbedder.Helper;
using Fic.XTB.PowerBiEmbedder.Model;
using Fic.XTB.PowerBiEmbedder.Proxy;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using Control = Fic.XTB.PowerBiEmbedder.Model.Control;

namespace Fic.XTB.PowerBiEmbedder
{
    public partial class PowerBiEmbedder : PluginControlBase, IGitHubPlugin {
        private Settings mySettings;
        public string RepositoryName => "PowerBiEmbedder";
        public string UserName => "DynamicsNinja";

        private List<EntityMetadataProxy> _entities;
        private string _fetchXml;
        private FormModel _formModel;
        private bool _pbiEnabled;
        private Entity _systemSettings;

        public PowerBiEmbedder() {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e) {
            // Loads or creates the settings for the plugin
            if(!SettingsManager.Instance.TryLoad(GetType(), out mySettings)) {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            } else {
                LogInfo("Settings found and loaded");
            }

            this.ActiveControl = cmbEntity;
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e) {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter) {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if(mySettings != null && detail != null) {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void PowerBiEmbedder_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e) {
            LogInfo("Connection has changed to: {0}", e.ConnectionDetail.WebApplicationUrl);

            UnlockControls();

            tbGrpId.Text = "00000000-0000-0000-0000-000000000000";
            tbReportId.Text = "00000000-0000-0000-0000-000000000000";
            tbPbiUrl.Text = "https://app.powerbi.com";
            tbRowspan.Text = "20";
            cmbEntity.Text = "Select an entity";      

            cmbEntityField.Enabled = false;

            _entities = new List<EntityMetadataProxy>();

            LoadEntities();
            CheckIfPbiSettingsEnabled();
        }

        private void LoadEntities() {
            gbTarget.Enabled = false;
            _entities = new List<EntityMetadataProxy>();
            WorkAsync(new WorkAsyncInfo("Loading entities...",
                (eventargs) => {
                    eventargs.Result = MetadataHelper.LoadEntities(Service);
                }) {
                PostWorkCallBack = (completedargs) => {
                    if(completedargs.Error != null) {
                        MessageBox.Show(completedargs.Error.Message);
                    } else {
                        if(completedargs.Result is RetrieveMetadataChangesResponse) {
                            var metaresponse = ((RetrieveMetadataChangesResponse)completedargs.Result).EntityMetadata;
                            _entities.AddRange(metaresponse
                                .Where(e => e.IsCustomizable.Value == true && e.IsIntersect.Value != true)
                                .Select(m => new EntityMetadataProxy(m))
                                .OrderBy(e => e.ToString()));
                            cmbEntity.Items.AddRange(_entities.ToArray());
                            cmbEntityField.Enabled = true;
                        }
                        gbTarget.Enabled = true;
                    }
                }
            });
        }

        private void LoadEntityFields(string entityName) {
            cmbEntityField.Items.Clear();
            WorkAsync(new WorkAsyncInfo("Loading entity fields...",
                (eventargs) => {
                    eventargs.Result = MetadataHelper.LoadEntityDetails(Service, entityName).EntityMetadata.FirstOrDefault();
                }) {
                PostWorkCallBack = (completedargs) => {
                    if(completedargs.Error != null) {
                        MessageBox.Show(completedargs.Error.Message);
                    } else {
                        if(completedargs.Result is EntityMetadata) {
                            var metaresponsea = (EntityMetadata)completedargs.Result;
                            var attributes = metaresponsea.Attributes
                                                          .Select(a => new AttributeProxy((AttributeMetadata)a)).OrderBy(a => a.LogicalName).ToList();
                            cmbEntityField.Items.AddRange(attributes.ToArray());
                        }
                    }
                }
            });
        }

        private void LoadForms(string entityName) {
            cmbForm.Items.Clear();
            cmbTab.Items.Clear();
            cmbSection.Items.Clear();

            cmbEntity.Enabled = false;
            WorkAsync(new WorkAsyncInfo("Loading forms...",
                (eventargs) => {
                    var qx = new QueryExpression("systemform");
                    qx.ColumnSet = new ColumnSet(true);
                    qx.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, entityName);
                    qx.Criteria.AddCondition("type", ConditionOperator.Equal, 2);
                    eventargs.Result = Service.RetrieveMultiple(qx);
                }) {
                PostWorkCallBack = (completedargs) => {
                    if(completedargs.Error != null) {
                        MessageBox.Show(completedargs.Error.Message);
                    } else {
                        if(completedargs.Result is EntityCollection) {
                            var result = (EntityCollection)completedargs.Result;
                            var forms = result.Entities.Select(f => new FormProxy(f)).OrderBy(f => f.ToString());
                            cmbForm.Items.AddRange(forms.ToArray());
                            cmbEntity.Enabled = true;
                        }
                    }
                }
            });
        }

        private void LoadTabs(string formXml) {
            cmbTab.Items.Clear();
            cmbSection.Items.Clear();

            cmbTab.Enabled = false;
            var serializer = new XmlSerializer(typeof(FormModel));
            using(var reader = new StringReader(formXml)) {
                _formModel = (FormModel)serializer.Deserialize(reader);
            }

            foreach(var tab in _formModel.Tabs) {
                var tabProxy = new TabProxy {
                    Text = tab.Labels.FirstOrDefault()?.Description,
                    Value = tab.Id
                };
                cmbTab.Items.Add(tabProxy);
            }
            cmbTab.Enabled = true;
        }

        private void LoadSections(string tabId) {
            cmbSection.Items.Clear();

            var tab = _formModel.Tabs.FirstOrDefault(t => t.Id == tabId);

            foreach(var column in tab.Columns) {
                foreach(var section in column.Sections) {
                    cmbSection.Items.Add(new SectionProxy {
                        Id = section.Id,
                        Text = section.Labels.FirstOrDefault()?.Description == "" ? section.Name : $"{section.Labels.FirstOrDefault()?.Description} ({section.Name})",
                        Section = section
                    });
                }
            }
        }

        private void cmbEntity_SelectedIndexChanged(object sender, EventArgs e) {
            var entityselected = (EntityMetadataProxy)cmbEntity.SelectedItem;
            var entityName = entityselected.Metadata.LogicalName;
            LoadForms(entityName);
            LoadEntityFields(entityName);
        }

        private void cmbForm_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedForm = ((FormProxy)cmbForm.SelectedItem).Entity;
            _fetchXml = (string)selectedForm["formxml"];
            LoadTabs(_fetchXml);
        }

        private void cmbTab_SelectedIndexChanged(object sender, EventArgs e) {
            var tab = (TabProxy)cmbTab.SelectedItem;
            var tabId = (string)tab.Value;

            LoadSections(tabId);
        }

        private string GeneratePowerBiSectionXml(FormTabColumnSection section) {
            var powerBiGroupId = tbGrpId.Text == "" ? "00000000-0000-0000-0000-000000000000" : tbGrpId.Text;
            var powerBiReportId = tbReportId.Text;
            var reportUrl = tbPbiUrl.Text == "" ? "https://app.powerbi.com" : tbPbiUrl.Text;

            var filter = "";
            if(cbxPbiFilter.Checked) {
                var pbiTableName = tbPbiTable.Text;
                var pbiColumnName = tbPbiColumn.Text;
                var cdsFieldName = ((AttributeProxy)cmbEntityField.SelectedItem).LogicalName;
                filter = $"<PowerBIFilter>{{\"Filter\": \"[{{\\\"$schema\\\":\\\"basic\\\",\\\"target\\\":{{\\\"table\\\":\\\"{pbiTableName}\\\",\\\"column\\\":\\\"{pbiColumnName}\\\"}},\\\"operator\\\":\\\"In\\\",\\\"values\\\":[$a],\\\"filterType\\\":1}}]\", \"Alias\": {{\"$a\": \"{cdsFieldName}\"}}}}</PowerBIFilter>";
            }

            var rowspan = tbRowspan.Text != "" ? tbRowspan.Text : "1";

            var sectionLabel = tbSectionName.Text;
            var sectionName = section.Name ?? sectionLabel.Replace(" ", "_");

            var xml =
                $"<section id=\"{section.Id}\" locklevel=\"0\" showlabel=\"{section.ShowLabel.ToString().ToLower()}\" IsUserDefined=\"0\" name=\"{sectionName}\" labelwidth=\"115\" columns=\"1\" layout=\"varwidth\" showbar=\"false\">" +
                    "<labels>" +
                        $"<label description=\"{sectionLabel}\" languagecode=\"1033\" />" +
                    "</labels>" +
                    "<rows>" +
                        "<row>" +
                            $"<cell id=\"{Guid.NewGuid():B}\" showlabel=\"true\" rowspan=\"{rowspan}\" colspan=\"1\" auto=\"false\">" +
                                "<labels>" +
                                    "<label description=\"Power BI Report\" languagecode=\"1033\" />" +
                                "</labels>" +
                                "<control id=\"filteredreport\" classid=\"{8C54228C-1B25-4909-A12A-F2B968BB0D62}\">" +
                                    "<parameters>" +
                                        $"<PowerBIGroupId>{powerBiGroupId}</PowerBIGroupId>" +
                                        $"<PowerBIReportId>{powerBiReportId}</PowerBIReportId>" +
                                        $"<TileUrl>{reportUrl}/reportEmbed?reportId={powerBiReportId}</TileUrl>" +
                                        $"{filter}"+
                                    "</parameters>" +
                                "</control>" +
                            "</cell>" +
                        "</row>" +
                    "</rows>" +
                "</section>";

            return xml;
        }

        private void tbReportId_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            var textbox = (TextBox)sender;
            var guid = new Guid();

            var isValid = Guid.TryParse(textbox.Text, out guid);

            if(!isValid) {
                errorProvider.SetError(textbox, "Input is not a valid GUID format.");
                e.Cancel = true;
            }else if (guid == Guid.Empty){
                errorProvider.SetError(textbox, "Input must not be an enmpty GUID.");
                e.Cancel = true;
            } else {
                errorProvider.SetError(textbox, "");
            }
        }

        private void tbGrpId_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            var textbox = (TextBox)sender;
            var guid = new Guid();

            var isValid = Guid.TryParse(textbox.Text, out guid);
            var isEmpty = textbox.Text == "";

            if(!isValid && !isEmpty) {
                errorProvider.SetError(textbox, "Input is not a valid GUID format.");
                e.Cancel = true;
            } else {
                errorProvider.SetError(textbox, "");
            }
        }

        private void cbxPbiFilter_CheckedChanged(object sender, EventArgs e) {
            var checkbox = (CheckBox)sender;
            if(checkbox.Checked) {
                gbPbiFilters.Enabled = true;
            } else {
                gbPbiFilters.Enabled = false;
                errorProvider.SetError(tbPbiTable, "");
                errorProvider.SetError(tbPbiColumn, "");
                errorProvider.SetError(cmbEntityField, "");
            }
        }

        private void tbPbiTable_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            var filterActive = cbxPbiFilter.Checked;

            if(filterActive && tbPbiTable.Text == "") {
                errorProvider.SetError(tbPbiTable, "Input must not be empty.");
                e.Cancel = true;
            } else {
                errorProvider.SetError(tbPbiTable, "");
            }
        }

        private void tbPbiColumn_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            var filterActive = cbxPbiFilter.Checked;

            if(filterActive && tbPbiColumn.Text == "") {
                errorProvider.SetError(tbPbiColumn, "Input must not be empty.");
                e.Cancel = true;
            } else {
                errorProvider.SetError(tbPbiColumn, "");
            }
        }

        private void btnPublish_Click(object sender, EventArgs e) {
            var isValidForm = ValidateChildren();
            if (!isValidForm) { return; }

            var selectedSectionProxy = (SectionProxy)cmbSection.SelectedItem;

            var selectedSection = selectedSectionProxy.Section;

            var sectionXml = GeneratePowerBiSectionXml(selectedSection);

            var regexString = $@"<section[^>]+{selectedSection.Id}.*?<\/section>";
            Regex regex = new Regex(regexString);
            Match match = regex.Match(_fetchXml);
            var selectedSectionXml = match.Value;

            _fetchXml = _fetchXml.Replace(selectedSectionXml, sectionXml);

            var oldformEntity = ((FormProxy)cmbForm.SelectedItem).Entity;

            var formEntity = new Entity(oldformEntity.LogicalName, oldformEntity.Id);
            formEntity["formxml"] = _fetchXml;

            WorkAsync(new WorkAsyncInfo("Publishing report on the form...",
                (eventargs) => {
                    Service.Update(formEntity);
                    PublishAllXmlRequest publishallxmlrequest = new PublishAllXmlRequest();
                    Service.Execute(publishallxmlrequest);
                }) {
                PostWorkCallBack = (completedargs) => {
                    if(completedargs.Error != null) {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                }
            });
        }

        private void tbRowspan_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e) {
            CloseTool();
        }

        private void CheckIfPbiSettingsEnabled() {
            var qe = new QueryExpression("organization");
            qe.ColumnSet = new ColumnSet("powerbifeatureenabled");
            _systemSettings = Service.RetrieveMultiple(qe).Entities.FirstOrDefault();
            _pbiEnabled = (bool)_systemSettings["powerbifeatureenabled"];

            if(_pbiEnabled) {
                cmbPbiSettings.SelectedItem = "On";
            } else {
                cmbPbiSettings.SelectedItem = "Off";
            }
        }

        private void cmbPbiSettings_Change(object sender, EventArgs e) {
            var cmbPbiSettingsValue = cmbPbiSettings.SelectedItem;

            var updatedSettings = new Entity(_systemSettings.LogicalName, _systemSettings.Id);
            switch (cmbPbiSettingsValue) {
                case "On":
                    if (_pbiEnabled) { return;}
                    updatedSettings["powerbifeatureenabled"] = true;
                    Service.Update(updatedSettings);
                    _pbiEnabled = true;
                    break;
                case "Off":
                    if (!_pbiEnabled) { return;}
                    updatedSettings["powerbifeatureenabled"] = false;
                    Service.Update(updatedSettings);
                    _pbiEnabled = false;
                    break;
            }
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSectionProxy = (SectionProxy)cmbSection.SelectedItem;
            var selectedSection = selectedSectionProxy.Section;

            var sectionName = selectedSection.Labels.FirstOrDefault()?.Description;
            var rowSpan = selectedSection.Rows.FirstOrDefault()?.Cells.FirstOrDefault()?.RowSpan ?? "1";

            tbSectionName.Text = sectionName;
            tbRowspan.Text = rowSpan;

            Control powerBiControl = null;
            foreach(var row in selectedSection.Rows) {
                foreach(var cell in row.Cells) {
                    if(cell.Control == null) { continue;}
                    if(cell.Control.ClassId.ToUpper() == "{8C54228C-1B25-4909-A12A-F2B968BB0D62}") {
                        powerBiControl = cell.Control;
                    }
                }
            }

            if(powerBiControl != null) {
                tbGrpId.Text = powerBiControl.Parameters.PowerBIGroupId.ToUpper();
                tbReportId.Text = powerBiControl.Parameters.PowerBIReportId.ToUpper();
                tbPbiUrl.Text = powerBiControl.Parameters.TileUrl.Split(new []{ "/reportEmbed" },StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                cbxPbiFilter.Checked = powerBiControl.Parameters.PowerBIFilter != null;
            } else {
                tbGrpId.Text = "00000000-0000-0000-0000-000000000000";
                tbReportId.Text = "00000000-0000-0000-0000-000000000000";
                tbPbiUrl.Text = "https://app.powerbi.com";
                cbxPbiFilter.Checked = false;
            }
        }

        private void UnlockControls() {
            gbTarget.Enabled = true;
            gbFormatting.Enabled = true;
            gbPowerBiConfig.Enabled = true;

            cmbPbiSettings.Enabled = true;

            btnPublish.Enabled = true;
        }

        private void cmbEntity_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(cmbEntity.SelectedItem == null) {
                errorProvider.SetError(cmbEntity, "You must select entity.");
                e.Cancel = true;
            } else {
                errorProvider.SetError(cmbEntity, "");
            }
        }

        private void cmbForm_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbForm.SelectedItem == null){
                errorProvider.SetError(cmbForm, "You must select form.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(cmbForm, "");
            }
        }

        private void cmbTab_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbTab.SelectedItem == null){
                errorProvider.SetError(cmbTab, "You must select tab.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(cmbTab, "");
            }
        }

        private void cmbSection_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbSection.SelectedItem == null){
                errorProvider.SetError(cmbSection, "You must select section.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(cmbSection, "");
            }
        }

        private void tbSectionName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tbSectionName.Text == ""){
                errorProvider.SetError(tbSectionName, "Field must contain value.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(tbSectionName, "");
            }
        }

        private void tbRowspan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tbRowspan.Text == ""){
                errorProvider.SetError(tbRowspan, "Field must contain value.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(tbRowspan, "");
            }
        }

        private void cmbEntityField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var filterActive = cbxPbiFilter.Checked;

            if (filterActive && cmbEntityField.SelectedItem == null){
                errorProvider.SetError(cmbEntityField, "Please select entity field.");
                e.Cancel = true;
            }else{
                errorProvider.SetError(cmbEntityField, "");
            }
        }
    }
}