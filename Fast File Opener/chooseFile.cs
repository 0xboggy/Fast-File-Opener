using System.Collections.Generic;
using System.Windows.Forms;

public partial class chooseFile : Form
{
    private string replaceFile;
    private int nodeIndex;
    private int arrayIndex;
    private List<object[]> possibleFiles;
    public chooseFile(List<object[]> possibleFiles,string fileName)
    {
        InitializeComponent();
        label4.Text = "File Name: "+fileName;
        this.possibleFiles = possibleFiles;
        for (int a = 0; a < possibleFiles.Count; a++)
            comboBox1.Items.Add(possibleFiles[a][0]);
        comboBox1.SelectedIndex = 0;
        replaceFile = (string)possibleFiles[comboBox1.SelectedIndex][0];
        nodeIndex = (int)possibleFiles[comboBox1.SelectedIndex][1];
        arrayIndex = (int)possibleFiles[comboBox1.SelectedIndex][2];
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
        replaceFile = (string)possibleFiles[comboBox1.SelectedIndex][0];
        nodeIndex = (int)possibleFiles[comboBox1.SelectedIndex][1];
        arrayIndex = (int)possibleFiles[comboBox1.SelectedIndex][2];
        Close();
    }

    public string getReplacedFileName() {return replaceFile;}
    public int getNodeIndex() {return nodeIndex;}
    public int getArrayIndex() {return arrayIndex;}
}
