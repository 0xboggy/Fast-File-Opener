using System.Windows.Forms;
partial class Form1
{
    private System.ComponentModel.IContainer components = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFastFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFastFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFastFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRawFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFFExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFastFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.advFileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFastFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressAllCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastFileInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.rawFileMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressRawFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.stubCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.rawFileMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFastFileToolStripMenuItem,
            this.saveFastFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.closeFastFile,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFastFileToolStripMenuItem
            // 
            this.openFastFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFastFileToolStripMenuItem1,
            this.openToolStripMenuItem});
            this.openFastFileToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.open;
            this.openFastFileToolStripMenuItem.Name = "openFastFileToolStripMenuItem";
            this.openFastFileToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openFastFileToolStripMenuItem.Text = "Open               ";
            // 
            // openFastFileToolStripMenuItem1
            // 
            this.openFastFileToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFastFileToolStripMenuItem1.Name = "openFastFileToolStripMenuItem1";
            this.openFastFileToolStripMenuItem1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.openFastFileToolStripMenuItem1.RightToLeftAutoMirrorImage = true;
            this.openFastFileToolStripMenuItem1.Size = new System.Drawing.Size(190, 26);
            this.openFastFileToolStripMenuItem1.Text = "Open Fast File";
            this.openFastFileToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openFastFileToolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.openFastFileToolStripMenuItem1.Click += new System.EventHandler(this.openFastFileToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.openToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.openToolStripMenuItem.Text = "Open .FF Extract";
            this.openToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveFastFileToolStripMenuItem
            // 
            this.saveFastFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveRawFileToolStripMenuItem,
            this.saveFFExtractToolStripMenuItem,
            this.saveFastFileToolStripMenuItem1,
            this.advFileSaveToolStripMenuItem});
            this.saveFastFileToolStripMenuItem.Enabled = false;
            this.saveFastFileToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.blueSave;
            this.saveFastFileToolStripMenuItem.Name = "saveFastFileToolStripMenuItem";
            this.saveFastFileToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveFastFileToolStripMenuItem.Text = "Save";
            // 
            // saveRawFileToolStripMenuItem
            // 
            this.saveRawFileToolStripMenuItem.Name = "saveRawFileToolStripMenuItem";
            this.saveRawFileToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.saveRawFileToolStripMenuItem.Text = "Save Raw File";
            this.saveRawFileToolStripMenuItem.Click += new System.EventHandler(this.saveRawFileToolStripMenuItem_Click);
            // 
            // saveFFExtractToolStripMenuItem
            // 
            this.saveFFExtractToolStripMenuItem.Name = "saveFFExtractToolStripMenuItem";
            this.saveFFExtractToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.saveFFExtractToolStripMenuItem.Text = "Save .FF Extract";
            this.saveFFExtractToolStripMenuItem.Click += new System.EventHandler(this.saveFFExtractToolStripMenuItem_Click);
            // 
            // saveFastFileToolStripMenuItem1
            // 
            this.saveFastFileToolStripMenuItem1.Name = "saveFastFileToolStripMenuItem1";
            this.saveFastFileToolStripMenuItem1.Size = new System.Drawing.Size(185, 26);
            this.saveFastFileToolStripMenuItem1.Text = "Save Fast File";
            this.saveFastFileToolStripMenuItem1.Click += new System.EventHandler(this.saveFastFile_Click);
            // 
            // advFileSaveToolStripMenuItem
            // 
            this.advFileSaveToolStripMenuItem.Name = "advFileSaveToolStripMenuItem";
            this.advFileSaveToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.advFileSaveToolStripMenuItem.Text = "Adv. File Save";
            this.advFileSaveToolStripMenuItem.Click += new System.EventHandler(this.advFileSaveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderToolStripMenuItem,
            this.fileToolStripMenuItem1});
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Image = global::Fast_File_Opener.Properties.Resources.inject;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem1.Text = "Inject";
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.folder;
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.folderToolStripMenuItem.Text = "Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Image = global::Fast_File_Opener.Properties.Resources.file;
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(126, 26);
            this.fileToolStripMenuItem1.Text = "File";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem2.Text = "Eject";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // closeFastFile
            // 
            this.closeFastFile.Enabled = false;
            this.closeFastFile.Image = ((System.Drawing.Image)(resources.GetObject("closeFastFile.Image")));
            this.closeFastFile.Name = "closeFastFile";
            this.closeFastFile.Size = new System.Drawing.Size(181, 26);
            this.closeFastFile.Text = "Close File";
            this.closeFastFile.Click += new System.EventHandler(this.closeFastFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditsToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("creditsToolStripMenuItem.Image")));
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.devToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1166, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // devToolStripMenuItem
            // 
            this.devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeFFToolStripMenuItem,
            this.removeAllCommentsToolStripMenuItem,
            this.compressAllCodeToolStripMenuItem,
            this.fastFileInfoToolStripMenuItem});
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.devToolStripMenuItem.Text = "Developer";
            // 
            // resizeFFToolStripMenuItem
            // 
            this.resizeFFToolStripMenuItem.Enabled = false;
            this.resizeFFToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.resize;
            this.resizeFFToolStripMenuItem.Name = "resizeFFToolStripMenuItem";
            this.resizeFFToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.resizeFFToolStripMenuItem.Text = "Resize FF";
            this.resizeFFToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // removeAllCommentsToolStripMenuItem
            // 
            this.removeAllCommentsToolStripMenuItem.Enabled = false;
            this.removeAllCommentsToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.Remove_From_Code;
            this.removeAllCommentsToolStripMenuItem.Name = "removeAllCommentsToolStripMenuItem";
            this.removeAllCommentsToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.removeAllCommentsToolStripMenuItem.Text = "Remove All Comments";
            this.removeAllCommentsToolStripMenuItem.Click += new System.EventHandler(this.removeAllCommentsToolStripMenuItem_Click);
            // 
            // compressAllCodeToolStripMenuItem
            // 
            this.compressAllCodeToolStripMenuItem.Enabled = false;
            this.compressAllCodeToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.compress2;
            this.compressAllCodeToolStripMenuItem.Name = "compressAllCodeToolStripMenuItem";
            this.compressAllCodeToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.compressAllCodeToolStripMenuItem.Text = "Compress All Script Files";
            this.compressAllCodeToolStripMenuItem.Click += new System.EventHandler(this.compressAllCodeToolStripMenuItem_Click);
            // 
            // fastFileInfoToolStripMenuItem
            // 
            this.fastFileInfoToolStripMenuItem.Enabled = false;
            this.fastFileInfoToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.fileInfo;
            this.fastFileInfoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.fastFileInfoToolStripMenuItem.Name = "fastFileInfoToolStripMenuItem";
            this.fastFileInfoToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.fastFileInfoToolStripMenuItem.Text = "Fast File Info";
            this.fastFileInfoToolStripMenuItem.Click += new System.EventHandler(this.fastFileInfoToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1166, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ActiveLinkColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(1138, 20);
            this.toolStripStatusLabel3.Spring = true;
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel4.Text = " ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1166, 474);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.CausesValidation = false;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ForeColor = System.Drawing.Color.Black;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.PathSeparator = "";
            this.treeView1.Size = new System.Drawing.Size(303, 472);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.LostFocus += new System.EventHandler(this.treeView1_LostFocus);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_ContextMenu);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(855, 425);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.MaxLength = 2147483647;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(855, 425);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 47);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightGray;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox2, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(855, 47);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(564, 41);
            this.textBox2.TabIndex = 0;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(573, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Find Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.findNextStringButton);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(703, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 41);
            this.button2.TabIndex = 2;
            this.button2.Text = "Previous";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.findPreviousStringButton);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Fast_File_Opener.Properties.Resources.closeFind;
            this.pictureBox2.Location = new System.Drawing.Point(833, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(19, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_Clicked);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_Click);
            // 
            // rawFileMenuStrip
            // 
            this.rawFileMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rawFileMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rawFileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.compressRawFileToolStripMenuItem,
            this.toolStripMenuItem3,
            this.stubCodeToolStripMenuItem});
            this.rawFileMenuStrip.Name = "contextMenuStrip1";
            this.rawFileMenuStrip.Size = new System.Drawing.Size(196, 134);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.blueSave;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.saveToolStripMenuItem.Text = "Save Raw File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renameToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.rename2;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.renameToolStripMenuItem.Text = "Rename Raw File";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // compressRawFileToolStripMenuItem
            // 
            this.compressRawFileToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.compress2;
            this.compressRawFileToolStripMenuItem.Name = "compressRawFileToolStripMenuItem";
            this.compressRawFileToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.compressRawFileToolStripMenuItem.Text = "Compress Code";
            this.compressRawFileToolStripMenuItem.Click += new System.EventHandler(this.compressRawFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::Fast_File_Opener.Properties.Resources.download;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(195, 26);
            this.toolStripMenuItem3.Text = "Format Code";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.formatCode);
            // 
            // stubCodeToolStripMenuItem
            // 
            this.stubCodeToolStripMenuItem.Image = global::Fast_File_Opener.Properties.Resources.stub;
            this.stubCodeToolStripMenuItem.Name = "stubCodeToolStripMenuItem";
            this.stubCodeToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.stubCodeToolStripMenuItem.Text = "Stub code";
            this.stubCodeToolStripMenuItem.Click += new System.EventHandler(this.stubCodeToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Fast_File_Opener.Properties.Resources.agreedbog1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1166, 474);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1166, 527);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(995, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fast File Opener";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_Close);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.rawFileMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openFastFileToolStripMenuItem;
    public ToolStripMenuItem saveFastFileToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem creditsToolStripMenuItem;
    private MenuStrip menuStrip1;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripMenuItem saveRawFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripStatusLabel toolStripStatusLabel3;
    private ToolStripStatusLabel toolStripStatusLabel4;
    private SplitContainer splitContainer1;
    public TreeView treeView1;
    private ToolStripMenuItem openFastFileToolStripMenuItem1;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveFFExtractToolStripMenuItem;
    private ToolStripMenuItem saveFastFileToolStripMenuItem1;
    private ToolStripMenuItem advFileSaveToolStripMenuItem;
    public ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem folderToolStripMenuItem;
    private ToolStripMenuItem fileToolStripMenuItem1;
    private ToolStripMenuItem exitToolStripMenuItem;
    public ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem devToolStripMenuItem;
    public ToolStripMenuItem resizeFFToolStripMenuItem;
    public ToolStripMenuItem removeAllCommentsToolStripMenuItem;
    public ToolStripMenuItem compressAllCodeToolStripMenuItem;
    public ContextMenuStrip rawFileMenuStrip;
    private ToolStripMenuItem renameToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    public ToolStripMenuItem fastFileInfoToolStripMenuItem;
    public ToolStripMenuItem compressRawFileToolStripMenuItem;
    public ToolStripMenuItem stubCodeToolStripMenuItem;
    public PictureBox pictureBox1;
    public ToolStripMenuItem closeFastFile;
    private Panel panel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TextBox textBox2;
    private Button button1;
    private Button button2;
    public TextBox textBox1;
    private PictureBox pictureBox2;
    private TableLayoutPanel tableLayoutPanel1;
    private ToolStripMenuItem toolStripMenuItem3;
}