namespace Fic.XTB.PowerBiEmbedder
{
    partial class PowerBiEmbedder
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerBiEmbedder));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPublish = new System.Windows.Forms.ToolStripButton();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblPbiSettings = new System.Windows.Forms.ToolStripLabel();
            this.cmbPbiSettings = new System.Windows.Forms.ToolStripComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.cbxLinkValues = new System.Windows.Forms.CheckBox();
            this.lblLinkValues = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.cmbTab = new System.Windows.Forms.ComboBox();
            this.lblTab = new System.Windows.Forms.Label();
            this.cmbForm = new System.Windows.Forms.ComboBox();
            this.lblForm = new System.Windows.Forms.Label();
            this.cmbEntity = new System.Windows.Forms.ComboBox();
            this.lblEntity = new System.Windows.Forms.Label();
            this.gbPowerBiConfig = new System.Windows.Forms.GroupBox();
            this.cbPage = new System.Windows.Forms.ComboBox();
            this.tbPbiPage = new System.Windows.Forms.TextBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.cbReport = new System.Windows.Forms.ComboBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.pMethod = new System.Windows.Forms.Panel();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.rbApi = new System.Windows.Forms.RadioButton();
            this.lblMethod = new System.Windows.Forms.Label();
            this.tbPbiUrl = new System.Windows.Forms.TextBox();
            this.lblPbiUrl = new System.Windows.Forms.Label();
            this.cbxPbiFilter = new System.Windows.Forms.CheckBox();
            this.lblPbiFilter = new System.Windows.Forms.Label();
            this.tbGrpId = new System.Windows.Forms.TextBox();
            this.tbReportId = new System.Windows.Forms.TextBox();
            this.lblReportId = new System.Windows.Forms.Label();
            this.lblGrpId = new System.Windows.Forms.Label();
            this.gbFormatting = new System.Windows.Forms.GroupBox();
            this.lblRowspan = new System.Windows.Forms.Label();
            this.lblSectionName = new System.Windows.Forms.Label();
            this.tbSectionName = new System.Windows.Forms.TextBox();
            this.tbRowspan = new System.Windows.Forms.TextBox();
            this.gbPbiFilters = new System.Windows.Forms.GroupBox();
            this.cmbEntityField = new System.Windows.Forms.ComboBox();
            this.lblCdsField = new System.Windows.Forms.Label();
            this.lblPbiColumn = new System.Windows.Forms.Label();
            this.lblPbiTable = new System.Windows.Forms.Label();
            this.tbPbiTable = new System.Windows.Forms.TextBox();
            this.tbPbiColumn = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.gbPowerBiConfig.SuspendLayout();
            this.pMethod.SuspendLayout();
            this.gbFormatting.SuspendLayout();
            this.gbPbiFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.tssSeparator1,
            this.btnPublish,
            this.btnConnect,
            this.tsSeparator1,
            this.lblPbiSettings,
            this.cmbPbiSettings});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(968, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 28);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnPublish
            // 
            this.btnPublish.Enabled = false;
            this.btnPublish.Image = ((System.Drawing.Image)(resources.GetObject("btnPublish.Image")));
            this.btnPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(133, 28);
            this.btnPublish.Text = "Publish Report";
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(134, 28);
            this.btnConnect.Text = "Connect to PBI";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // lblPbiSettings
            // 
            this.lblPbiSettings.Name = "lblPbiSettings";
            this.lblPbiSettings.Size = new System.Drawing.Size(168, 28);
            this.lblPbiSettings.Text = "Embed Power BI Setting";
            // 
            // cmbPbiSettings
            // 
            this.cmbPbiSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPbiSettings.Enabled = false;
            this.cmbPbiSettings.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.cmbPbiSettings.Name = "cmbPbiSettings";
            this.cmbPbiSettings.Size = new System.Drawing.Size(121, 31);
            this.cmbPbiSettings.SelectedIndexChanged += new System.EventHandler(this.cmbPbiSettings_Change);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gbTarget, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbPowerBiConfig, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFormatting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbPbiFilters, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 577);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // gbTarget
            // 
            this.gbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTarget.Controls.Add(this.cbxLinkValues);
            this.gbTarget.Controls.Add(this.lblLinkValues);
            this.gbTarget.Controls.Add(this.cmbSection);
            this.gbTarget.Controls.Add(this.lblSection);
            this.gbTarget.Controls.Add(this.cmbTab);
            this.gbTarget.Controls.Add(this.lblTab);
            this.gbTarget.Controls.Add(this.cmbForm);
            this.gbTarget.Controls.Add(this.lblForm);
            this.gbTarget.Controls.Add(this.cmbEntity);
            this.gbTarget.Controls.Add(this.lblEntity);
            this.gbTarget.Enabled = false;
            this.gbTarget.Location = new System.Drawing.Point(3, 3);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(475, 322);
            this.gbTarget.TabIndex = 24;
            this.gbTarget.TabStop = false;
            this.gbTarget.Text = "Target";
            // 
            // cbxLinkValues
            // 
            this.cbxLinkValues.AutoSize = true;
            this.cbxLinkValues.Checked = true;
            this.cbxLinkValues.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxLinkValues.Enabled = false;
            this.cbxLinkValues.Location = new System.Drawing.Point(111, 236);
            this.cbxLinkValues.Name = "cbxLinkValues";
            this.cbxLinkValues.Size = new System.Drawing.Size(18, 17);
            this.cbxLinkValues.TabIndex = 29;
            this.cbxLinkValues.UseVisualStyleBackColor = true;
            this.cbxLinkValues.CheckedChanged += new System.EventHandler(this.cbxLockConfig_CheckedChanged);
            // 
            // lblLinkValues
            // 
            this.lblLinkValues.AutoSize = true;
            this.lblLinkValues.Location = new System.Drawing.Point(6, 234);
            this.lblLinkValues.Name = "lblLinkValues";
            this.lblLinkValues.Size = new System.Drawing.Size(34, 17);
            this.lblLinkValues.TabIndex = 14;
            this.lblLinkValues.Text = "Link";
            this.toolTip1.SetToolTip(this.lblLinkValues, "optrbv rtv rtvetrvbwtvb tbvrgwe vtwrbev rwebv rvteqbvrw ebvw");
            // 
            // cmbSection
            // 
            this.cmbSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(111, 181);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(322, 24);
            this.cmbSection.TabIndex = 13;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            this.cmbSection.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSection_Validating);
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(6, 184);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(55, 17);
            this.lblSection.TabIndex = 12;
            this.lblSection.Text = "Section";
            // 
            // cmbTab
            // 
            this.cmbTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTab.FormattingEnabled = true;
            this.cmbTab.Location = new System.Drawing.Point(111, 133);
            this.cmbTab.Name = "cmbTab";
            this.cmbTab.Size = new System.Drawing.Size(322, 24);
            this.cmbTab.TabIndex = 11;
            this.cmbTab.SelectedIndexChanged += new System.EventHandler(this.cmbTab_SelectedIndexChanged);
            this.cmbTab.Validating += new System.ComponentModel.CancelEventHandler(this.cmbTab_Validating);
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Location = new System.Drawing.Point(6, 136);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(33, 17);
            this.lblTab.TabIndex = 10;
            this.lblTab.Text = "Tab";
            // 
            // cmbForm
            // 
            this.cmbForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForm.FormattingEnabled = true;
            this.cmbForm.Location = new System.Drawing.Point(111, 82);
            this.cmbForm.Name = "cmbForm";
            this.cmbForm.Size = new System.Drawing.Size(322, 24);
            this.cmbForm.TabIndex = 9;
            this.cmbForm.SelectedIndexChanged += new System.EventHandler(this.cmbForm_SelectedIndexChanged);
            this.cmbForm.Validating += new System.ComponentModel.CancelEventHandler(this.cmbForm_Validating);
            // 
            // lblForm
            // 
            this.lblForm.AutoSize = true;
            this.lblForm.Location = new System.Drawing.Point(6, 85);
            this.lblForm.Name = "lblForm";
            this.lblForm.Size = new System.Drawing.Size(40, 17);
            this.lblForm.TabIndex = 8;
            this.lblForm.Text = "Form";
            // 
            // cmbEntity
            // 
            this.cmbEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntity.FormattingEnabled = true;
            this.cmbEntity.Location = new System.Drawing.Point(111, 31);
            this.cmbEntity.Name = "cmbEntity";
            this.cmbEntity.Size = new System.Drawing.Size(322, 24);
            this.cmbEntity.TabIndex = 7;
            this.cmbEntity.SelectedIndexChanged += new System.EventHandler(this.cmbEntity_SelectedIndexChanged);
            this.cmbEntity.Validating += new System.ComponentModel.CancelEventHandler(this.cmbEntity_Validating);
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.Location = new System.Drawing.Point(6, 34);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(43, 17);
            this.lblEntity.TabIndex = 6;
            this.lblEntity.Text = "Entity";
            // 
            // gbPowerBiConfig
            // 
            this.gbPowerBiConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPowerBiConfig.Controls.Add(this.cbPage);
            this.gbPowerBiConfig.Controls.Add(this.tbPbiPage);
            this.gbPowerBiConfig.Controls.Add(this.lblPage);
            this.gbPowerBiConfig.Controls.Add(this.cbReport);
            this.gbPowerBiConfig.Controls.Add(this.cbGroup);
            this.gbPowerBiConfig.Controls.Add(this.pMethod);
            this.gbPowerBiConfig.Controls.Add(this.lblMethod);
            this.gbPowerBiConfig.Controls.Add(this.tbPbiUrl);
            this.gbPowerBiConfig.Controls.Add(this.lblPbiUrl);
            this.gbPowerBiConfig.Controls.Add(this.cbxPbiFilter);
            this.gbPowerBiConfig.Controls.Add(this.lblPbiFilter);
            this.gbPowerBiConfig.Controls.Add(this.tbGrpId);
            this.gbPowerBiConfig.Controls.Add(this.tbReportId);
            this.gbPowerBiConfig.Controls.Add(this.lblReportId);
            this.gbPowerBiConfig.Controls.Add(this.lblGrpId);
            this.gbPowerBiConfig.Enabled = false;
            this.gbPowerBiConfig.Location = new System.Drawing.Point(484, 3);
            this.gbPowerBiConfig.Name = "gbPowerBiConfig";
            this.gbPowerBiConfig.Size = new System.Drawing.Size(475, 322);
            this.gbPowerBiConfig.TabIndex = 25;
            this.gbPowerBiConfig.TabStop = false;
            this.gbPowerBiConfig.Text = "Power BI Config 🔗";
            // 
            // cbPage
            // 
            this.cbPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPage.FormattingEnabled = true;
            this.cbPage.Location = new System.Drawing.Point(92, 181);
            this.cbPage.Name = "cbPage";
            this.cbPage.Size = new System.Drawing.Size(342, 24);
            this.cbPage.TabIndex = 31;
            this.cbPage.Visible = false;
            this.cbPage.SelectedIndexChanged += new System.EventHandler(this.cbPage_SelectedIndexChanged);
            // 
            // tbPbiPage
            // 
            this.tbPbiPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPbiPage.Location = new System.Drawing.Point(92, 181);
            this.tbPbiPage.Name = "tbPbiPage";
            this.tbPbiPage.Size = new System.Drawing.Size(342, 22);
            this.tbPbiPage.TabIndex = 30;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(6, 184);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(41, 17);
            this.lblPage.TabIndex = 29;
            this.lblPage.Text = "Page";
            // 
            // cbReport
            // 
            this.cbReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReport.FormattingEnabled = true;
            this.cbReport.Location = new System.Drawing.Point(92, 133);
            this.cbReport.Name = "cbReport";
            this.cbReport.Size = new System.Drawing.Size(342, 24);
            this.cbReport.TabIndex = 28;
            this.cbReport.Visible = false;
            this.cbReport.SelectedIndexChanged += new System.EventHandler(this.cbReport_SelectedIndexChanged);
            this.cbReport.Validating += new System.ComponentModel.CancelEventHandler(this.cbReport_Validating);
            // 
            // cbGroup
            // 
            this.cbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(92, 82);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(342, 24);
            this.cbGroup.TabIndex = 14;
            this.cbGroup.Visible = false;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            this.cbGroup.Validating += new System.ComponentModel.CancelEventHandler(this.cbGroup_Validating);
            // 
            // pMethod
            // 
            this.pMethod.Controls.Add(this.rbManual);
            this.pMethod.Controls.Add(this.rbApi);
            this.pMethod.Location = new System.Drawing.Point(92, 21);
            this.pMethod.Name = "pMethod";
            this.pMethod.Size = new System.Drawing.Size(342, 40);
            this.pMethod.TabIndex = 27;
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Checked = true;
            this.rbManual.Location = new System.Drawing.Point(3, 11);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(75, 21);
            this.rbManual.TabIndex = 26;
            this.rbManual.TabStop = true;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.method_CheckedChanged);
            // 
            // rbApi
            // 
            this.rbApi.AutoSize = true;
            this.rbApi.Enabled = false;
            this.rbApi.Location = new System.Drawing.Point(84, 11);
            this.rbApi.Name = "rbApi";
            this.rbApi.Size = new System.Drawing.Size(109, 21);
            this.rbApi.TabIndex = 25;
            this.rbApi.Text = "Power BI API";
            this.rbApi.UseVisualStyleBackColor = true;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(6, 34);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(55, 17);
            this.lblMethod.TabIndex = 24;
            this.lblMethod.Text = "Method";
            // 
            // tbPbiUrl
            // 
            this.tbPbiUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPbiUrl.Location = new System.Drawing.Point(92, 231);
            this.tbPbiUrl.Name = "tbPbiUrl";
            this.tbPbiUrl.Size = new System.Drawing.Size(342, 22);
            this.tbPbiUrl.TabIndex = 23;
            // 
            // lblPbiUrl
            // 
            this.lblPbiUrl.AutoSize = true;
            this.lblPbiUrl.Location = new System.Drawing.Point(6, 234);
            this.lblPbiUrl.Name = "lblPbiUrl";
            this.lblPbiUrl.Size = new System.Drawing.Size(36, 17);
            this.lblPbiUrl.TabIndex = 22;
            this.lblPbiUrl.Text = "URL";
            // 
            // cbxPbiFilter
            // 
            this.cbxPbiFilter.AutoSize = true;
            this.cbxPbiFilter.Location = new System.Drawing.Point(92, 280);
            this.cbxPbiFilter.Name = "cbxPbiFilter";
            this.cbxPbiFilter.Size = new System.Drawing.Size(18, 17);
            this.cbxPbiFilter.TabIndex = 21;
            this.cbxPbiFilter.UseVisualStyleBackColor = true;
            this.cbxPbiFilter.CheckedChanged += new System.EventHandler(this.cbxPbiFilter_CheckedChanged);
            // 
            // lblPbiFilter
            // 
            this.lblPbiFilter.AutoSize = true;
            this.lblPbiFilter.Location = new System.Drawing.Point(6, 279);
            this.lblPbiFilter.Name = "lblPbiFilter";
            this.lblPbiFilter.Size = new System.Drawing.Size(39, 17);
            this.lblPbiFilter.TabIndex = 21;
            this.lblPbiFilter.Text = "Filter";
            // 
            // tbGrpId
            // 
            this.tbGrpId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGrpId.Location = new System.Drawing.Point(92, 82);
            this.tbGrpId.Name = "tbGrpId";
            this.tbGrpId.Size = new System.Drawing.Size(342, 22);
            this.tbGrpId.TabIndex = 16;
            this.tbGrpId.Validating += new System.ComponentModel.CancelEventHandler(this.tbGrpId_Validating);
            // 
            // tbReportId
            // 
            this.tbReportId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReportId.Location = new System.Drawing.Point(92, 133);
            this.tbReportId.Name = "tbReportId";
            this.tbReportId.Size = new System.Drawing.Size(342, 22);
            this.tbReportId.TabIndex = 15;
            this.tbReportId.Validating += new System.ComponentModel.CancelEventHandler(this.tbReportId_Validating);
            // 
            // lblReportId
            // 
            this.lblReportId.AutoSize = true;
            this.lblReportId.Location = new System.Drawing.Point(6, 136);
            this.lblReportId.Name = "lblReportId";
            this.lblReportId.Size = new System.Drawing.Size(68, 17);
            this.lblReportId.TabIndex = 8;
            this.lblReportId.Text = "Report ID";
            // 
            // lblGrpId
            // 
            this.lblGrpId.AutoSize = true;
            this.lblGrpId.Location = new System.Drawing.Point(6, 85);
            this.lblGrpId.Name = "lblGrpId";
            this.lblGrpId.Size = new System.Drawing.Size(65, 17);
            this.lblGrpId.TabIndex = 6;
            this.lblGrpId.Text = "Group ID";
            // 
            // gbFormatting
            // 
            this.gbFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFormatting.Controls.Add(this.lblRowspan);
            this.gbFormatting.Controls.Add(this.lblSectionName);
            this.gbFormatting.Controls.Add(this.tbSectionName);
            this.gbFormatting.Controls.Add(this.tbRowspan);
            this.gbFormatting.Enabled = false;
            this.gbFormatting.Location = new System.Drawing.Point(3, 331);
            this.gbFormatting.Name = "gbFormatting";
            this.gbFormatting.Size = new System.Drawing.Size(475, 243);
            this.gbFormatting.TabIndex = 27;
            this.gbFormatting.TabStop = false;
            this.gbFormatting.Text = "Formatting";
            // 
            // lblRowspan
            // 
            this.lblRowspan.AutoSize = true;
            this.lblRowspan.Location = new System.Drawing.Point(6, 86);
            this.lblRowspan.Name = "lblRowspan";
            this.lblRowspan.Size = new System.Drawing.Size(66, 17);
            this.lblRowspan.TabIndex = 12;
            this.lblRowspan.Text = "Rowspan";
            // 
            // lblSectionName
            // 
            this.lblSectionName.AutoSize = true;
            this.lblSectionName.Location = new System.Drawing.Point(6, 38);
            this.lblSectionName.Name = "lblSectionName";
            this.lblSectionName.Size = new System.Drawing.Size(96, 17);
            this.lblSectionName.TabIndex = 10;
            this.lblSectionName.Text = "Section Name";
            // 
            // tbSectionName
            // 
            this.tbSectionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSectionName.Location = new System.Drawing.Point(108, 35);
            this.tbSectionName.Name = "tbSectionName";
            this.tbSectionName.Size = new System.Drawing.Size(322, 22);
            this.tbSectionName.TabIndex = 17;
            this.tbSectionName.Validating += new System.ComponentModel.CancelEventHandler(this.tbSectionName_Validating);
            // 
            // tbRowspan
            // 
            this.tbRowspan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRowspan.Location = new System.Drawing.Point(108, 83);
            this.tbRowspan.Name = "tbRowspan";
            this.tbRowspan.Size = new System.Drawing.Size(322, 22);
            this.tbRowspan.TabIndex = 18;
            this.tbRowspan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRowspan_KeyPress);
            this.tbRowspan.Validating += new System.ComponentModel.CancelEventHandler(this.tbRowspan_Validating);
            // 
            // gbPbiFilters
            // 
            this.gbPbiFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPbiFilters.Controls.Add(this.cmbEntityField);
            this.gbPbiFilters.Controls.Add(this.lblCdsField);
            this.gbPbiFilters.Controls.Add(this.lblPbiColumn);
            this.gbPbiFilters.Controls.Add(this.lblPbiTable);
            this.gbPbiFilters.Controls.Add(this.tbPbiTable);
            this.gbPbiFilters.Controls.Add(this.tbPbiColumn);
            this.gbPbiFilters.Enabled = false;
            this.gbPbiFilters.Location = new System.Drawing.Point(484, 331);
            this.gbPbiFilters.Name = "gbPbiFilters";
            this.gbPbiFilters.Size = new System.Drawing.Size(475, 243);
            this.gbPbiFilters.TabIndex = 26;
            this.gbPbiFilters.TabStop = false;
            this.gbPbiFilters.Text = "FIlter 🔗";
            // 
            // cmbEntityField
            // 
            this.cmbEntityField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntityField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityField.FormattingEnabled = true;
            this.cmbEntityField.Location = new System.Drawing.Point(92, 127);
            this.cmbEntityField.Name = "cmbEntityField";
            this.cmbEntityField.Size = new System.Drawing.Size(338, 24);
            this.cmbEntityField.TabIndex = 14;
            this.cmbEntityField.Validating += new System.ComponentModel.CancelEventHandler(this.cmbEntityField_Validating);
            // 
            // lblCdsField
            // 
            this.lblCdsField.AutoSize = true;
            this.lblCdsField.Location = new System.Drawing.Point(6, 130);
            this.lblCdsField.Name = "lblCdsField";
            this.lblCdsField.Size = new System.Drawing.Size(70, 17);
            this.lblCdsField.TabIndex = 19;
            this.lblCdsField.Text = "CDS Field";
            // 
            // lblPbiColumn
            // 
            this.lblPbiColumn.AutoSize = true;
            this.lblPbiColumn.Location = new System.Drawing.Point(6, 86);
            this.lblPbiColumn.Name = "lblPbiColumn";
            this.lblPbiColumn.Size = new System.Drawing.Size(80, 17);
            this.lblPbiColumn.TabIndex = 12;
            this.lblPbiColumn.Text = "PBI Column";
            // 
            // lblPbiTable
            // 
            this.lblPbiTable.AutoSize = true;
            this.lblPbiTable.Location = new System.Drawing.Point(6, 38);
            this.lblPbiTable.Name = "lblPbiTable";
            this.lblPbiTable.Size = new System.Drawing.Size(69, 17);
            this.lblPbiTable.TabIndex = 10;
            this.lblPbiTable.Text = "PBI Table";
            // 
            // tbPbiTable
            // 
            this.tbPbiTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPbiTable.Location = new System.Drawing.Point(92, 35);
            this.tbPbiTable.Name = "tbPbiTable";
            this.tbPbiTable.Size = new System.Drawing.Size(342, 22);
            this.tbPbiTable.TabIndex = 17;
            this.tbPbiTable.Validating += new System.ComponentModel.CancelEventHandler(this.tbPbiTable_Validating);
            // 
            // tbPbiColumn
            // 
            this.tbPbiColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPbiColumn.Location = new System.Drawing.Point(92, 83);
            this.tbPbiColumn.Name = "tbPbiColumn";
            this.tbPbiColumn.Size = new System.Drawing.Size(342, 22);
            this.tbPbiColumn.TabIndex = 18;
            this.tbPbiColumn.Validating += new System.ComponentModel.CancelEventHandler(this.tbPbiColumn_Validating);
            // 
            // PowerBiEmbedder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(968, 513);
            this.Name = "PowerBiEmbedder";
            this.Size = new System.Drawing.Size(968, 605);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.PowerBiEmbedder_ConnectionUpdated);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.gbPowerBiConfig.ResumeLayout(false);
            this.gbPowerBiConfig.PerformLayout();
            this.pMethod.ResumeLayout(false);
            this.pMethod.PerformLayout();
            this.gbFormatting.ResumeLayout(false);
            this.gbFormatting.PerformLayout();
            this.gbPbiFilters.ResumeLayout(false);
            this.gbPbiFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolStripButton btnPublish;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripLabel lblPbiSettings;
        private System.Windows.Forms.ToolStripComboBox cmbPbiSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbTarget;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.ComboBox cmbTab;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.ComboBox cmbForm;
        private System.Windows.Forms.Label lblForm;
        private System.Windows.Forms.ComboBox cmbEntity;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.GroupBox gbPowerBiConfig;
        private System.Windows.Forms.TextBox tbPbiUrl;
        private System.Windows.Forms.Label lblPbiUrl;
        private System.Windows.Forms.CheckBox cbxPbiFilter;
        private System.Windows.Forms.Label lblPbiFilter;
        private System.Windows.Forms.TextBox tbGrpId;
        private System.Windows.Forms.TextBox tbReportId;
        private System.Windows.Forms.Label lblReportId;
        private System.Windows.Forms.Label lblGrpId;
        private System.Windows.Forms.GroupBox gbFormatting;
        private System.Windows.Forms.Label lblRowspan;
        private System.Windows.Forms.Label lblSectionName;
        private System.Windows.Forms.TextBox tbSectionName;
        private System.Windows.Forms.TextBox tbRowspan;
        private System.Windows.Forms.GroupBox gbPbiFilters;
        private System.Windows.Forms.ComboBox cmbEntityField;
        private System.Windows.Forms.Label lblCdsField;
        private System.Windows.Forms.Label lblPbiColumn;
        private System.Windows.Forms.Label lblPbiTable;
        private System.Windows.Forms.TextBox tbPbiTable;
        private System.Windows.Forms.TextBox tbPbiColumn;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.Panel pMethod;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.RadioButton rbApi;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.ComboBox cbReport;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.CheckBox cbxLinkValues;
        private System.Windows.Forms.Label lblLinkValues;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbPbiPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.ComboBox cbPage;
    }
}
