using Autofac;

namespace TagsCloudVisualization.FileReaders;

public class FileReaderFactory
{
    private readonly IComponentContext context;
    
    public FileReaderFactory(IComponentContext context)
        => this.context = context;

    public IFileReader GetFileReader(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);
        
        var extension = Path.GetExtension(filePath).ToLower();

        if (context.IsRegisteredWithKey<IFileReader>(extension))
            return context.ResolveKeyed<IFileReader>(extension);

        throw new NotSupportedException($"File type {extension} is not supported.");
    }
}