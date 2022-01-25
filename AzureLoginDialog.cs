using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Fic.XTB.PowerBiEmbedder.Helper;
using Fic.XTB.PowerBiEmbedder.Model;
using Fic.XTB.PowerBiEmbedder.Proxy;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using XrmToolBox.Extensibility;

namespace Fic.XTB.PowerBiEmbedder
{
    public partial class AzureLoginDialog : System.Windows.Forms.Form
    {
        private readonly PowerBiEmbedder _pbe;

        public AzureLoginDialog(PowerBiEmbedder pbe)
        {
            InitializeComponent();

            _pbe = pbe;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DisableButtons();

            var appId = tbAppId.Text;
            var tenantId = tbTenantId.Text;
            var redirectUrl = tbRedirectUri.Text;

            var orgSettings = new OrgSettings
            {
                Organization = _pbe.CurrentOrg,
                ClientId = appId,
                TenantId = tenantId,
                RedirectUrl = redirectUrl
            };

            var existingSettings = _pbe.Settings.OrgSettings.FirstOrDefault(o => o.Organization.ToLower() == _pbe.CurrentOrg.ToLower());
            if (existingSettings != null) { _pbe.Settings.OrgSettings.Remove(existingSettings); }

            _pbe.Settings.OrgSettings.Add(orgSettings);

            SettingsManager.Instance.Save(GetType(), _pbe.Settings);

            try
            {
                var pbiClient = new PbiHelper(appId, tenantId, redirectUrl);

                this.Close();
                _pbe.WorkAsync(new WorkAsyncInfo("Loading Power BI groups and reports...",
                    (eventargs) =>
                    {
                        eventargs.Result = pbiClient.GetGroups();
                    })
                {
                    PostWorkCallBack = (completedargs) =>
                    {
                        if (completedargs.Error != null)
                        {
                            MessageBox.Show(completedargs.Error.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            EnableButtons();
                        }
                        else
                        {
                            if (!(completedargs.Result is List<PbiGroup> groups)) return;

                            _pbe.PbiGroups = groups;

                            PopulateGroupsDropdown();
                            _pbe.InitializeDropdowns(_pbe.TbGroup.Text, _pbe.TbReport.Text, _pbe.TbPage.Text);

                            _pbe.RbApiButton.Enabled = true;

                            EnableButtons();
                        }
                    }
                });
            }
            catch (AdalServiceException ex)
            {
                EnableButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                EnableButtons();
            }
        }

        private void EnableButtons()
        {
            btnCancel.Enabled = true;
            btnConnect.Enabled = true;
        }

        private void DisableButtons()
        {
            btnCancel.Enabled = false;
            btnConnect.Enabled = false;
        }

        private void PopulateGroupsDropdown()
        {
            _pbe.CmbGroups.Items.Clear();
            _pbe.CmbReports.Items.Clear();   
            _pbe.CmbPages.Items.Clear();

            foreach (var group in _pbe.PbiGroups)
            {
                var groupProxy = new GroupProxy
                {
                    Text = group.Name,
                    Value = group
                };
                _pbe.CmbGroups.Items.Add(groupProxy);
            }
        }

        private void AzureLoginDialog_Load(object sender, EventArgs e) {
            var existingSettings = _pbe.Settings.OrgSettings.FirstOrDefault(o => o.Organization.ToLower() == _pbe.CurrentOrg.ToLower());

            if(existingSettings == null) { return;}

            if (existingSettings.ClientId != null)
            {
                tbAppId.Text = existingSettings.ClientId;
            }

            if (existingSettings.TenantId != null)
            {
                tbTenantId.Text = existingSettings.TenantId;
            }

            if (existingSettings.RedirectUrl != null)
            {
                tbRedirectUri.Text = existingSettings.RedirectUrl;
            }
        }
    }
}
