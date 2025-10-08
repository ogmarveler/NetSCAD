using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Scale(double sx, double sy, double sz, params IScadObject[] children) : IScadObject
    {
        public double SX => sx;
        public double SY => sy;
        public double SZ => sz;
        public IScadObject[] Children => children;

        public string OSCADMethod => $"scale([{sx}, {sy}, {sz}]) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
