using System.Text;
using NPOI.XWPF.UserModel;

namespace TagsCloudVisualization.FileReaders;

public class DocxFileReader : IFileReader
{
    public string Read(string filePath)
    {
        var text = new StringBuilder();
        using var doc = new XWPFDocument(File.OpenRead(filePath));
        
        foreach (var paragraph in doc.Paragraphs)
            text.Append(paragraph.Text);
        
        return text.ToString();
    }
}