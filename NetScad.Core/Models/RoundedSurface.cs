using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class RoundedSurface(string file, double round_r, bool center = false, double round_h = 0.001, double resolution = 200) : IScadObject
    {
        public string File => file;
        public double RoundRadius => round_r;
        public bool Center => center;
        public double RoundHeight => round_h;
        public double Resolution => resolution;

        private Surface AdjustedSurface => new Surface(file, center, 1);

        private Cylinder RoundingCylinder => new Cylinder(round_r, round_h, resolution: resolution);

        public string OSCADMethod => new Minkowski(AdjustedSurface, RoundingCylinder).OSCADMethod;
    }
}
