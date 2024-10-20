using System.Windows.Forms;

public partial class FFInfo : Form
{
    private OpenFastFile off;
    public FFInfo(OpenFastFile off)
    {
        this.off = off;
        InitializeComponent();
        label1.Text = "File Name: " + off.fileName;
        label2.Text = "Game: " + off.game;
        label3.Text = "Console: " + off.getConsole();
        label4.Text = "Security: " + off.getFileSign();
        label5.Text = "Endian: " + off.getEndian();
        label6.Text = "File Size: " + off.ffBytes.Length + " Bytes";
    }
}