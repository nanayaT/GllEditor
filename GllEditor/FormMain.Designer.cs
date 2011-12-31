namespace GrassLikeLanguage.Editor
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.新規作成NToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.開くOToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.名前を付けて保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openFileDialogSource = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialogSource = new System.Windows.Forms.SaveFileDialog();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.labelPrintStack = new System.Windows.Forms.Label();
			this.labelVisualizedLanguage = new System.Windows.Forms.Label();
			this.labelSourceCode = new System.Windows.Forms.Label();
			this.toolStripMainLabel = new System.Windows.Forms.ToolStrip();
			this.toolStripSplitButtonFile = new System.Windows.Forms.ToolStripDropDownButton();
			this.newTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSplitButtonEdit = new System.Windows.Forms.ToolStripDropDownButton();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.sourceToVisualizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.visualizedToSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButtonRule = new System.Windows.Forms.ToolStripDropDownButton();
			this.grassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ほむほむToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSplitButtonHelp = new System.Windows.Forms.ToolStripDropDownButton();
			this.customTextBoxPrintStack = new GrassLikeLanguage.Editor.CustomTextBox();
			this.textBoxVisualizedLanguage = new GrassLikeLanguage.Editor.CustomTextBox();
			this.textBoxSourceCode = new GrassLikeLanguage.Editor.CustomTextBox();
			this.toolStripMain.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStripMainLabel.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMain
			// 
			this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripButton,
            this.開くOToolStripButton,
            this.名前を付けて保存SToolStripButton,
            this.切り取りUToolStripButton,
            this.印刷PToolStripButton,
            this.toolStripSeparator,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.toolStripSeparator1,
            this.ヘルプLToolStripButton});
			this.toolStripMain.Location = new System.Drawing.Point(3, 25);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(185, 25);
			this.toolStripMain.TabIndex = 0;
			this.toolStripMain.Text = "toolStripMain";
			// 
			// 新規作成NToolStripButton
			// 
			this.新規作成NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.新規作成NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripButton.Image")));
			this.新規作成NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.新規作成NToolStripButton.Name = "新規作成NToolStripButton";
			this.新規作成NToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.新規作成NToolStripButton.Text = "新規作成(&N)";
			this.新規作成NToolStripButton.Click += new System.EventHandler(this.新規作成NToolStripButton_Click);
			// 
			// 開くOToolStripButton
			// 
			this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
			this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.開くOToolStripButton.Name = "開くOToolStripButton";
			this.開くOToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.開くOToolStripButton.Text = "開く(&O)";
			this.開くOToolStripButton.Click += new System.EventHandler(this.開くOToolStripButton_Click);
			// 
			// 名前を付けて保存SToolStripButton
			// 
			this.名前を付けて保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.名前を付けて保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("名前を付けて保存SToolStripButton.Image")));
			this.名前を付けて保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.名前を付けて保存SToolStripButton.Name = "名前を付けて保存SToolStripButton";
			this.名前を付けて保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.名前を付けて保存SToolStripButton.Text = "名前を付けて保存(&S)";
			this.名前を付けて保存SToolStripButton.ToolTipText = "名前を付けて保存(S)";
			this.名前を付けて保存SToolStripButton.Click += new System.EventHandler(this.名前を付けて保存SToolStripButton_Click);
			// 
			// 切り取りUToolStripButton
			// 
			this.切り取りUToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.切り取りUToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("切り取りUToolStripButton.Image")));
			this.切り取りUToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.切り取りUToolStripButton.Name = "切り取りUToolStripButton";
			this.切り取りUToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.切り取りUToolStripButton.Text = "切り取り(&U)";
			this.切り取りUToolStripButton.Click += new System.EventHandler(this.切り取りUToolStripButton_Click);
			// 
			// 印刷PToolStripButton
			// 
			this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.印刷PToolStripButton.Enabled = false;
			this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
			this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.印刷PToolStripButton.Name = "印刷PToolStripButton";
			this.印刷PToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.印刷PToolStripButton.Text = "印刷(&P)";
			this.印刷PToolStripButton.Visible = false;
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// コピーCToolStripButton
			// 
			this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
			this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.コピーCToolStripButton.Name = "コピーCToolStripButton";
			this.コピーCToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.コピーCToolStripButton.Text = "コピー(&C)";
			this.コピーCToolStripButton.Click += new System.EventHandler(this.コピーCToolStripButton_Click);
			// 
			// 貼り付けPToolStripButton
			// 
			this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
			this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
			this.貼り付けPToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
			this.貼り付けPToolStripButton.Click += new System.EventHandler(this.貼り付けPToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// ヘルプLToolStripButton
			// 
			this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
			this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
			this.ヘルプLToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
			this.ヘルプLToolStripButton.Click += new System.EventHandler(this.ヘルプLToolStripButton_Click);
			// 
			// openFileDialogSource
			// 
			this.openFileDialogSource.FileName = "*.*";
			this.openFileDialogSource.Filter = "すべて(*.*)|*.*|Grass(*.grass)|*.grass|ほむほむ(*.homehome)|*.homehome";
			// 
			// saveFileDialogSource
			// 
			this.saveFileDialogSource.FileName = "*.*";
			this.saveFileDialogSource.Filter = "すべて(*.*)|*.*|Grass(*.grass)|*.grass|ほむほむ(*.homehome)|*.homehome";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.labelPrintStack);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.customTextBoxPrintStack);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.textBoxVisualizedLanguage);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.textBoxSourceCode);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.labelVisualizedLanguage);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.labelSourceCode);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1008, 680);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(1008, 730);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMainLabel);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMain);
			// 
			// labelPrintStack
			// 
			this.labelPrintStack.AutoSize = true;
			this.labelPrintStack.Location = new System.Drawing.Point(11, 527);
			this.labelPrintStack.Name = "labelPrintStack";
			this.labelPrintStack.Size = new System.Drawing.Size(67, 12);
			this.labelPrintStack.TabIndex = 3;
			this.labelPrintStack.Text = "ResultStack";
			// 
			// labelVisualizedLanguage
			// 
			this.labelVisualizedLanguage.AutoSize = true;
			this.labelVisualizedLanguage.Location = new System.Drawing.Point(11, 269);
			this.labelVisualizedLanguage.Name = "labelVisualizedLanguage";
			this.labelVisualizedLanguage.Size = new System.Drawing.Size(105, 12);
			this.labelVisualizedLanguage.TabIndex = 0;
			this.labelVisualizedLanguage.Text = "VisualizedLanguage";
			// 
			// labelSourceCode
			// 
			this.labelSourceCode.AutoSize = true;
			this.labelSourceCode.Location = new System.Drawing.Point(11, 11);
			this.labelSourceCode.Name = "labelSourceCode";
			this.labelSourceCode.Size = new System.Drawing.Size(66, 12);
			this.labelSourceCode.TabIndex = 0;
			this.labelSourceCode.Text = "SourceCode";
			// 
			// toolStripMainLabel
			// 
			this.toolStripMainLabel.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripMainLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonFile,
            this.toolStripSplitButtonEdit,
            this.toolStripDropDownButtonRule,
            this.toolStripSplitButtonHelp});
			this.toolStripMainLabel.Location = new System.Drawing.Point(3, 0);
			this.toolStripMainLabel.Name = "toolStripMainLabel";
			this.toolStripMainLabel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripMainLabel.Size = new System.Drawing.Size(260, 25);
			this.toolStripMainLabel.TabIndex = 1;
			// 
			// toolStripSplitButtonFile
			// 
			this.toolStripSplitButtonFile.AutoToolTip = false;
			this.toolStripSplitButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButtonFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTextToolStripMenuItem,
            this.openTextToolStripMenuItem,
            this.saveTextToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.toolStripSplitButtonFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonFile.Image")));
			this.toolStripSplitButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonFile.Name = "toolStripSplitButtonFile";
			this.toolStripSplitButtonFile.Size = new System.Drawing.Size(58, 22);
			this.toolStripSplitButtonFile.Text = "File(&F)";
			// 
			// newTextToolStripMenuItem
			// 
			this.newTextToolStripMenuItem.Name = "newTextToolStripMenuItem";
			this.newTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newTextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.newTextToolStripMenuItem.Text = "New Text";
			this.newTextToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripButton_Click);
			// 
			// openTextToolStripMenuItem
			// 
			this.openTextToolStripMenuItem.Name = "openTextToolStripMenuItem";
			this.openTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openTextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.openTextToolStripMenuItem.Text = "Open Text";
			this.openTextToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripButton_Click);
			// 
			// saveTextToolStripMenuItem
			// 
			this.saveTextToolStripMenuItem.Name = "saveTextToolStripMenuItem";
			this.saveTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveTextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.saveTextToolStripMenuItem.Text = "Save Text";
			this.saveTextToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存SToolStripButton_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolStripSplitButtonEdit
			// 
			this.toolStripSplitButtonEdit.AutoToolTip = false;
			this.toolStripSplitButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButtonEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.sourceToVisualizedToolStripMenuItem,
            this.visualizedToSourceToolStripMenuItem,
            this.toolStripSeparator3,
            this.runToolStripMenuItem});
			this.toolStripSplitButtonEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonEdit.Image")));
			this.toolStripSplitButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonEdit.Name = "toolStripSplitButtonEdit";
			this.toolStripSplitButtonEdit.Size = new System.Drawing.Size(60, 22);
			this.toolStripSplitButtonEdit.Text = "Edit(&E)";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.コピーCToolStripButton_Click);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.切り取りUToolStripButton_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.貼り付けPToolStripButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
			// 
			// sourceToVisualizedToolStripMenuItem
			// 
			this.sourceToVisualizedToolStripMenuItem.Name = "sourceToVisualizedToolStripMenuItem";
			this.sourceToVisualizedToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.sourceToVisualizedToolStripMenuItem.Text = "SourceToVisualized";
			this.sourceToVisualizedToolStripMenuItem.Click += new System.EventHandler(this.sourceToVisualizedToolStripMenuItem_Click);
			// 
			// visualizedToSourceToolStripMenuItem
			// 
			this.visualizedToSourceToolStripMenuItem.Name = "visualizedToSourceToolStripMenuItem";
			this.visualizedToSourceToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.visualizedToSourceToolStripMenuItem.Text = "VisualizedToSource";
			this.visualizedToSourceToolStripMenuItem.Click += new System.EventHandler(this.visualizedToSourceToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(184, 6);
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.runToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.runToolStripMenuItem.Text = "Run";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// toolStripDropDownButtonRule
			// 
			this.toolStripDropDownButtonRule.AutoToolTip = false;
			this.toolStripDropDownButtonRule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grassToolStripMenuItem,
            this.ほむほむToolStripMenuItem});
			this.toolStripDropDownButtonRule.Name = "toolStripDropDownButtonRule";
			this.toolStripDropDownButtonRule.Size = new System.Drawing.Size(64, 22);
			this.toolStripDropDownButtonRule.Text = "Rule(&R)";
			// 
			// grassToolStripMenuItem
			// 
			this.grassToolStripMenuItem.Checked = true;
			this.grassToolStripMenuItem.CheckOnClick = true;
			this.grassToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.grassToolStripMenuItem.Name = "grassToolStripMenuItem";
			this.grassToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.grassToolStripMenuItem.Text = "Grass";
			this.grassToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.grassToolStripMenuItem_CheckStateChanged);
			this.grassToolStripMenuItem.Click += new System.EventHandler(this.grassToolStripMenuItem_Click);
			// 
			// ほむほむToolStripMenuItem
			// 
			this.ほむほむToolStripMenuItem.CheckOnClick = true;
			this.ほむほむToolStripMenuItem.Name = "ほむほむToolStripMenuItem";
			this.ほむほむToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.ほむほむToolStripMenuItem.Text = "ほむほむ";
			this.ほむほむToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.ほむほむToolStripMenuItem_CheckStateChanged);
			this.ほむほむToolStripMenuItem.Click += new System.EventHandler(this.ほむほむToolStripMenuItem_Click);
			// 
			// toolStripSplitButtonHelp
			// 
			this.toolStripSplitButtonHelp.AutoToolTip = false;
			this.toolStripSplitButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonHelp.Image")));
			this.toolStripSplitButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonHelp.Name = "toolStripSplitButtonHelp";
			this.toolStripSplitButtonHelp.Size = new System.Drawing.Size(66, 22);
			this.toolStripSplitButtonHelp.Text = "Help(&H)";
			this.toolStripSplitButtonHelp.Click += new System.EventHandler(this.ヘルプLToolStripButton_Click);
			// 
			// customTextBoxPrintStack
			// 
			this.customTextBoxPrintStack.AcceptsTab = true;
			this.customTextBoxPrintStack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.customTextBoxPrintStack.Location = new System.Drawing.Point(11, 542);
			this.customTextBoxPrintStack.Multiline = true;
			this.customTextBoxPrintStack.Name = "customTextBoxPrintStack";
			this.customTextBoxPrintStack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.customTextBoxPrintStack.ShortcutsEnabled = false;
			this.customTextBoxPrintStack.Size = new System.Drawing.Size(982, 125);
			this.customTextBoxPrintStack.TabIndex = 2;
			this.customTextBoxPrintStack.TabStop = false;
			this.customTextBoxPrintStack.WordWrap = false;
			// 
			// textBoxVisualizedLanguage
			// 
			this.textBoxVisualizedLanguage.AcceptsReturn = true;
			this.textBoxVisualizedLanguage.AcceptsTab = true;
			this.textBoxVisualizedLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxVisualizedLanguage.Location = new System.Drawing.Point(13, 284);
			this.textBoxVisualizedLanguage.Multiline = true;
			this.textBoxVisualizedLanguage.Name = "textBoxVisualizedLanguage";
			this.textBoxVisualizedLanguage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxVisualizedLanguage.ShortcutsEnabled = false;
			this.textBoxVisualizedLanguage.Size = new System.Drawing.Size(982, 240);
			this.textBoxVisualizedLanguage.TabIndex = 1;
			this.textBoxVisualizedLanguage.TabStop = false;
			this.textBoxVisualizedLanguage.WordWrap = false;
			this.textBoxVisualizedLanguage.TextChanged += new System.EventHandler(this.textBoxVisualizedLanguage_TextChanged);
			this.textBoxVisualizedLanguage.Enter += new System.EventHandler(this.textBoxVisualizedLanguage_Enter);
			this.textBoxVisualizedLanguage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxVisualizedLanguage_KeyPress);
			this.textBoxVisualizedLanguage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyUp);
			// 
			// textBoxSourceCode
			// 
			this.textBoxSourceCode.AcceptsReturn = true;
			this.textBoxSourceCode.AcceptsTab = true;
			this.textBoxSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSourceCode.Location = new System.Drawing.Point(11, 26);
			this.textBoxSourceCode.MaxLength = 0;
			this.textBoxSourceCode.Multiline = true;
			this.textBoxSourceCode.Name = "textBoxSourceCode";
			this.textBoxSourceCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxSourceCode.ShortcutsEnabled = false;
			this.textBoxSourceCode.Size = new System.Drawing.Size(984, 240);
			this.textBoxSourceCode.TabIndex = 1;
			this.textBoxSourceCode.WordWrap = false;
			this.textBoxSourceCode.TextChanged += new System.EventHandler(this.textBoxSourceCode_TextChanged);
			this.textBoxSourceCode.Enter += new System.EventHandler(this.textBoxSourceCode_Enter);
			this.textBoxSourceCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSourceCode_KeyPress);
			this.textBoxSourceCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyUp);
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 730);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "FormMain";
			this.Text = "Grass Like Language Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStripMainLabel.ResumeLayout(false);
			this.toolStripMainLabel.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton 新規作成NToolStripButton;
        private System.Windows.Forms.ToolStripButton 開くOToolStripButton;
        private System.Windows.Forms.ToolStripButton 名前を付けて保存SToolStripButton;
        private System.Windows.Forms.ToolStripButton 印刷PToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton 切り取りUToolStripButton;
        private System.Windows.Forms.ToolStripButton コピーCToolStripButton;
        private System.Windows.Forms.ToolStripButton 貼り付けPToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ヘルプLToolStripButton;
        private System.Windows.Forms.OpenFileDialog openFileDialogSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSource;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Label labelSourceCode;
		private CustomTextBox textBoxSourceCode;
        private System.Windows.Forms.Label labelVisualizedLanguage;
		private CustomTextBox textBoxVisualizedLanguage;
		private System.Windows.Forms.ToolStrip toolStripMainLabel;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonRule;
		private System.Windows.Forms.ToolStripMenuItem grassToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ほむほむToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButtonFile;
		private System.Windows.Forms.ToolStripMenuItem newTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButtonEdit;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem sourceToVisualizedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem visualizedToSourceToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButtonHelp;
        private CustomTextBox customTextBoxPrintStack;
        private System.Windows.Forms.Label labelPrintStack;
    }
}

