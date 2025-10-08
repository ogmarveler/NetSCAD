using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Text(string text, double size = 10, string? font = null, string halign = "left", string valign = "baseline", double spacing = 1, string direction = "ltr", string? language = null, string? script = null, double resolution = 100) : IScadObject
    {
        public string TextValue => text;
        public double Size => size;
        public string? Font => font;
        public string HAlign => halign;
        public string VAlign => valign;
        public double Spacing => spacing;
        public string Direction => direction;
        public string? Language => language;
        public string? Script => script;
        public double Resolution => resolution;

        private string OptionalParam(string name, string? value) => value != null ? $", {name} = \"{value}\"" : "";

        public string OSCADMethod => $"text(text = \"{text}\", size = {size}{OptionalParam("font", font)}, halign = \"{halign}\", valign = \"{valign}\", spacing = {spacing}, direction = \"{direction}\"{OptionalParam("language", language)}{OptionalParam("script", script)}, $fn = {resolution});";
    }
}
