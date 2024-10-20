using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class FFSizing : Form
{
    public int nodeIndex;
    public int arrayIndex;
    private OpenFastFile off;
    private int[] collectedfolderSize;
    private List<byte> ffBytes;
    private List<List<RawFileData>> rawFileInfoTree;//COPY
    private TreeNode oldNode = new TreeNode();
    public FFSizing(OpenFastFile off)
    {
        InitializeComponent();
        this.off = off;
        nodeIndex = 0;
        collectedfolderSize = new int[off.form.treeView1.GetNodeCount(false)];
        ffBytes = off.ffBytes.ToList<byte>();

        rawFileInfoTree = new List<List<RawFileData>>();
        fillComponents();
    }

    private void fillComponents()
    {
        List<RawFileData> root = new List<RawFileData>();
        for (int a=0;a<off.rawFileInfoTree.Count;a++)
        {
            root = new List<RawFileData>();
            for(int b=0;b<off.rawFileInfoTree[a].Count;b++)
                root.Add(new RawFileData(off.rawFileInfoTree[a][b].rawFileContents, off.rawFileInfoTree[a][b].rawFileName, off.rawFileInfoTree[a][b].rawFileSize, off.rawFileInfoTree[a][b].rawFileStartOffset, off.rawFileInfoTree[a][b].rawFileEndOffset, off.rawFileInfoTree[a][b].rawFileTextColor));
            rawFileInfoTree.Add(root);
        }

        for (int a = 0; a < collectedfolderSize.Length; a++)
            collectedfolderSize[a] = 0;
        off.form.treeView1.Focus();
        off.form.resetNodeColors();
        foreach (TreeNode node in off.form.treeView1.Nodes)
            treeView1.Nodes.Add((TreeNode)node.Clone());
        //treeView1.Nodes[0].Expand();
        //treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if(treeView1.SelectedNode.Parent != null || treeView1.SelectedNode.Nodes.Count == 0)
            oldNode.ForeColor = rawFileInfoTree[arrayIndex][nodeIndex].getRawFileTextColor();
        if (treeView1.SelectedNode.Nodes.Count == 0)
        {
            arrayIndex = treeView1.SelectedNode.Index;
            nodeIndex = 0;
            if (e.Node.Parent != null)
            {
                arrayIndex = e.Node.Parent.Index;
                nodeIndex = treeView1.SelectedNode.Index;
            }
            textBox1.Text = rawFileInfoTree[arrayIndex][nodeIndex].getRawFileFreeSpace().ToString();
            textBox2.Text = "0";
            updateLabels();

            if (treeView1.SelectedNode.Parent != null || treeView1.SelectedNode.Nodes.Count == 0)
            {
                oldNode.BackColor = System.Drawing.Color.White;
                oldNode = e.Node;
                e.Node.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
                e.Node.ForeColor = rawFileInfoTree[arrayIndex][nodeIndex].getRawFileTextColor();
            }
        }
    }

    private void treeView1_LostFocus(object sender, EventArgs e)
    {
        oldNode.ForeColor = System.Drawing.Color.Black;
        oldNode.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
    }

    private void updateLabels()
    {
        label1.Text = "Endian: " + off.getEndian();
        label2.Text = "Max File Size: " + rawFileInfoTree[arrayIndex][nodeIndex].getRawFileSize() + " Bytes";
        label3.Text = "Free Space: " + rawFileInfoTree[arrayIndex][nodeIndex].getRawFileFreeSpace() + " Bytes";
        label4.Text = "Collected Folder Size: " + collectedfolderSize[arrayIndex];
        rawFile.Text = "Raw File: " + rawFileInfoTree[arrayIndex][nodeIndex].getRawFileName();
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
        else
            textBox2.Text = "0";
    }

    private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
    {
        if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
        else
            textBox1.Text = "0";
    }

    private void button1_Click(object sender, EventArgs e)
    {
        int take = 0, add = 0;
        if (textBox1.Text.Length != 0)
            take = int.Parse(textBox1.Text);
        if (textBox2.Text.Length != 0)
            add = int.Parse(textBox2.Text);

        //TAKE FILE SIZE FROM RAW FILE
        if (add == 0)
        {
            if (rawFileInfoTree[arrayIndex][nodeIndex].getRawFileFreeSpace() - take >= 0)
            {
                collectedfolderSize[arrayIndex] += int.Parse(textBox1.Text);//TAKE SIZE FROM FILE
                rawFileInfoTree[arrayIndex][nodeIndex].rawFileSize -= take;
                rawFileInfoTree[arrayIndex][nodeIndex].rawFileFreeSpace -= take;
            }
        }
        else
        {
            //YOU CAN ADD THOSE BYTES
            if (add <= collectedfolderSize[arrayIndex])
            {
                collectedfolderSize[arrayIndex] -= int.Parse(textBox2.Text);//ADD SIZE TO FILE
                rawFileInfoTree[arrayIndex][nodeIndex].rawFileSize += add;
                rawFileInfoTree[arrayIndex][nodeIndex].rawFileFreeSpace += add;
            }
        }
        updateLabels();
        treeView1.Focus();//TEMP
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (!folderSizeEmpty())
            MessageBox.Show("Cannot Apply Changes Until Each Folder Size Is Empty", "Overflow Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        else
            saveNewFileSize();
    }

    private bool folderSizeEmpty()
    {
        for (int a = 0; a < collectedfolderSize.Length; a++)
            if (collectedfolderSize[a] > 0)
                return false;
        return true;
    }

    private void saveNewFileSize()
    {
        string fileName;
        int startOffset,endOffset;
        for (int a = 0; a < off.rawFileInfoTree.Count; a++)
        {
            for (int b = 0; b < off.rawFileInfoTree[a].Count; b++)
            {
                if (off.rawFileInfoTree[a][b].getRawFileFreeSpace() != rawFileInfoTree[a][b].getRawFileFreeSpace())
                {
                    fileName = rawFileInfoTree[a][b].getRawFileName();
                    startOffset = off.getOffset(ffBytes.ToArray(),Encoding.Default.GetBytes(fileName),0);
                    endOffset = (startOffset + (fileName.Length + 1) + off.rawFileInfoTree[a][b].getRawFileContents().Length);

                    ffBytes.RemoveRange(endOffset, off.rawFileInfoTree[a][b].getRawFileFreeSpace());
                    //INSERT SPACE WHERE IT WAS REMOVED IF THERE IS ANY
                    if (rawFileInfoTree[a][b].getRawFileFreeSpace() > 0)
                    {
                        byte[] freeSpace = new byte[rawFileInfoTree[a][b].getRawFileFreeSpace()];
                        for (int i = 0; i < freeSpace.Length; i++)
                            freeSpace[i] = 0x00;
                        ffBytes.InsertRange(endOffset, freeSpace);
                    }
                    fixHeaderSize(ffBytes,rawFileInfoTree[a][b].getRawFileSize(),(startOffset-8),off.isBigEndian());
                }
            }
        }

        

        /*for (int a = 0; a < rawFileInfoTree.Count; a++)
        {
            for (int b = 0; b < rawFileInfoTree[a].Count; b++)
            {
                fileName = off.rawFileInfoTree[a][b].getRawFileName();
                startOffset = off.getOffset(ffBytes.ToArray(), Encoding.Default.GetBytes(fileName), 0);
                startOffset += fileName.Length+1;
                endOffset = startOffset + rawFileInfoTree[a][b].rawFileSize;

                off.rawFileInfoTree[a][b].rawFileStartOffset = startOffset;
                off.rawFileInfoTree[a][b].rawFileEndOffset = endOffset;
                off.rawFileInfoTree[a][b].rawFileSize = rawFileInfoTree[a][b].rawFileSize;
                off.rawFileInfoTree[a][b].rawFileFreeSpace = rawFileInfoTree[a][b].rawFileFreeSpace;
            }
        }*/
        off.ffBytes = ffBytes.ToArray();
        off.form.updateToolStrip();
        off.addRawInfo();

        /*byte[] compressedBytes = ffBytes.ToArray();
        FileStream stream2 = new FileStream(off.fileLocation + "-etstttts.dat", FileMode.Create);
        stream2.Write(compressedBytes, 0, compressedBytes.Length);
        stream2.Close();
        stream2.Dispose();*/
        
        Close();
    }

    //FIX SIZE HEADER
    public static void fixHeaderSize(List<byte> allBytes,int fileSize,int startOffset,bool bigEndian)
    {
        allBytes.RemoveRange(startOffset, 4);
        byte[] sizeHeader = new byte[4];
        byte[] hexBytes = BitConverter.GetBytes(fileSize);
        for (int i = 0; i < 4; i++)
        {
            if (i < hexBytes.Length)
                sizeHeader[i] = hexBytes[i];
            else
                sizeHeader[i] = 0x00;
        }
        if(bigEndian)
            Array.Reverse(sizeHeader);
        allBytes.InsertRange(startOffset, sizeHeader);
    }
}