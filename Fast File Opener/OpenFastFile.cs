using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using Ionic.Zlib;

public class OpenFastFile
{
    public Form1 form = Program.window;
    public string fileName;
    public string fileDirectory;
    public string fileLocation;
    public int startOffset;

    public byte[] ffBytes;
    public byte[] fileBytes;
    public static string currentOpenFFextracted = "";
    public static bool fileOpened = false;
    public static bool extractSaved = false;

    //PS3 Information
    public List<Byte[]> sizeBlocks;

    //Multiplayer Information (XBOX)
    public bool isMulti;
    public int multiplayerStartOffset;
    public int multiplayerEndOffset;
    public int multiplayerBlockRounds;
    public byte[][] multiplayerSkippedBytes;
    public byte[][] extensionArray;
    public Color[] extensionColor;
    public string[] supportedGames;

    public string game;
    public string console = "Unknown";
    public string endian = "Big Endian";
    public string sign = "Unsigned";
    public bool signed = false;
    public bool littleEndian = false;
    public bool extractOpened = false;
    public List<RawFileData> rawFileInfo;
    public List< List<RawFileData> > rawFileInfoTree;

    public OpenFastFile()
    {
        rawFileInfo = new List<RawFileData>();
        rawFileInfoTree = new List< List<RawFileData> >();
        isMulti = false;
        multiplayerSkippedBytes = null;
        sizeBlocks = new List<Byte[]>();
        extensionArray = new byte[9][] { new byte[] { 0x2E, 0x67, 0x73, 0x63, 0x00 },//.gsc
                                         new byte[] { 0x2E, 0x63, 0x73, 0x63, 0x00 },//.csc
                                         new byte[] { 0x2E, 0x61, 0x74, 0x72, 0x00 },//.atr
                                         new byte[] { 0x2E, 0x72, 0x6d, 0x62, 0x00 },//.rmb
                                         new byte[] { 0x2E, 0x63, 0x66, 0x67, 0x00 },//.cfg
                                         new byte[] { 0x2E, 0x67, 0x72, 0x61, 0x70, 0x68, 0x00 },//.graph
                                         new byte[] { 0x2E, 0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x00 },//.script
                                         new byte[] { 0x2E, 0x73, 0x68, 0x6F, 0x63, 0x6B, 0x00 },//.shock
                                         new byte[] { 0x2E, 0x76, 0x69, 0x73, 0x69, 0x6F, 0x6E, 0x00 }};//.vision

        extensionColor = new Color[] { Color.DarkRed,
                                       Color.DarkOrange,
                                       Color.DarkGreen,
                                       Color.Gold,
                                       Color.Purple,
                                       Color.SaddleBrown,
                                       Color.Purple,
                                       Color.Blue,
                                       Color.DeepSkyBlue};

        supportedGames = new string[] { "Call of Duty: World at War", "Call of Duty: Modern Warefare", "Call of Duty: Modern Warefare 2" };
        game = supportedGames[0];
    }

    internal void setFileName(string name) {fileName = name; if(name.Contains("_mp")) isMulti = true;}
    internal void setFileLocation(string location) {fileLocation = location;}
    internal void setFileDirectory(string directory) {fileDirectory = directory;}

    internal void openFile(Form1 form, bool datExtract)
    {
        form.Text = "Fast File Opener";
        ffBytes = File.ReadAllBytes(fileLocation);
        fileBytes = (byte[])ffBytes.Clone();


        if(!datExtract) setConsole();
        setEndian(datExtract);
        setFileSign();
        endian = getEndian();
        signed = isSigned();

        //EXTRACT THE FAST FILE
        if(!datExtract) extractFastFile();
        addRawInfo();

        form.updateForm("Fast File Opener [" + fileLocation + "]",true);
        form.pictureBox1.Hide();
    }

    internal void addRawInfo()
    {
        rawFileInfo.Clear();
        rawFileInfoTree.Clear();
        form.treeView1.Nodes.Clear();
        addRawFile(ffBytes, extensionArray, extensionColor);
        sortRawFile();
        string currentRoot = "";
        string oldRoot = "";
        string fileName;
        TreeNode root = new TreeNode(currentRoot);
        List<RawFileData> temp = new List<RawFileData>();
        for (int a = 0; a < rawFileInfo.Count; a++)
        {
            fileName = rawFileInfo[a].getRawFileName();
            if(fileName.Contains("/"))
                currentRoot = fileName.Substring(0, fileName.LastIndexOf("/"));
            else
                currentRoot = "misc.";

            if(oldRoot != currentRoot)
            {
                if(a > 0)
                {
                    temp = temp.OrderBy(o => o.getRawFileName()).ToList();
                    for(int b = 0;b < temp.Count;b++)
                    {
                        root.Nodes.Add(temp[b].getRawFileName());
                        root.Nodes[b].ForeColor = temp[b].getRawFileTextColor();
                    }
                    rawFileInfoTree.Add(temp);
                    temp = new List<RawFileData>();
                }
                oldRoot = currentRoot;
                root = new TreeNode(currentRoot);
                form.treeView1.Nodes.Add(root);
            }
            temp.Add(rawFileInfo[a]);
        }

        temp = temp.OrderBy(o => o.getRawFileName()).ToList();
        for (int b = 0; b < temp.Count; b++)
        {
            root.Nodes.Add(temp[b].getRawFileName());
            root.Nodes[b].ForeColor = temp[b].getRawFileTextColor();
        }
        rawFileInfoTree.Add(temp);
        form.treeView1.Focus();
        /*if (form.treeView1.Nodes.Count > 0)
        {
            form.treeView1.Nodes[Form1.arrayIndex].Expand();
            form.treeView1.SelectedNode = form.treeView1.Nodes[Form1.arrayIndex].Nodes[Form1.nodeIndex];
        }*/
    }

    private void sortRawFile()
    {
        rawFileInfo = rawFileInfo.OrderByDescending(o => o.getRawFileName().Contains("/")).ToList();
  
        int smallestFileIndex;
        string n1, n2;
        int l1, l2;
        for(int a=0;a<rawFileInfo.Count;a++)
        {
            smallestFileIndex = a;
            n1 = rawFileInfo[smallestFileIndex].getRawFileName();
            if(!n1.Contains("/")) continue;
            for (int b=a;b<rawFileInfo.Count;b++)
            {
                n2 = rawFileInfo[b].getRawFileName();
                l1 = n1.LastIndexOf("/");
                l2 = n2.LastIndexOf("/");
                if (!n1.Contains("/")) continue; // l1 = n1.Length;
                if (!n2.Contains("/")) continue; // l2 = n2.Length;
                if (string.Compare(n1.Substring(0,l1), n2.Substring(0,l2)) == 1)
                {
                    smallestFileIndex = b;
                    n1 = rawFileInfo[smallestFileIndex].getRawFileName();
                }
            }
            RawFileData temp = rawFileInfo[a];
            rawFileInfo[a] = rawFileInfo[smallestFileIndex];
            rawFileInfo[smallestFileIndex] = temp;
        }
    }

    internal void addRawFile(byte[] array, byte[][] extension,Color[] color)
    {
        int fileSize, i = 0;
        while (i >= 0 && i < array.Length)
        {
            for (int a = 0; a < extension.Length; a++)
            {
                byte[] segment = new byte[extension[a].Length];
                Buffer.BlockCopy(array, i, segment, 0, extension[a].Length);
                if(segment.SequenceEqual<byte>(extension[a]))
                {
                    fileSize = getFileSize(i);
                    rawFileInfo.Add(new RawFileData(getFileContents(i + (extension[a].Length)), getFileName(i, extension[a].Length - 1), fileSize, startOffset, startOffset + fileSize, color[a]));
                    i += fileSize+4;
                }
            }
            i = Array.IndexOf<byte>(array, extension[0][0], i + 1);//extension.Length
        }
    }

    internal string getFileContents(int offset)
    {
        startOffset = offset;
        int savedOffset = offset;
        while(ffBytes[offset] != 0x00)
            offset++;
        byte[] code = new byte[offset - savedOffset];
        int count = 0;
        for (int a = savedOffset; a < offset; a++)
        {
            code[count] = ffBytes[a];
            count++;
        }
        string fCode = Encoding.Default.GetString(code);
        return fCode;
    }

    internal string getFileName(int offset,int extension)
    {
        int savedOffset = offset;
        while(ffBytes[offset] != 0xff)
            offset--;
        offset++;
        byte[] name = new byte[savedOffset+extension - offset];
        int count = 0;
        for (int a = offset; a < savedOffset+extension; a++)
        {
            name[count] = ffBytes[a];
            count++;
        }
        string fName = Encoding.Default.GetString(name);
        return fName;
    }

    internal int getFileSize(int offset)
    {
        while (ffBytes[offset] != 0xff)
            offset--;
        offset -= 7;

        byte[] size = new byte[4];
        for(int a = 0; a < 4; a++)
            size[a] = ffBytes[offset+a];
        if(isBigEndian()) Array.Reverse(size);
        int fSize = BitConverter.ToInt32(size, 0);
        return fSize;
    }

    private void extractFastFile()
    {
        FileStream stream = new FileStream(fileLocation.Substring(0, fileLocation.LastIndexOf(".")) + "-extract.dat", FileMode.Create);
        if(console != "PS3")
        {
            byte[] compressed;
            if(!isMultiplayer() || isLittleEndian())
            {
                StreamWriter writer = new StreamWriter(stream);
                compressed = File.ReadAllBytes(fileLocation);
                
                byte[] bytes = new byte[] {0x78};
                if(game == supportedGames[2])
                    bytes = new byte[] {0x78, 0xDA};

                int offset = getOffset(compressed, bytes, 0);

                Array.ConstrainedCopy(compressed, offset, compressed, 0, compressed.Length - offset);
            }
            else
                compressed = RemoveHashBlocks();

            ffBytes = ZlibStream.UncompressBuffer(compressed);
        }
        else
            ffBytes = PS3Uncompress();

        stream.Write(ffBytes, 0, ffBytes.Length);
        stream.Close();
        stream.Dispose();
    }

    private byte[] PS3Uncompress()
    {
        byte[] buffer = File.ReadAllBytes(fileLocation);
        BinaryReader reader = new BinaryReader(new FileStream(fileLocation, FileMode.Open), Encoding.Default);
        BinaryWriter writer = new BinaryWriter(new FileStream(fileLocation + "-no-block.dat", FileMode.Create), Encoding.Default);
        reader.BaseStream.Position = 12L;
        byte[] skip;
        while (true)
        {
            skip = reader.ReadBytes(2);
            int count = int.Parse(BitConverter.ToString(skip).Replace("-", ""),NumberStyles.AllowHexSpecifier);
            byte[] blockBytes = DeflateStream.UncompressBuffer(reader.ReadBytes(count));
            sizeBlocks.Add(skip);
            writer.Write(blockBytes);
            if(reader.BaseStream.Position >= buffer.Length-2)
                break;
        }
        reader.Close(); writer.Close();
        byte[] buffer3 = File.ReadAllBytes(fileLocation + "-no-block.dat");
        File.Delete(fileLocation + "-no-block.dat");
        return buffer3;
    }

    private byte[] RemoveHashBlocks()
    {
        byte[] buffer = File.ReadAllBytes(fileLocation);
        FileStream stream2 = new FileStream(fileLocation + "-no-block.dat", FileMode.Create);
        FileStream input = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
        BinaryReader reader = new BinaryReader(input);

        multiplayerStartOffset = getOffset(buffer, new byte[]{0x78, 0x01, 0xEC},0);
        byte[] buffer2 = reader.ReadBytes(multiplayerStartOffset);

        multiplayerBlockRounds = (int)Math.Round(Math.Floor((double)(((double)(reader.BaseStream.Length - multiplayerStartOffset)) / 2105344.0)));

        multiplayerSkippedBytes = new byte[multiplayerBlockRounds][];
        multiplayerEndOffset = multiplayerStartOffset;

        for (int i = 0; i < multiplayerBlockRounds; i++)
        {
            stream2.Write(reader.ReadBytes(0x200000), 0, 0x200000);
            multiplayerEndOffset += 0x200000;
            multiplayerSkippedBytes[i] = reader.ReadBytes(0x2000);
            multiplayerEndOffset += 0x2000;
        }
        stream2.Write(buffer, multiplayerEndOffset, buffer.Length - multiplayerEndOffset);
        stream2.Close();
        input.Close();
        input.Dispose();
        stream2.Dispose();
        byte[] buffer3 = File.ReadAllBytes(fileLocation + "-no-block.dat");
        File.Delete(fileLocation + "-no-block.dat");
        return buffer3;
    }

    public int getOffset(byte[] array, byte[] extension, int startOffset)
    {
        int i = startOffset;
        while (i >= 0 && i <= array.Length - extension.Length)
        {
            byte[] segment = new byte[extension.Length];
            Buffer.BlockCopy(array, i, segment, 0, extension.Length);
            if (segment.SequenceEqual<byte>(extension))
                return i;
            i = Array.IndexOf<byte>(array, extension[0], i + 1);
        }
        return -1;
    }

    public bool isXboxFF()
    {
        if (isBigEndian())
            return true;
        return false;
    }

    public bool isMultiplayer()
    {
        if(isMulti)
            return true;
        return false;
    }

    public string getConsole()
    {
        return console;
    }

    public void setConsole()
    {
        console = checkConsole();
    }

    private string checkConsole()
    {
        List<byte> bytes = ffBytes.ToList();

        if (ffBytes[12] == 0x78 && ffBytes[8] != 0x00)
        {
            if(ffBytes[8] != 0x83)
                setGameFile(supportedGames[1]);
            return "PC";
        }

        if (Encoding.Default.GetString(bytes.GetRange(0, 8).ToArray()) == "IWffu100")
        {
            setGameFile(supportedGames[2]);
            return "Xbox";
        }

        if(ffBytes[12] == 0x78 && ffBytes[8] == 0x00 || ffBytes[0x25] == 0x78)
            return "Xbox";

        return "PS3";
    }

    public void setEndian(bool extract)
    {
        if(ffBytes[8 + 36*Convert.ToInt32(extract)] != 0x00)
            littleEndian = true;
        else
            littleEndian = false;
    }

    public bool isLittleEndian()
    {
        return littleEndian;
    }

    public bool isBigEndian()
    {
        return !littleEndian;
    }

    public string getEndian()
    {
        if(isLittleEndian())
            return "Little Endian";
        return "Big Endian";
    }

    public void setFileSign()
    {
        BinaryReader reader = new BinaryReader(new FileStream(fileLocation, FileMode.Open), Encoding.Default);
        string security = new string(reader.ReadChars(8));
        reader.Close();
        reader.Dispose();
        if (security == "IWffu100")
            sign = "Unsigned";
        else
        {
            isMulti = true;
            sign = "Signed";
        }
    }

    public string getFileSign()
    {
        return sign;
    }

    public bool isSigned()
    {
        if(sign == "Signed")
            return true;
        return false;
    }

    private void setGameFile(string game)
    {
        this.game = game;
    }

    public string getGameFile()
    {
        return game;
    }
}