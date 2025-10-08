using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Translate(double x, double y, double z, params IScadObject[] children) : IScadObject
    {
        public double X => x;
        public double Y => y;
        public double Z => z;
        public IScadObject[] Children => children;

        public string OSCADMethod => $"translate([{x}, {y}, {z}]) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
