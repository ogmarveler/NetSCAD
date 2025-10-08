using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Hull(params IScadObject[] children) : IScadObject
    {
        public IScadObject[] Children => children;

        public string OSCADMethod => $"hull() {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
