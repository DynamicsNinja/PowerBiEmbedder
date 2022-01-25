namespace Fic.XTB.PowerBiEmbedder.Form
{
    partial class ReportPreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wvPreview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.lblRecord = new System.Windows.Forms.Label();
            this.cbRecords = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.wvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // wvPreview
            // 
            this.wvPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wvPreview.CreationProperties = null;
            this.wvPreview.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wvPreview.Location = new System.Drawing.Point(12, 64);
            this.wvPreview.Name = "wvPreview";
            this.wvPreview.Size = new System.Drawing.Size(1071, 646);
            this.wvPreview.TabIndex = 0;
            this.wvPreview.ZoomFactor = 1D;
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(12, 22);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(73, 20);
            this.lblRecord.TabIndex = 1;
            this.lblRecord.Text = "Contacts";
            // 
            // cbRecords
            // 
            this.cbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRecords.FormattingEnabled = true;
            this.cbRecords.Location = new System.Drawing.Point(189, 19);
            this.cbRecords.Name = "cbRecords";
            this.cbRecords.Size = new System.Drawing.Size(894, 28);
            this.cbRecords.TabIndex = 2;
            this.cbRecords.SelectedIndexChanged += new System.EventHandler(this.cbRecords_SelectedIndexChanged);
            // 
            // ReportPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 722);
            this.Controls.Add(this.cbRecords);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.wvPreview);
            this.Name = "ReportPreviewForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportPreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wvPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 wvPreview;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.ComboBox cbRecords;
    }
}