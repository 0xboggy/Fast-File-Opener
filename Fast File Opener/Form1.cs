using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public OpenFastFile off;
    public SaveFastFile sff;
    public OpenFileDialog ofd;//Open File Dialogue Box
    FolderBrowserDialog fbd;//Open Folder Dialogue Box
    public bool dialogueOpen = false;
    public TreeNode oldNode = new TreeNode();
    public static int arrayIndex, nodeIndex;
    public bool overFlow = false;//When a file is over the max amount it can't Save

    public Form1()
    {
        InitializeComponent();
        nodeIndex = 0;
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
        //checkForUpdates();
    }
    
    private void openFastFileToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        OpenFileStart("Open a .FF file", ".FF files(*.ff)|*.ff", false);
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileStart("Open a .dat file", ".dat files(*.dat)|*.dat", true);
    }

    private void restoreElements()
    {
        if(OpenFastFile.fileOpened)
        {
            textBox1.Text = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileContents();
            toolStripStatusLabel1.Text = "Max Size: " + off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileSize();
            toolStripStatusLabel2.Text = "Current Size: " + textBox1.Text.Length;
            off.addRawInfo();
        }
    }

    private void OpenFileStart(string title, string filter, bool datExtract)
    {
        ofd = new OpenFileDialog();
        ofd.Title = title;
        ofd.Filter = filter;
        textBox1.Text = "";
        toolStripStatusLabel1.Text = "";
        toolStripStatusLabel2.Text = "";
        treeView1.Nodes.Clear();
        dialogueOpen = true;
        if (ofd.ShowDialog() != DialogResult.OK || !ofd.CheckFileExists)
        {
            dialogueOpen = false;
            restoreElements();
            return;
        }
        dialogueOpen = false;
        openTheFile(ofd.SafeFileName, ofd.FileName, datExtract);
    }

    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
        if(this.Visible && this.CanFocus && !dialogueOpen)
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
        if (this.Visible && !this.CanFocus || dialogueOpen)
            return;
        textBox1.Text = "";
        toolStripStatusLabel1.Text = "";
        toolStripStatusLabel2.Text = "";
        treeView1.Nodes.Clear();
        string[] fileLocation = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileLocation.Length != 1)
        {
            restoreElements();
            MessageBox.Show("Error: You can only drop one file at a time.", "Drag and Drop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string fileExtension = Path.GetExtension(fileLocation[0]);
        if (fileExtension.Contains(".ff") || fileExtension.Contains(".dat"))
        {
            string fileName = Path.GetFileName(fileLocation[0]);
            bool datExtract = false;
            if(fileExtension.Contains(".dat"))
                datExtract = true;
            openTheFile(fileName, fileLocation[0], datExtract);
        }
        else
        {
            restoreElements();
            MessageBox.Show("Error: Could not recognize file as a fast file or extract.", "Drag and Drop File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void openTheFile(string fileName, string fileLocation, bool extract)
    {
        deleteDatFile();
        OpenFastFile.fileOpened = false;
        treeView1.Nodes.Clear();

        off = new OpenFastFile();
        nodeIndex = 0;
        arrayIndex = 0;

        off.setFileName(fileName);
        off.setFileLocation(fileLocation);
        off.setFileDirectory(Path.GetDirectoryName(fileLocation));
        off.openFile(this, extract);

        off.extractOpened = extract;
        saveFastFileToolStripMenuItem1.Enabled = !extract;
        advFileSaveToolStripMenuItem.Enabled = !extract;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        //this.textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (treeView1.SelectedNode.Parent != null || treeView1.SelectedNode.Nodes.Count == 0)
            oldNode.ForeColor = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileTextColor();
        if (treeView1.SelectedNode.Nodes.Count == 0)
        {
            arrayIndex = treeView1.SelectedNode.Index;
            nodeIndex = 0;
            if (e.Node.Parent != null)
            {
                arrayIndex = e.Node.Parent.Index;
                nodeIndex = treeView1.SelectedNode.Index;
            }
            
            textBox1.Text = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileContents();
            if (treeView1.SelectedNode.Parent != null || treeView1.SelectedNode.Nodes.Count == 0)
            {
                oldNode.BackColor = System.Drawing.Color.White;
                oldNode = e.Node;
                e.Node.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
                e.Node.ForeColor = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileTextColor();
            }
            updateToolStrip();
        }
    }

    public void updateToolStrip()
    {
        toolStripStatusLabel1.Text = "Max Size: " + off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileSize();
        toolStripStatusLabel2.Text = "Current Size: " + textBox1.Text.Length;
    }

    public void resetNodeColors()
    {
        oldNode.ForeColor = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileTextColor();
        oldNode.BackColor = System.Drawing.Color.White;
    }

    private void treeView1_LostFocus(object sender, EventArgs e)
    {
        oldNode.ForeColor = System.Drawing.Color.Black;
        oldNode.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
    }

    private void treeView1_ContextMenu(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                string name = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileName();
                if (name.Contains(".gsc") || name.Contains(".cfg") || name.Contains(".csc"))
                    compressRawFileToolStripMenuItem.Enabled = true;
                else
                    compressRawFileToolStripMenuItem.Enabled = false;

                if (name.Contains(".gsc") || name.Contains(".csc"))
                    stubCodeToolStripMenuItem.Enabled = true;
                else
                    stubCodeToolStripMenuItem.Enabled = false;
                rawFileMenuStrip.Show(treeView1, e.Location);
            }
        }
    }

    private void textBox1_KeyDown(object sender, EventArgs e)
    {
        if (e == null || !OpenFastFile.fileOpened) return;
        int size = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileSize();
        toolStripStatusLabel1.Text = "Max Size: " + size;
        if (size >= textBox1.Text.Length)
        {
            overFlow = false;
            toolStripStatusLabel2.Text = "Current Size: " + textBox1.Text.Length;
            toolStripStatusLabel2.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            overFlow = true;
            toolStripStatusLabel2.Text = "Current Size: " + textBox1.Text.Length;
            toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
        }
    }

    public void updateFileInfo()
    {
        try
        {
            saveRawData();
        }
        catch (ProgramException E)
        {
            MessageBox.Show(E.getError(), E.getTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileFreeSpace = (off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileSize - off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileContents.Length);
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
        {
            textBox1.SelectAll();
        }
        else if(e.Control && e.KeyCode == Keys.F)
        {
            //this.tableLayoutPanel1.RowCount = 2;
            //this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            //tableLayoutPanel1.Controls.Add(this.panel1,0,1);
            panel1.Visible = true;
            textBox2.Focus();
            textBox2.SelectAll();
        }
        else if(e.KeyCode == Keys.F2)
        {
            findPreviousString(textBox2.Text);
        }
        else if(e.KeyCode == Keys.F3)
        {
            findNextString(textBox2.Text);
        }
    }

    private void textBox2_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
        {
            textBox2.SelectAll();
        }
        else if (e.KeyCode == Keys.F2)
        {
            findPreviousString(textBox2.Text);
        }
        else if (e.KeyCode == Keys.F3)
        {
            findNextString(textBox2.Text);
        }
    }

    private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AboutBox1 about = new AboutBox1();
        about.ShowDialog(this);
    }

    //SAVE RAW FILE
    private void saveRawFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        formSaveRawFile();
    }

    private void formSaveRawFile()
    {
        try
        {
            saveRawData();
        }
        catch (ProgramException E)
        {
            MessageBox.Show(E.getError(), E.getTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void saveAllRawFiles()
    {
        for (int a = 0; a < off.rawFileInfoTree.Count; a++)
            for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
                replaceData(off.rawFileInfoTree[a][b],off.rawFileInfoTree[a][b].getRawFileContents(), off.rawFileInfoTree[a][b].getRawFileStartOffset(), off.rawFileInfoTree[a][b].getRawFileEndOffset());
    }

    private void saveRawData()
    {
        if (off == null || overFlow)
            throw new ProgramException("Error: Will Result In An Overflow If Saved!", "Unable To Save");
        off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileContents = textBox1.Text;
        replaceData(off.rawFileInfoTree[arrayIndex][nodeIndex],off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileContents(), off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileStartOffset(), off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileEndOffset());
    }

    internal void replaceData(RawFileData tree, string code, int startOffset, int endOffset)
    {
        byte[] codeBytes = Encoding.Default.GetBytes(code);
        int num = endOffset - startOffset;
        tree.rawFileFreeSpace = (tree.rawFileSize - tree.rawFileContents.Length);

        for (int a = 0; a < num; a++)
        {
            if (a < codeBytes.Length)
                off.ffBytes[startOffset + a] = codeBytes[a];
            else
                off.ffBytes[startOffset + a] = 0x00;
        }
    }

    //SAVE .FF Extract
    private void saveFFExtractToolStripMenuItem_Click(object sender, EventArgs e)
    {
        sff = new SaveFastFile(off);
        try
        {
            saveRawData();
            sff.saveExtract();
        }
        catch (ProgramException E)
        {
            MessageBox.Show(E.getError(), E.getTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        OpenFastFile.extractSaved = true;
    }

    //Save Fast File
    private void saveFastFile_Click(object sender, EventArgs e)
    {
        sff = new SaveFastFile(off);
        try
        {
            saveRawData();
            if (!off.isMultiplayer())
                sff.saveFile(off.getConsole(), false);
            else
                sff.saveMultiplayerFile(off.isXboxFF(), false);
        }
        catch (ProgramException E)
        {
            MessageBox.Show(E.getError(), E.getTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //Advanced File Save
    private void advFileSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        sff = new SaveFastFile(off);
        try
        {
            if (!off.isMultiplayer())
                sff.saveFile(off.getConsole(), true);
            else
                sff.saveMultiplayerFile(off.isXboxFF(), true);
        }
        catch (ProgramException E)
        {
            MessageBox.Show(E.getError(), E.getTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Form1_Close(object sender, FormClosedEventArgs e)
    {
        deleteDatFile();
    }

    private void deleteDatFile()
    {
        try
        {
            if (!OpenFastFile.extractSaved)
                File.Delete(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat");
        }
        catch (NullReferenceException)
        {
            return;
        }
    }

    //INJECT GSC,CFG,etc FOLDER
    private void folderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        fbd.Description = "Select Folder To Inject";
        dialogueOpen = true;
        if (fbd.ShowDialog() == DialogResult.OK)
        {
            string folder = fbd.SelectedPath;
            string[] files = Directory.GetFiles(fbd.SelectedPath);
            string overFlowFiles = "";
            string unknownFiles = "";
            string currentFile;
            List<Object[]> possibleFiles;
            int oIndex = 0, uIndex = 0;
            bool foundFile;
            for (int a = 0; a < files.Length; a++)
            {
                possibleFiles = getAllPossibleFiles(Path.GetFileName(files[a]));
                foundFile = false;
                for (int b = 0; b < off.rawFileInfoTree.Count; b++)
                {
                    for (int c = 0; c < off.rawFileInfoTree[b].Count; c++)
                    {
                        currentFile = off.rawFileInfoTree[b][c].getRawFileName();
                        if (possibleFiles.Count > 1)
                        {
                            chooseFile editor = new chooseFile(possibleFiles, Path.GetFileName(files[a]));
                            editor.ShowDialog(this);
                            currentFile = editor.getReplacedFileName();
                            b = editor.getNodeIndex();
                            c = editor.getArrayIndex();
                        }

                        if (currentFile.Contains(Path.GetFileName(files[a])) && currentFile[currentFile.Length - Path.GetFileName(files[a]).Length - 1] == '/')
                        {
                            foundFile = true;
                            byte[] code = File.ReadAllBytes(files[a]);
                            if (off.rawFileInfoTree[b][c].getRawFileSize() >= code.Length)
                                off.rawFileInfoTree[b][c].rawFileContents = Encoding.Default.GetString(code);
                            else
                            {
                                oIndex++;
                                if (oIndex < 6) overFlowFiles += "[ " + Path.GetFileName(files[a]) + " ]\n";
                            }
                            break;
                        }
                    }
                    if (foundFile) break;
                }
                if (!foundFile)
                {
                    uIndex++;
                    if (uIndex < 6) unknownFiles += "[ " + Path.GetFileName(files[a]) + " ]\n";
                }
            }
            textBox1.Text = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileContents();

            if (overFlowFiles != "")
            {
                if (oIndex > 5)
                    overFlowFiles += oIndex + " Matches (To Many To Show Here)\n";
                overFlowFiles += "\nThese Files Were To Large!";
            }
            if (unknownFiles != "")
            {
                if (overFlowFiles != "")
                    overFlowFiles += "\n\n";

                if (uIndex > 5)
                    unknownFiles += uIndex + " Matches (To Many To Show Here)\n";
                unknownFiles += "\nThese Files Are Unknown!";
            }

            if (overFlowFiles == "" && unknownFiles == "")
                MessageBox.Show("Folder Successfully Injected", "Full Folder Injected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Unable To Inject The Following: \n\n" + overFlowFiles + unknownFiles, "Full Folder Not Injected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        dialogueOpen = false;
        saveAllRawFiles();
    }

    private List<Object[]> getAllPossibleFiles(string file)
    {
        List<Object[]> possibleFiles = new List<Object[]>();
        for (int a = 0; a < off.rawFileInfoTree.Count; a++)
        {
            for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
            {
                if (off.rawFileInfoTree[a][b].getRawFileName().Contains(file) && off.rawFileInfoTree[a][b].getRawFileName()[off.rawFileInfoTree[a][b].getRawFileName().Length - file.Length - 1] == '/')
                {
                    possibleFiles.Add(new Object[] { off.rawFileInfoTree[a][b].getRawFileName(), a, b });
                }
            }
        }
        return possibleFiles;
    }

    //INJECT GSC,CFG,etc FILE
    private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        ofd = new OpenFileDialog();
        ofd.Title = "Inject A File";
        string badFile = "";
        dialogueOpen = true;
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            int updateIndex = -1;
            int firstIndex = -1;
            for (int a = 0; a < off.rawFileInfoTree.Count; a++)
            {
                for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
                {
                    if (off.rawFileInfoTree[a][b].getRawFileName().Contains(Path.GetFileName(ofd.FileName)) && off.rawFileInfoTree[a][b].getRawFileName()[off.rawFileInfoTree[a][b].getRawFileName().Length - Path.GetFileName(ofd.FileName).Length - 1] == '/')
                    {
                        firstIndex = a;
                        updateIndex = b;
                        break;
                    }
                }
                if (updateIndex >= 0) break;
            }
            if (updateIndex >= 0)
            {
                byte[] code = File.ReadAllBytes(ofd.FileName);
                if (off.rawFileInfoTree[firstIndex][updateIndex].getRawFileSize() >= code.Length)
                    off.rawFileInfoTree[firstIndex][updateIndex].rawFileContents = Encoding.Default.GetString(code);
                else
                    badFile += "[ " + Path.GetFileName(ofd.FileName) + " ]\n\nThis File Was To Large!";
            }
            else
                badFile += "[ " + Path.GetFileName(ofd.FileName) + " ]\n\nThis File Is Unknown!";
            textBox1.Text = off.rawFileInfoTree[arrayIndex][nodeIndex].getRawFileContents();

            if (badFile == "")
                MessageBox.Show("File Successfully Injected", "File Injected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Unable To Inject The Following: \n\n" + badFile, "File Not Injected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        dialogueOpen = false;
        saveAllRawFiles();
    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
        fbd = new FolderBrowserDialog();
        fbd.Description = "Select A Folder You Would Like To Extract All The Files To";
        dialogueOpen = true;
        if (fbd.ShowDialog() == DialogResult.OK)
        {
            string folder, fileName, fullFileName;
            for (int a = 0; a < off.rawFileInfoTree.Count; a++)
            {
                for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
                {
                    folder = fbd.SelectedPath;
                    fullFileName = off.rawFileInfoTree[a][b].getRawFileName();
                    fileName = fullFileName.Substring(fullFileName.LastIndexOf("/") + 1, fullFileName.Length - (fullFileName.LastIndexOf("/") + 1));
                    if (fullFileName.Contains("/"))
                        folder += "\\" + fullFileName.Substring(0, fullFileName.LastIndexOf("/"));
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    File.WriteAllText(folder + "\\" + fileName, off.rawFileInfoTree[a][b].getRawFileContents(), Encoding.Default);
                }
            }
            MessageBox.Show("All Files Successfully Extracted", "File Injected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        dialogueOpen = false;
    }

    private void removeAllCommentsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        removeComments(@"//.*|/\*(?s:.*?)\*/|/\#(.|\n)*?\#/", "", true);
        removeComments(@"^\s*$\n", "", true);
        saveAllRawFiles();
    }

    private void removeComments(string pattern, string replace, bool allFiles)
    {
        string contents, compressed;
        if (allFiles)
        {
            for (int a = 0; a < off.rawFileInfoTree.Count; a++)
            {
                for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
                {
                    if (off.rawFileInfoTree[a][b].rawFileName.Contains(".gsc") || off.rawFileInfoTree[a][b].rawFileName.Contains(".cfg") || off.rawFileInfoTree[a][b].rawFileName.Contains(".csc"))
                    {
                        contents = off.rawFileInfoTree[a][b].rawFileContents;
                        if (a == arrayIndex && b == nodeIndex)
                            contents = textBox1.Text;
                        compressed = Regex.Replace(contents, pattern, replace, RegexOptions.Multiline);

                        if (a == arrayIndex && b == nodeIndex)
                            textBox1.Text = compressed;

                        off.rawFileInfoTree[a][b].rawFileContents = compressed;
                        off.rawFileInfoTree[a][b].rawFileFreeSpace = (off.rawFileInfoTree[a][b].rawFileSize - off.rawFileInfoTree[a][b].rawFileContents.Length);
                    }
                }
            }
        }
        else
        {
            contents = textBox1.Text;
            compressed = Regex.Replace(contents, pattern, replace, RegexOptions.Multiline);
            textBox1.Text = compressed;
        }
    }

    private void compressAllCodeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        compressRawFile(true);
        saveAllRawFiles();
    }

    private void compressRawFile(bool allFiles)
    {
        removeComments(@"//.*|/\*(?s:.*?)\*/|/\#(.|\n)*?\#/", "", allFiles);
        removeComments(@"\*/", "", allFiles);

        removeComments(@"^\s*$\n", "", allFiles);
        removeComments(@"\s+", " ", allFiles);

        //,
        removeComments(@", ", ",", allFiles);
        removeComments(@" ,", ",", allFiles);

        //;
        removeComments(@"; ", ";", allFiles);

        //{}
        removeComments(@" \}", "}", allFiles);
        removeComments(@"\} ", "}", allFiles);
        removeComments(@" \{", "{", allFiles);
        removeComments(@"\{ ", "{", allFiles);

        //()
        removeComments(@"\( ", "(", allFiles);
        removeComments(@" \(", "(", allFiles);
        removeComments(@" \)", ")", allFiles);
        removeComments(@"\) ", ")", allFiles);

        //&&
        removeComments(@" &", "&", allFiles);
        removeComments(@"& ", "&", allFiles);

        //||
        removeComments(@" \|", "|", allFiles);
        removeComments(@"\| ", "|", allFiles);

        //[]
        removeComments(@"\[ ", "[", allFiles);
        removeComments(@"\ ]", "]", allFiles);

        //=
        removeComments(@" =", "=", allFiles);
        removeComments(@"= ", "=", allFiles);

        //-
        removeComments(@" -", "-", allFiles);
        removeComments(@"- ", "-", allFiles);

        //+
        removeComments(@" \+", "+", allFiles);
        removeComments(@"\+ ", "+", allFiles);

        //<
        removeComments(@" <", "<", allFiles);
        removeComments(@"< ", "<", allFiles);

        //>
        removeComments(@" >", ">", allFiles);
        removeComments(@"> ", ">", allFiles);

        //*
        removeComments(@" \*", "*", allFiles);
        removeComments(@"\* ", "*", allFiles);
    }

    //RESIZING GSC,CFG,ETC... FILE SIZE
    private void testToolStripMenuItem_Click(object sender, EventArgs e)
    {
        updateFileInfo();
        FFSizing resize = new FFSizing(off);
        resize.ShowDialog(this);
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        formSaveRawFile();
    }

    //File Information
    private void fastFileInfoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FFInfo info = new FFInfo(off);
        info.ShowDialog(this);
    }

    private void compressRawFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        compressRawFile(false);
    }

    private void stubCodeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string code = Regex.Replace(textBox1.Text, @"//.*|/\*(?s:.*?)\*/|/\#(.|\n)*?\#/", "", RegexOptions.Multiline);
        code = Regex.Replace(code, @"\*/", "", RegexOptions.Multiline);
        code = Regex.Replace(code, @"^\s*$\n", "", RegexOptions.Multiline);
        code = Regex.Replace(code, @"\s+", "", RegexOptions.Multiline);

        string newCode = "";
        int include = 0;
        int openBracket = 0;
        int parameters = 0;
        for (int a = 0; a < code.Length; a++)
        {
            if (code[a] == '#')
            {
                include++;
                continue;
            }
            if (include > 0)
            {
                if (code[a] == ';') include--;
                continue;
            }

            if (openBracket > 0)
            {
                if (code[a] == '{') openBracket++;
                if (code[a] == '}') openBracket--;
                if (openBracket == 0)
                {
                    newCode += code[a];
                    newCode += "\r\n";
                }
                continue;
            }

            if (code[a] != '\n' && code[a] != '\r')
            {
                if (code[a] == ')') parameters--;
                if (parameters == 0) newCode += code[a];
                if (code[a] == '{') openBracket++;
                if (code[a] == '(') parameters++;
            }
        }
        if (newCode.Length > 0)
            newCode = newCode.Remove(newCode.Length - 1, 1);
        textBox1.Text = newCode;
    }

    private void closeFastFile_Click(object sender, EventArgs e)
    {
        deleteDatFile();
        off.rawFileInfo.Clear();
        off.rawFileInfoTree.Clear();
        treeView1.Nodes.Clear();
        updateForm("Fast File Opener",false);
        textBox1.Clear();
        toolStripStatusLabel1.Text = "";
        toolStripStatusLabel2.Text = "";
        pictureBox1.Show();
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
    }

    public void updateForm(string title, bool value)
    {
        Text = title;
        saveFastFileToolStripMenuItem.Enabled = value;
        toolStripMenuItem1.Enabled = value;
        toolStripMenuItem2.Enabled = value;
        resizeFFToolStripMenuItem.Enabled = value;
        removeAllCommentsToolStripMenuItem.Enabled = value;
        compressAllCodeToolStripMenuItem.Enabled = value;
        fastFileInfoToolStripMenuItem.Enabled = value;
        closeFastFile.Enabled = value;
        OpenFastFile.fileOpened = value;
    }

    private void findNextStringButton(object sender, EventArgs e)
    {
        if(textBox1.Text != "" && textBox2.Text != "")
            findNextString(textBox2.Text);
    }

    private void findNextString(string key)
    {
        int index = 0;
        index = findNext(textBox1.Text, key, textBox1.SelectionStart + 1);
        if (index < 0) index = findNext(textBox1.Text, key, 0);
        if (index < 0) return;//nothing found, exit now
        textBox1.Select(index, key.Length);
        textBox1.ScrollToCaret();
        textBox1.Focus();
    }

    private int findNext(string code, string key, int start)
    {
        for (int a = start; a <= code.Length-key.Length; a++)
            if (code.Substring(a, key.Length).ToLower() == key.ToLower())
                return a;
        return -1;
    }

    private void findPreviousStringButton(object sender, EventArgs e)
    {
        if(textBox1.Text != "" && textBox2.Text != "")
            findPreviousString(textBox2.Text);
    }

    private void findPreviousString(string key)
    {
        int index = 0;
        index = findPrevious(textBox1.Text,key, textBox1.SelectionStart);
        if (index < 0) index = findPrevious(textBox1.Text, key, textBox1.TextLength);
        if (index < 0) return;//nothing found, exit now
        textBox1.Select(index, key.Length);
        textBox1.ScrollToCaret();
        textBox1.Focus();
    }

    private int findPrevious(string code,string key,int start)
    {
        for(int a=start;a>=key.Length;a--)
            if(code.Substring(a-key.Length,key.Length).ToLower() == key.ToLower())
                return a - key.Length;
        return -1;
    }

    private void pictureBox2_Click(object sender, MouseEventArgs e)
    {
        this.pictureBox2.Image = global::Fast_File_Opener.Properties.Resources.redCloseFind;
    }
    private void pictureBox2_Clicked(object sender, MouseEventArgs e)
    {
        this.pictureBox2.Image = global::Fast_File_Opener.Properties.Resources.closeFind;
        //this.tableLayoutPanel1.RowCount = 1;
        //tableLayoutPanel1.Controls.Remove(this.panel1);
        panel1.Visible = false;
        textBox1.Focus();
    }

    private void formatCode(object sender, EventArgs e)
    {
        compressRawFile(false);
        if (off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileName.Contains(".gsc") || off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileName.Contains(".cfg") || off.rawFileInfoTree[arrayIndex][nodeIndex].rawFileName.Contains(".csc"))
        {
            string formattedCode = prettyCode(textBox1.Text);
            textBox1.Text = formattedCode;
        }
    }

    private string prettyCode(string compressedCode)
    {
        int parenthesis = 0;
        int curlybraces = 0;
        int quotes = 0;
        int brackets = 0;
        int statement = 0;
        bool _else = false;
        bool xtra = false;
        int _case = 0;
        
        for (int a=0;a<compressedCode.Length;a++)
        {
            if (compressedCode[a] == '"')
            {
                quotes++;
                if (quotes > 1) quotes = 0;
            }
            if (quotes > 0) continue;

            if (a + 5 < compressedCode.Length)
                if (compressedCode.Substring(a, 5).ToLower() == "break")
                {
                    a += 5;
                    if(curlybraces == _case)
                        _case = 0;
                }

            if (a + 5 < compressedCode.Length)
                if (compressedCode.Substring(a, 5).ToLower() == "case ")
                {
                    a = compressedCode.IndexOf(':', a);
                    a++;
                    
                    compressedCode = compressedCode.Insert(a + 1, "\r\n");
                    for (int b = 0; b < curlybraces; b++)
                        compressedCode = compressedCode.Insert(a + 3, "\t");
                    compressedCode = compressedCode.Insert(a + 3, "\t");

                    _case = curlybraces;
                }

            if (a + 4 < compressedCode.Length)
                if (compressedCode.Substring(a, 4).ToLower() == "else")
                {
                    a += 4;
                    _else = true;
                }

            if(_else)
            {
                if(compressedCode[a] == '{')
                {
                    compressedCode = compressedCode.Insert(a, "\r\n");
                    if (_case > 0) compressedCode = compressedCode.Insert(a + 2, "\t");
                    for (int b = 0; b < curlybraces; b++)
                        compressedCode = compressedCode.Insert(a + 2, "\t");
                }
                else
                {
                    if (a + 4 < compressedCode.Length)
                        if (compressedCode.Substring(a+1, 3).ToLower() == "if(")
                        {
                            a += 4;
                            statement++;
                            parenthesis++;
                        }
                        else
                        {
                            compressedCode = compressedCode.Insert(a + 1, "\r\n");
                            if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                            for (int b = 0; b < curlybraces; b++)
                                compressedCode = compressedCode.Insert(a + 3, "\t");
                            compressedCode = compressedCode.Insert(a + 3, "\t");
                        }
                }
                _else = false;
            }

            if (a + 6 < compressedCode.Length)
                if (compressedCode.Substring(a, 6).ToLower() == "while(")
                {
                    a += 6;
                    statement++;
                    parenthesis++;
                }

            if (a + 4 < compressedCode.Length)
                if (compressedCode.Substring(a, 4).ToLower() == "for(")
                {
                    a += 4;
                    statement++;
                    parenthesis++;
                }

            if (a + 3 < compressedCode.Length)
                if (compressedCode.Substring(a, 3).ToLower() == "if(")
                {
                    a += 3;
                    statement++;
                    parenthesis++;
                }

            if (compressedCode[a] == '(') parenthesis++;
            else if (compressedCode[a] == ')') parenthesis--;
            if (compressedCode[a] == '{')
            {
                if(statement > 0) statement--;
                curlybraces++;
            }
            else if (compressedCode[a] == '}') curlybraces--;
            if (compressedCode[a] == '[') brackets++;
            else if (compressedCode[a] == ']') brackets--;

            if (parenthesis == 0 && brackets == 0)
            {
                if (compressedCode[a + 1] != '\r')
                {
                    if (compressedCode[a] == ')')
                    {
                        if(compressedCode[a + 1] != ';' && compressedCode[a + 1] != '[' && compressedCode[a + 1] != '+' && compressedCode[a + 1] != '-' && compressedCode[a + 1] != '/' && compressedCode[a + 1] != '*')
                        {
                            if (compressedCode[a + 1] == '{')
                            {
                                compressedCode = compressedCode.Insert(a + 1, "\r\n");
                                if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                                for (int b = 0; b < curlybraces; b++)
                                    compressedCode = compressedCode.Insert(a + 3, "\t");
                                if (statement > 1)
                                {
                                    xtra = true;
                                    for (int c = 0; c < statement - 1; c++)
                                        compressedCode = compressedCode.Insert(a + 3, "\t");
                                }
                            }
                            else
                            {
                                compressedCode = compressedCode.Insert(a + 1, "\r\n");
                                if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                                for (int b = 0; b < curlybraces; b++)
                                    compressedCode = compressedCode.Insert(a + 3, "\t");
                                for (int c = 0; c < statement; c++)
                                    compressedCode = compressedCode.Insert(a + 3, "\t");
                            }
                        }
                    }

                    if(compressedCode[a] == ';')
                    {
                        if(!xtra) statement = 0;
                        compressedCode = compressedCode.Insert(a + 1, "\r\n");
                        if (compressedCode[a + 3] != '}')
                        {
                            if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                            for (int b = 0; b < curlybraces; b++)
                                compressedCode = compressedCode.Insert(a + 3, "\t");
                            for (int c = 0; c < statement; c++)
                                compressedCode = compressedCode.Insert(a + 3, "\t");
                        }
                        else
                        {
                            for (int b = 0; b < curlybraces - 1; b++)
                                compressedCode = compressedCode.Insert(a + 3, "\t");
                            for (int c = 0; c < statement; c++)
                                compressedCode = compressedCode.Insert(a + 3, "\t");
                            if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                            xtra = false;
                        }
                    }
                }
            }
            if (compressedCode[a + 1] != '\r')
            {
                if (compressedCode[a] == '{')
                {
                    compressedCode = compressedCode.Insert(a + 1, "\r\n");
                    if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                    if (compressedCode[a + 3] != '}')
                        for (int b = 0; b < curlybraces; b++)
                            compressedCode = compressedCode.Insert(a + 3, "\t");
                    if(xtra)
                    {
                        for (int c = 0; c < statement; c++)
                            compressedCode = compressedCode.Insert(a + 3, "\t");
                    }
                }
                else if (compressedCode[a] == '}')
                {
                    compressedCode = compressedCode.Insert(a + 1, "\r\n");
                    if (compressedCode[a + 3] != '}')
                    {
                        for (int b = 0; b < curlybraces; b++)
                            compressedCode = compressedCode.Insert(a + 3, "\t");
                    }
                    else
                    {
                        for (int b = 0; b < curlybraces - 1; b++)
                            compressedCode = compressedCode.Insert(a + 3, "\t");
                    }
                    if (_case > 0) compressedCode = compressedCode.Insert(a + 3, "\t");
                }
            }
            if (a == compressedCode.Length - 2) break;
        }
        return compressedCode;
    }

	//RENAME RAW FILE FORM OPEN
	private void renameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        updateFileInfo();
        renameRawFile rename = new renameRawFile(off);
        rename.ShowDialog(this);
    }
}