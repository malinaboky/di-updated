namespace TagsCloudVisualization.WordPreprocessors;

public interface IWordPreprocessor
{
    public IEnumerable<Tuple<string, int>> ProcessTextToWords(string text);
}