using TagsCloudVisualization.Domain;

namespace TagsCloudVisualization.Layouters;

public interface ICloudLayouter
{
    public IEnumerable<Tag> CreateTagsCloud(IEnumerable<Tuple<string, int>> wordsCollection);
}