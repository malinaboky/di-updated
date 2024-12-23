using System.Drawing;
using TagsCloudVisualization.ConsoleCommands;

namespace TagsCloudVisualization.WordPreprocessors.FontCreators;

public class DefaultFontCreator : IFontCreator
{
    private readonly string fontName;
    private readonly int maxFontSize;
    private readonly int minFontSize;

    public DefaultFontCreator(Options options)
    {
        fontName = options.TagsFont;
        maxFontSize = options.MaxTagsFontSize;
        minFontSize = options.MinTagsFontSize;
    }
    
    public Font CreateFont(int fontSizeFactor)
    {
        var fontSize = Math.Min(Math.Max(minFontSize, fontSizeFactor), maxFontSize);

        return new Font(fontName, fontSize);
    }
}