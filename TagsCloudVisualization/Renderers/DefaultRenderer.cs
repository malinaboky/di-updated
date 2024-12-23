using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.ConsoleCommands;
using TagsCloudVisualization.Domain;
using TagsCloudVisualization.Renderers.ColorGenerators;

namespace TagsCloudVisualization.Renderers;

public class DefaultRenderer(ColorGeneratorFactory colorGeneratorFactory, Options options) : ICloudRenderer
{
    private readonly IColorGenerator colorGenerator = colorGeneratorFactory.GetColorGenerator(options.ColorOption);
    private readonly string outputDirectory = options.OutputDirectory;
    private readonly Size imageSize = new(options.ImageWidth, options.ImageHeight);
    private readonly Color backgroundColor = Color.FromName(options.BackgroundColor);

    public void Render(IEnumerable<Tag> tags)
    {
        if (!tags.Any())
            throw new ArgumentException("The cloud layout is empty");
        
        using var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphic = Graphics.FromImage(bitmap);
        graphic.Clear(backgroundColor);
        foreach (var tag in tags)
        {
            var color = colorGenerator.GetColor();
            var brush = new SolidBrush(color);
            graphic.DrawString(tag.Content, tag.Font, brush, tag.Rectangle.Location);
        }
        
        SaveImage(bitmap, tags.Count());
    }

    private void SaveImage(Bitmap bitmap, int tagCount)
    {
        var filePath = Path.Combine(outputDirectory, $"cloud_{tagCount}.png");
        bitmap.Save(filePath, ImageFormat.Png);
    }
}