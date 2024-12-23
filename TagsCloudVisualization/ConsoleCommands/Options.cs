using CommandLine;
using TagsCloudVisualization.Enums;

namespace TagsCloudVisualization.ConsoleCommands;

public class Options
{
    [Option('i', "inputFilePath", Required = true, 
        HelpText = "Set path to a file containing words in one column (under one word per row).")]
    public string InputFilePath { get; set; }

    [Option('o', "outputDirectory", Required = true, 
        HelpText = "Set directory for output image.")]
    public string OutputDirectory { get; set; }

    [Option('f', "font", Default = "Arial", HelpText = "Set font for tags cloud words.")]
    public string TagsFont { get; set; }
    
    [Option("minFontSize", Default = 5, HelpText = "Set min font size for tags cloud words.")]
    public int MinTagsFontSize { get; set; }
    
    [Option("maxFontSize", Default = 80, HelpText = "Set max font size for tags cloud words.")]
    public int MaxTagsFontSize { get; set; }

    [Option('h', "imageHeight", Default = 1080, HelpText = "Set output image height.")]
    public int ImageHeight { get; set; }
    
    [Option('w', "imageWidth", Default = 1920, HelpText = "Set output image width.")]
    public int ImageWidth { get; set; }

    [Option('b', "backgroundColor", Default = "Empty", 
        HelpText = "Set background color for tags cloud. Example : -b white")]
    public string BackgroundColor { get; set; }
    
    [Option("pathToMyStem", Default = null, HelpText = "Set path to mystem.exe.")]
    public string PathToMyStem { get; set; }
    
    [Option("numOfColors", Default = 85, HelpText = "Set number of colors for gradient color generator.")]
    public int NumOfColors { get; set; }
    
    [Option("colorOption", Default = ColorOption.Random, 
        HelpText = "Set option of color generator for words. Possible values: Random, Gradient.")]
    public ColorOption ColorOption { get; set; }
}