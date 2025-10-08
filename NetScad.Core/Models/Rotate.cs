using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Rotate(double ax, double ay, double az, params IScadObject[] children) : IScadObject
    {
        public double AX => ax;
        public double AY => ay;
        public double AZ => az;
        public IScadObject[] Children => children;

        public string OSCADMethod => $"rotate([{ax}, {ay}, {az}]) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
