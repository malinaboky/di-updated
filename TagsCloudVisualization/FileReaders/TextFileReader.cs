namespace TagsCloudVisualization.FileReaders;

public class TextFileReader : IFileReader
{
    public string Read(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}