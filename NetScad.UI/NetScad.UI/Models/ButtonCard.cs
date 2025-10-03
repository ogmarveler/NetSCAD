using Avalonia.Media;

namespace NetScad.UI.Models
{
    
    public class ButtonCard
    {
        // Card Resource Inputs
        public int? CardHeight { get; set; } = 75;
        public int? Card2Width { get; set; } = 60;
        public int? Card2Height { get; set; } = 60;
        public int? CardWidth { get; set; } = 75;
        public string? PrimaryFontWeight { get; set; } = "SemiBold";
        public string? SecondaryFontWeight { get; set; } = "SemiBold";
        public int? PrimaryTextSize { get; set; } = 13;
        public int? SecondaryTextSize { get; set; } = 13;
        public int? IconHeight { get; set; } = 25;
        public int? IconWidth { get; set; } = 25;
        public int? Icon2Height { get; set; } = 45;
        public int? Icon2Width { get; set; } = 45;
        public int? SeparatorMargin { get; set; } = 0;
        public int? SeparatorPadding { get; set; } = 0;

        // Card Resource Variables
        public string? PrimaryText { get; set; }
        public string? SecondaryText { get; set; }
        public StreamGeometry? CardIcon { get; set; }
    }
}