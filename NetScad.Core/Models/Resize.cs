using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Resize(double rx, double ry, double rz, bool auto = false, params IScadObject[] children) : IScadObject
    {
        public double RX => rx;
        public double RY => ry;
        public double RZ => rz;
        public bool Auto => auto;
        public IScadObject[] Children => children;

        public string OSCADMethod => $"resize([{rx}, {ry}, {rz}], auto = {auto.ToString().ToLower()}) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
