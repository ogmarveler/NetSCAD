using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Minkowski(params IScadObject[] children) : IScadObject
    {
        public IScadObject[] Children => children;

        public string OSCADMethod => $"minkowski() {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
