using System.Text.Json;
using TagsCloudVisualization.MyStemWrapper;
using TagsCloudVisualization.WordPreprocessors.WordValidators;

namespace TagsCloudVisualization.WordPreprocessors;

public class DefaultWordPreprocessor : IWordPreprocessor
{
    private IWordValidator wordValidator;
    private MyStem myStem;

    public DefaultWordPreprocessor(IWordValidator wordValidator, MyStem myStem)
    {
        this.wordValidator = wordValidator;
        this.myStem = myStem;
    }
    
    public IEnumerable<Tuple<string, int>> ProcessTextToWords(string text)
    {
        var analysis = myStem.Analysis(text).Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var result = new Dictionary<string, int>();
        
        foreach (var wordInfo in analysis)
        {
            var dto = JsonSerializer.Deserialize<MyStemDto>(wordInfo);
            var word = dto.Analysis.First();
                
            if (!wordValidator.IsValid(word)) continue;
                
            if (!result.TryAdd(word.Lemma, 1))
                result[word.Lemma] += 1;
        }
        
        return result.Select(x => new Tuple<string, int>(x.Key, x.Value))
            .OrderByDescending(x => x.Item2);
    }
}