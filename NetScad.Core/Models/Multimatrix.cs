using NetScad.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Multmatrix(List<List<double>> matrix, params IScadObject[] children) : IScadObject
    {
        public List<List<double>> Matrix => matrix;
        public IScadObject[] Children => children;

        private string FormatMatrix => $"[{string.Join(", ", matrix.Select(row => $"[{string.Join(", ", row)}]"))}]";

        public string OSCADMethod => $"multmatrix({FormatMatrix}) {{ {string.Join("\n", children.Select(c => c.OSCADMethod))} }};";
    }
}
