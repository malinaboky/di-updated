using System.Drawing;
using TagsCloudVisualization.Distributors;
using TagsCloudVisualization.Domain;
using TagsCloudVisualization.WordPreprocessors.FontCreators;

namespace TagsCloudVisualization.Layouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly ICloudDistribution distribution;
    private readonly IFontCreator fontCreator;
    
    public CircularCloudLayouter(ICloudDistribution distribution, IFontCreator fontCreator)
    {
        this.distribution = distribution;
        this.fontCreator = fontCreator;
    }
    
    public IEnumerable<Tag> CreateTagsCloud(IEnumerable<Tuple<string, int>> wordsCollection)
    {
        List<Tag> tags = [];
        using var graphic = Graphics.FromImage(new Bitmap(1, 1));
        foreach (var (word, wordCount) in wordsCollection)
        {
            var tagFont = fontCreator.CreateFont(wordCount);
            var rectangleSize = ConvertWordToRectangleSize(graphic, word, tagFont);
            var rectangle = GetNextRectangle(tags, rectangleSize);
            
            tags.Add(new Tag(rectangle, tagFont, word));
        }
        
        return tags;
    }
    
    private Rectangle GetNextRectangle(List<Tag> tags, Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("The rectangle size must be greater than zero.");
        
        var newRectangle = new Rectangle(distribution.GetNextPoint(), rectangleSize);
        
        while (tags.Any(r => r.Rectangle.IntersectsWith(newRectangle)))
            newRectangle.Location = distribution.GetNextPoint();
        
        return newRectangle;
    }

    private static Size ConvertWordToRectangleSize(Graphics graphic, string word, Font font)
    {
        var sizeF = graphic.MeasureString(word, font);
        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
}