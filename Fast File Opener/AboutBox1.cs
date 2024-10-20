using System.Windows.Forms;

partial class AboutBox1 : Form
{
    public AboutBox1()
    {
        InitializeComponent();
        this.labelVersion.Text = "Version: "+Application.ProductVersion;
    }
}
