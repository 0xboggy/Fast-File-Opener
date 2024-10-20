using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class renameRawFile : Form
{
    private OpenFastFile off;
    private int maxFileNameSize;
    private int currentFileNameSize;
    private string oldFileName;
    private List<byte> ffBytes;
    public renameRawFile(OpenFastFile off)
    {
        InitializeComponent();
        this.off = off;
        ffBytes = off.ffBytes.ToList<byte>();
        fillComponents();
    }

    public void fillComponents()
    {
        comboBox1.SelectedIndex = 0;
        oldFileName = off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileName();
        textBox1.Text = oldFileName.Substring(0, oldFileName.LastIndexOf("."));
        updateLabels();
        textBox1.MaxLength = (maxFileNameSize-comboBox1.Text.Length);
    }

    private void updateLabels()
    {
        maxFileNameSize = off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileName().Length + off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileFreeSpace();
        currentFileNameSize = textBox1.Text.Length + comboBox1.Text.Length;
        currentSize.Text = "Current Size: " + currentFileNameSize;
        maxSize.Text = "Max Size: " + maxFileNameSize;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        updateLabels();
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if(!char.IsControl(e.KeyChar) && e.KeyChar.Equals('.'))
        {
            e.Handled = true;
        }
    }

    //Rename Raw File
    private void button1_Click(object sender, EventArgs e)
    {
        string newFileName = textBox1.Text + comboBox1.Text;
        int startOffset = off.getOffset(ffBytes.ToArray(), Encoding.Default.GetBytes(oldFileName), 0);
        int endOffset = startOffset + newFileName.Length + 1 + off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileContents().Length;

        //REMOVE THE CURRENT FILENAME FROM FILE
        ffBytes.RemoveRange(startOffset, oldFileName.Length);
        //WRITE NEW FILE NAME TO FILE
        ffBytes.InsertRange(startOffset, Encoding.Default.GetBytes(newFileName));

        if(newFileName.Length < oldFileName.Length)
        {
            int add = oldFileName.Length - newFileName.Length;
            byte[] addBytes = new Byte[add];
            for(int a = 0; a < addBytes.Length; a++)
                addBytes[a] = 0x00;
            ffBytes.InsertRange(endOffset,addBytes);
            FFSizing.fixHeaderSize(ffBytes, off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileSize() + add, (startOffset - 8), off.isBigEndian());

            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileFreeSpace += add;
            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileSize += add;
            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileStartOffset -= add;
        }
        else if(newFileName.Length > oldFileName.Length)
        {
            int removeBytes = newFileName.Length - oldFileName.Length;
            ffBytes.RemoveRange(endOffset,removeBytes);
            FFSizing.fixHeaderSize(ffBytes, off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileSize() - removeBytes, (startOffset - 8), off.isBigEndian());

            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileFreeSpace -= removeBytes;
            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileSize -= removeBytes;
            off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileStartOffset += removeBytes;
        }
        off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].rawFileName = newFileName;
        off.form.treeView1.Nodes[Form1.arrayIndex].Nodes[Form1.nodeIndex].Text = newFileName;
        off.ffBytes = ffBytes.ToArray();
        off.form.updateToolStrip();
        Close();
    }
}
