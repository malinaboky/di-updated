﻿using System.Drawing;
using TagsCloudVisualization.Distributors;
using TagsCloudVisualization.Domain;
using TagsCloudVisualization.Layouters.RectangleSizeCalculators;
using TagsCloudVisualization.WordPreprocessors.FontCreators;

namespace TagsCloudVisualization.Layouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly ICloudDistribution distribution;
    private readonly IFontCreator fontCreator;
    private readonly IRectangleSizeCalculator rectangleSizeCalculator;
    
    public CircularCloudLayouter(ICloudDistribution distribution, 
        IFontCreator fontCreator,
        IRectangleSizeCalculator rectangleSizeCalculator)
    {
        this.distribution = distribution;
        this.fontCreator = fontCreator;
        this.rectangleSizeCalculator = rectangleSizeCalculator;
    }
    
    public IEnumerable<Tag> CreateTagsCloud(IEnumerable<Tuple<string, int>> wordsCollection)
    {
        List<Tag> tags = [];
       
        foreach (var (word, wordCount) in wordsCollection)
        {
            var tagFont = fontCreator.CreateFont(wordCount);
            var rectangleSize = rectangleSizeCalculator.ConvertWordToRectangleSize(word, tagFont);
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
}