using NetScad.Core.Interfaces;
using System.Collections.Generic;

namespace NetScad.Core.Models
{
    public partial class RoundedPolyhedron(List<List<double>> points, List<List<int>> faces, double round_r, double round_h = 0.001, double resolution = 200) : IScadObject
    {
        public List<List<double>> Points => points;
        public List<List<int>> Faces => faces;
        public double RoundRadius => round_r;
        public double RoundHeight => round_h;
        public double Resolution => resolution;

        private Polyhedron AdjustedPolyhedron => new Polyhedron(points, faces, 1);

        private Cylinder RoundingCylinder => new Cylinder(round_r, round_h, resolution: resolution);

        public string OSCADMethod => new Minkowski(AdjustedPolyhedron, RoundingCylinder).OSCADMethod;
    }
}
