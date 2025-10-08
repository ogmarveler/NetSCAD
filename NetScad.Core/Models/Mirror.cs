using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Mirror(double mx, double my, double mz, params IScadObject[] children) : IScadObject
    {
        public double MX => mx;
        public double MY => my;
        public double MZ => mz;
        public IScadObject[] Children => children;

        public string OSCADMethod => $"mirror([{mx}, {my}, {mz}]) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
