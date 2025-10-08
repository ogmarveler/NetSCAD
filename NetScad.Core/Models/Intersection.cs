using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Intersection(params IScadObject[] children) : IScadObject
    {
        public IScadObject[] Children => children;

        public string OSCADMethod => $"intersection() {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
