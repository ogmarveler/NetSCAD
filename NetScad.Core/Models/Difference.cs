using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Difference(params IScadObject[] children) : IScadObject
    {
        public IScadObject[] Children => children;

        public string OSCADMethod => $"difference() {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
