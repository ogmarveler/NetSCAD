using NetScad.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class Polyhedron(List<List<double>> points, List<List<int>> faces, int convexity = 1) : IScadObject
    {
        public List<List<double>> Points => points;
        public List<List<int>> Faces => faces;
        public int Convexity => convexity;

        private string FormatList<T>(List<List<T>> list) => $"[{string.Join(", ", list.Select(inner => $"[{string.Join(", ", inner)}]"))}]";

        public string OSCADMethod => $"polyhedron(points = {FormatList(points)}, faces = {FormatList(faces)}, convexity = {convexity});";
    }
}
