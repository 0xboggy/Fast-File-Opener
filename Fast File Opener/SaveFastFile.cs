using Ionic.Zlib;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

public class SaveFastFile
{
    private OpenFastFile off;
    public SaveFastFile(OpenFastFile off)
    {
        this.off = off;
    }

    internal void saveExtract()
    {
        FileStream stream;
        try
        {
            if(off.extractOpened)
                stream = new FileStream(off.fileLocation, FileMode.Truncate);
            else
                stream = new FileStream(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat", FileMode.Truncate);
        }
        catch(FileNotFoundException)
        {
            throw new ProgramException("Error: No File Found In Save Directory", "File Not Found");
        }

        stream.Write(off.ffBytes,0, off.ffBytes.Length);
        stream.Close();
        stream.Dispose();
        MessageBox.Show("Fast File Extract Successfully Saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    internal void saveFile(string console,bool advancedSave)
    {
        try
        {
            if (advancedSave)
                off.ffBytes = File.ReadAllBytes(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat");
            else
            {
                BinaryWriter overwriter = new BinaryWriter(new FileStream(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat", FileMode.Create), Encoding.Default);
                overwriter.Write(off.ffBytes, 0, off.ffBytes.Length);
                overwriter.Close();
            }
        }
        catch(FileNotFoundException)
        {
            throw new ProgramException("Error: No File Found In Save Directory", "File Not Found");
        }

        BinaryReader reader = new BinaryReader(new FileStream(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat", FileMode.Open), Encoding.Default);
        BinaryWriter writer = new BinaryWriter(new FileStream(off.fileLocation, FileMode.Create), Encoding.Default);
        writer.Write(off.fileBytes,0,12);

        Stream compressor;
        MemoryStream sm;
        if(console == "PS3")
        {
            reader.BaseStream.Position = 0L;
            while(true)
            {
                sm = new MemoryStream();
                compressor = new ZlibStream(sm, CompressionMode.Compress, CompressionLevel.Level1);
                using(compressor)
                {
                    compressor.Write(reader.ReadBytes(0x10000),0,0x10000);
                }
                byte[] compressedBytes = sm.ToArray();

                byte[] blockSize = BitConverter.GetBytes(Convert.ToInt32((int)(compressedBytes.Length-2)));
                Array.Reverse(blockSize);
                byte[] distance = new byte[compressedBytes.Length];
                Buffer.BlockCopy(blockSize, 2, distance, 0, 2);
                Buffer.BlockCopy(compressedBytes, 2, distance, 2, compressedBytes.Length-2);
                writer.Write(distance);

                if(reader.BaseStream.Position >= off.ffBytes.Length - 2)
                    break;
            }
            byte[] footer = new byte[2];
            footer[1] = 0x01;
            writer.Write(footer);
        }
        else
        {
            sm = new MemoryStream();
            //COD 4 attempt (pc)
            if(console == "PC" && off.game == off.supportedGames[1])
            {
                compressor = new ZlibStream(sm, CompressionMode.Compress, CompressionLevel.BestCompression);
            }
            else
            {
                if (console == "Xbox")
                    compressor = new ZlibStream(sm, CompressionMode.Compress, CompressionLevel.BestCompression);
                else
                    compressor = new ZlibStream(sm, CompressionMode.Compress, CompressionLevel.Level1);
            }
            using(compressor)
            {
                compressor.Write(off.ffBytes, 0, off.ffBytes.Length);
            }
            byte[] compressedBytes = sm.ToArray();

            writer.Write(compressedBytes);
            int missingBytes = 0;
            if(missingBytes > 0)
                missingBytes = off.fileBytes.Length - (compressedBytes.Length + 12);
            else
                missingBytes = 16 - ((compressedBytes.Length + 12) % 16);
            for (int a = 0; a < missingBytes; a++)
                writer.Write((byte)0x00);
            //writer.Write(off.fileBytes, (compressedBytes.Length + 12), off.fileBytes.Length - (compressedBytes.Length + 12));
        }
        reader.Close();
        writer.Close();

        if(advancedSave) off.addRawInfo();
        MessageBox.Show(off.getConsole()+" Fast File Successfully Saved", "File Saved", MessageBoxButtons.OK,MessageBoxIcon.Information);
    }

    internal void saveMultiplayerFile(bool xboxFF,bool advancedSave)
    {
        try
        {
            if(advancedSave)
                off.ffBytes = File.ReadAllBytes(off.fileLocation.Substring(0, off.fileLocation.LastIndexOf(".")) + "-extract.dat");
        }
        catch (FileNotFoundException)
        {
            throw new ProgramException("Error: No File Found In Save Directory", "File Not Found");
        }

        BinaryWriter writer = new BinaryWriter(new FileStream(off.fileLocation, FileMode.Create), Encoding.Default);

        //CREATE HEADER
        MemoryStream sm = new MemoryStream();
        //FileStream stream = new FileStream(off.fileLocation, FileMode.Create);
        Stream compressor = new ZlibStream(sm, CompressionMode.Compress, CompressionLevel.Level1);
        if(xboxFF)
        {
            int startOffset = off.multiplayerStartOffset;
            writer.Write(off.fileBytes, 0, startOffset);
            using (compressor)
            {
                compressor.Write(off.ffBytes, 0, off.ffBytes.Length);
            }
            byte[] compressedBytes = sm.ToArray();

            /*FileStream stream2 = new FileStream(off.fileLocation + "-compressed.dat", FileMode.Create);
            stream2.Write(compressedBytes, 0, compressedBytes.Length);
            stream2.Close();
            stream2.Dispose();*/

            for (int i = 0; i < off.multiplayerBlockRounds; i++)
            {
                writer.Write(compressedBytes, i * 0x200000, 0x200000);
                writer.Write(off.multiplayerSkippedBytes[i], 0, 0x2000);
            }
            writer.Write(compressedBytes, off.multiplayerBlockRounds * 0x200000, compressedBytes.Length - (off.multiplayerBlockRounds * 0x200000));
            
            //WRITE FOOTER
            int endOffset = off.getOffset(off.fileBytes, new byte[] { 0x24, 0x06, 0x0F, 0x0E, 0x0A },0);
            writer.Write(off.fileBytes, endOffset, off.fileBytes.Length - endOffset);
        }
        else
        {
            writer.Write(off.fileBytes, 0, 12);
            compressor.Write(off.ffBytes, 0, off.ffBytes.Length);
            byte[] compressedBytes = sm.ToArray();
            writer.Write(compressedBytes);

            //Found a pattern on all PC multiplayer files, footer has an 0xFF byte seperator
            int footerOffset = off.getOffset(off.fileBytes, new byte[] { 0xFF }, compressedBytes.Length-30);

            int endFooterOffset = off.fileBytes.Length-1;
            while(off.fileBytes[endFooterOffset] == 0x00)
                endFooterOffset--;
            endFooterOffset++;

            int footBytes = off.fileBytes.Length - footerOffset;
            int zeroBytes = off.fileBytes.Length - endFooterOffset;
            int writeBytes = footBytes - zeroBytes;
            writer.Write(off.fileBytes, footerOffset, writeBytes);

            // int missingBytes = 16 - ((compressedBytes.Length + 12 + writeBytes) % 16);
            int missingBytes = off.fileBytes.Length - (int)writer.BaseStream.Length;
            for(int a = 0; a < missingBytes; a++)
                writer.Write((byte)0x00);

            //writer.Write(off.fileBytes, (compressedBytes.Length + 12), off.fileBytes.Length - (compressedBytes.Length + 12));

        }
        writer.Close();
        writer.Dispose();
        if(advancedSave)
        {
            off.addRawInfo();
            off.form.textBox1.Text = off.rawFileInfoTree[Form1.arrayIndex][Form1.nodeIndex].getRawFileContents();
        }
        MessageBox.Show(off.getConsole()+" Multiplayer Fast File Successfully Saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}