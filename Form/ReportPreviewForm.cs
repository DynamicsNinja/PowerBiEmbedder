using System.Collections.Generic;
using Fic.XTB.PowerBiEmbedder.Model;
using Microsoft.Xrm.Sdk;

namespace Fic.XTB.PowerBiEmbedder.Form
{
    public partial class ReportPreviewForm : System.Windows.Forms.Form
    {
        private readonly PowerBiEmbedder _pbe;
        private readonly List<Entity> _records;
        public ReportPreviewForm(PowerBiEmbedder pbe, List<Entity> records)
        {
            _pbe = pbe;
            _records = records;
            InitializeComponent();

            lblRecord.Text = _pbe.EntityDisplayNamePlural;
            lblRecord.Visible = records != null;
            cbRecords.Visible = records != null;
        }

        private async void ReportPreviewForm_Load(object sender, System.EventArgs e)
        {
            if (wvPreview != null && wvPreview.CoreWebView2 != null) { return; }

            await wvPreview.EnsureCoreWebView2Async();

            if (_records == null || _records.Count == 0)
            {
                var url = GenerateEmbedUrl();

                wvPreview.CoreWebView2.Navigate(url);
            }
            else
            {
                foreach (var record in _records)
                {
                    cbRecords.Items.Add(new RecordProxy
                    {
                        DisplayName = (string)record[_pbe.EntityPrimaryField],
                        Field = record.Contains(_pbe.EntitySelectedField) ? (string)record[_pbe.EntitySelectedField] : string.Empty
                    });
                }

                cbRecords.SelectedIndex = 0;
            }
        }

        private string GenerateEmbedUrl()
        {
            var reportId = _pbe.TbReport.Text;
            var pbiTable = _pbe.TbPbiTable.Text;
            var pbiColumn = _pbe.TbPbiColumn.Text;

            var pageName = _pbe.TbPage.Text;

            var selectedRecord = (RecordProxy)cbRecords.SelectedItem;

            var baseUrl = "https://app.powerbi.com";
            var url = $"{baseUrl}/reportEmbed?reportId={reportId}&filterPaneEnabled=false&autoAuth=true";

            if (!string.IsNullOrWhiteSpace(pbiTable) && !string.IsNullOrWhiteSpace(pbiColumn) && _pbe.CbxPbiFilter.Checked)
            {
                url += $"&$filter={pbiTable}/{pbiColumn} eq '{selectedRecord.Field}'";
            }

            if (!string.IsNullOrWhiteSpace(pageName))
            {
                url += $"&pageName={pageName}";
            }

            return url;
        }

        private void cbRecords_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var url = GenerateEmbedUrl();

            wvPreview.CoreWebView2.Navigate(url);
        }
    }
}
