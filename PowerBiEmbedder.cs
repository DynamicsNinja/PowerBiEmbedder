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
using CheckBox = System.Windows.Forms.CheckBox;
using ComboBox = System.Windows.Forms.ComboBox;
using Control = Fic.XTB.PowerBiEmbedder.Model.Control;
using RadioButton = System.Windows.Forms.RadioButton;
using TextBox = System.Windows.Forms.TextBox;

namespace Fic.XTB.PowerBiEmbedder
{
    public partial class PowerBiEmbedder : PluginControlBase, IGitHubPlugin {
        public Settings Settings;
        public string CurrentOrg;
        public string RepositoryName => "PowerBiEmbedder";
        public string UserName => "DynamicsNinja";

        private List<EntityMetadataProxy> _entities;
        private string _fetchXml;
        private FormModel _formModel;
        private bool _pbiEnabled;
        private Entity _systemSettings;

        public List<PbiGroup> PbiGroups;
        public RadioButton RbApiButton;
        public ComboBox CmbGroups;
        public ComboBox CmbReports;
        public ComboBox CmbPages;

        public TextBox TbGroup;
        public TextBox TbReport;
        public TextBox TbPage;

        private readonly AzureLoginDialog _azureLogin;

        public PowerBiEmbedder() {
            InitializeComponent();

            _azureLogin = new AzureLoginDialog(this);
        }

        private void MyPluginControl_Load(object sender, EventArgs e) {
            // Loads or creates the settings for the plugin
            if(!SettingsManager.Instance.TryLoad(GetType(), out Settings)) {
                Settings = new Settings { CurrentOrg = CurrentOrg };
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
            SettingsManager.Instance.Save(GetType(), Settings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter) {
            base.UpdateConnection(newService, detail, actionName, parameter);

            CurrentOrg = detail?.Organization;
            if(Settings != null && detail != null) {
                Settings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void PowerBiEmbedder_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e) {
            LogInfo("Connection has changed to: {0}", e.ConnectionDetail.WebApplicationUrl);

            UnlockControls();

            RbApiButton = rbApi;
            CmbGroups = cbGroup;
            CmbReports = cbReport;
            CmbPages = cbPage;

            TbReport = tbReportId;
            TbGroup = tbGrpId;
            TbPage = tbPbiPage;

            tbGrpId.Text = "00000000-0000-0000-0000-000000000000";
            tbReportId.Text = "00000000-0000-0000-0000-000000000000";
            tbPbiUrl.Text = "https://app.powerbi.com";
            tbRowspan.Text = "20";
            cmbEntity.Text = "Select an entity";
            lblSection.Text = "Section 🔗";

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
            var reportPageFilter  = tbPbiPage.Text != "" ? $"&amp;pageName={tbPbiPage.Text}" : "";

            var filterString = "";
            if(cbxPbiFilter.Checked) {
                var pbiTableName = tbPbiTable.Text;
                var pbiColumnName = tbPbiColumn.Text;
                var cdsFieldName = ((AttributeProxy)cmbEntityField.SelectedItem).LogicalName;

                var filter = new PbiFilter(pbiTableName, pbiColumnName, cdsFieldName);
                filterString = $"<PowerBIFilter>{filter.ToJsonString()}</PowerBIFilter>";
            };

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
                                        $"<TileUrl>{reportUrl}/reportEmbed?reportId={powerBiReportId}{reportPageFilter}</TileUrl>" +
                                        $"{filterString}"+
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
            _pbiEnabled = (bool)_systemSettings?["powerbifeatureenabled"];

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
            tbSectionName.Text = sectionName;

            if (!cbxLinkValues.Checked) { return; }

            var rowSpan = selectedSection.Rows.FirstOrDefault()?.Cells.FirstOrDefault()?.RowSpan ?? "1";
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
                tbPbiPage.Text = powerBiControl.Parameters.TileUrl.Split(new[] { "&pageName=" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

                InitializeDropdowns(tbGrpId.Text, tbReportId.Text, tbPbiPage.Text);

                tbPbiUrl.Text = powerBiControl.Parameters.TileUrl.Split(new []{ "/reportEmbed" },StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                cbxPbiFilter.Checked = powerBiControl.Parameters.PowerBIFilter != null;

                if(powerBiControl.Parameters.PowerBIFilter != null) {
                    var filter = new PbiFilter(powerBiControl.Parameters.PowerBIFilter);

                    tbPbiTable.Text = filter.Filter.Target.Table;
                    tbPbiColumn.Text = filter.Filter.Target.Column;

                    foreach (AttributeProxy ap in cmbEntityField.Items){
                        if(ap.LogicalName != filter.Alias.A) continue;
                        cmbEntityField.SelectedItem = ap;
                        break;
                    }
                } else {
                    tbPbiTable.Text = "";
                    tbPbiColumn.Text = "";
                    cmbEntityField.SelectedIndex = -1;

                }
            } else {
                tbGrpId.Text = "00000000-0000-0000-0000-000000000000";
                cbGroup.SelectedItem = null;

                tbReportId.Text = "00000000-0000-0000-0000-000000000000";
                cbReport.SelectedItem = null;

                tbPbiPage.Text = "";
                cbPage.SelectedItem = null;
        
                tbPbiUrl.Text = "https://app.powerbi.com";                
                cbxPbiFilter.Checked = false;

                tbPbiTable.Text = "";
                tbPbiColumn.Text = "";
                cmbEntityField.SelectedIndex = -1;
            }

            btnConnect.Enabled = true;
            gbFormatting.Enabled = true;
            gbPowerBiConfig.Enabled = true;
            cbxLinkValues.Enabled = true;
        }

        private void UnlockControls() {
            gbTarget.Enabled = true;
            //gbFormatting.Enabled = true;
            //gbPowerBiConfig.Enabled = true;

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

        private void btnConnect_Click(object sender, EventArgs e) {
            _azureLogin.ShowDialog();
        }

        private void method_CheckedChanged(object sender, EventArgs e)
        {
            if(rbApi.Checked) {
                tbGrpId.Visible = false;
                tbReportId.Visible = false;
                tbPbiUrl.Enabled = false;

                cbGroup.Visible = true;
                cbReport.Visible = true;
                cbPage.Visible = true;
            }

            if(rbManual.Checked) {
                tbGrpId.Visible = true;
                tbReportId.Visible = true;
                tbPbiUrl.Enabled = true;

                cbGroup.Visible = false;
                cbReport.Visible = false;
                cbPage.Visible = false;

            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e) {
            if(cbGroup.SelectedItem == null) { return;}
            var selectedGroup = ((GroupProxy)cbGroup.SelectedItem).Value;
            tbGrpId.Text = selectedGroup.Id;
            
            cbPage.Items.Clear();
            tbPbiPage.Text = "";

            cbReport.Items.Clear();
            tbReportId.Text = "";

            foreach (var report in selectedGroup.Reports) {
                var reportProxy = new ReportProxy
                {
                    Text = report.Name,
                    Value = report
                };
                cbReport.Items.Add(reportProxy);
            }

        }

        private void cbReport_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbReport.SelectedItem == null) { return; }
            var selectedReport = ((ReportProxy)cbReport.SelectedItem).Value;

            tbPbiUrl.Text = selectedReport.EmbedUrl.Split(new[] { "/reportEmbed" }, StringSplitOptions.None).FirstOrDefault();
            tbReportId.Text = selectedReport.Id;

            tbPbiPage.Text = "";
            cbPage.Items.Clear();

            foreach (var page in selectedReport.Pages){
                var pageProxy = new PageProxy
                {
                    Text = page.DisplayName,
                    Value = page
                };
                cbPage.Items.Add(pageProxy);
            }
        }

        private void cbGroup_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbGroup.SelectedItem == null && rbApi.Checked)
            {
                errorProvider.SetError(cbGroup, "You must select group.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbGroup, "");
            }
        }

        private void cbReport_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbReport.SelectedItem == null && cbGroup.SelectedItem != null && rbApi.Checked)
            {
                errorProvider.SetError(cbReport, "You must select group.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbReport, "");
            }
        }

        public void InitializeDropdowns(string groupId, string reportId, string pageName) {
            if(cbGroup.Items.Count == 0) { return;}
            foreach (GroupProxy group in cbGroup.Items)
            {
                if (@group.Value.Id.ToUpper() != groupId.ToUpper()) continue;
                cbGroup.SelectedItem = group;

                cbReport.Items.Clear();
                foreach (var report in group.Value.Reports)
                {
                    var reportProxy = new ReportProxy
                    {
                        Text = report.Name,
                        Value = report
                    };
                    cbReport.Items.Add(reportProxy);
                }
                break;
            }

            foreach (ReportProxy report in cbReport.Items)
            {
                if (report.Value.Id.ToUpper() != reportId.ToUpper()) continue;
                cbReport.SelectedItem = report;           

                cbPage.Items.Clear();
                foreach (var page in report.Value.Pages)
                {
                    var pageProxy = new PageProxy
                    {
                        Text = page.DisplayName,
                        Value = page
                    };
                    cbPage.Items.Add(pageProxy);
                }
                break;
            }

            foreach(PageProxy page in cbPage.Items) {
                if (page.Value.Name != pageName) continue;

                cbPage.SelectedItem = page;
                break;
            }
        }

        private void cbxLockConfig_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxLinkValues.Checked) {
                gbPbiFilters.Text = @"Filter 🔗";
                gbPowerBiConfig.Text = @"Power BI Config 🔗";
                lblSection.Text = @"Section 🔗";
            } else {
                gbPbiFilters.Text = @"Filter";
                gbPowerBiConfig.Text = @"Power BI Config";
                lblSection.Text = @"Section";
            }
        }

        private void cbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPage.SelectedItem == null) { return; }
            var selectedPage = ((PageProxy)cbPage.SelectedItem).Value;

            tbPbiPage.Text = selectedPage.Name;

        }
    }
}