using System.ComponentModel;

namespace NetScad.Core.Measurements
{
    public class Selector
    {
        public enum UnitSystem
        {
            [Description("Metric (mm)")]
            Metric,
            [Description("Imperial (in)")]
            Imperial
        }

        public enum BackgroundType
        {
            [Description("Light Theme")]
            Light,
            [Description("Dark Theme")]
            Dark
        }

    }
}
