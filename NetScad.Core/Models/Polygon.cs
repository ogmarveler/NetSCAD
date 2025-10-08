using NetScad.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Polygon(List<List<double>> points, List<List<int>>? paths = null, int convexity = 1) : IScadObject
    {
        public List<List<double>> Points => points;
        public List<List<int>>? Paths => paths;
        public int Convexity => convexity;

        private string FormatList<T>(List<List<T>> list) => $"[{string.Join(", ", list.Select(inner => $"[{string.Join(", ", inner)}]"))}]";

        private string PathsPart => paths != null ? $", paths = {FormatList(paths)}" : "";

        public string OSCADMethod => $"polygon(points = {FormatList(points)}{PathsPart}, convexity = {convexity});";
    }
}
