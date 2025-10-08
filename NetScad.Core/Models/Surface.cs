using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Surface(string file, bool center = false, int convexity = 1) : IScadObject
    {
        public string File => file;
        public bool Center => center;
        public int Convexity => convexity;

        public string OSCADMethod => $"surface(file = \"{file}\"{(center ? $", center = {center.ToString().ToLower()}" : "")}, convexity = {convexity});";
    }
}
