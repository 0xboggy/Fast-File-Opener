public class RawFileData
{
    public int rawFileStartOffset = 0;
    public int rawFileEndOffset = 0;
    public string rawFileContents;
    public string rawFileName;
    public int rawFileSize = 0;
    public System.Drawing.Color rawFileTextColor;
    public int rawFileFreeSpace;

    public RawFileData(string contents,string fileName,int fileSize,int startOffset,int endOffset,System.Drawing.Color color)
    {
        rawFileName = fileName;
        rawFileSize = fileSize;
        rawFileContents = contents;
        rawFileStartOffset = startOffset;
        rawFileEndOffset = endOffset;
        rawFileTextColor = color;
        rawFileFreeSpace = (fileSize-contents.Length);
    }

    public string getRawFileContents() { return rawFileContents; }
    public string getRawFileName() { return rawFileName; }
    public int getRawFileSize() { return rawFileSize; }
    public int getRawFileStartOffset() { return rawFileStartOffset; }
    public int getRawFileEndOffset() { return rawFileEndOffset; }
    public System.Drawing.Color getRawFileTextColor() { return rawFileTextColor; }
    public int getRawFileFreeSpace() { return rawFileFreeSpace;  }
}