using System;
using System.Windows.Forms;

public static class Program
{
    public static Form1 window = new Form1();
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(window);
    }
}