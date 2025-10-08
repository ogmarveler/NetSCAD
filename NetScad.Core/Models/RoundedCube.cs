using NetScad.Core.Interfaces;
using System;

namespace NetScad.Core.Models
{
    public partial class RoundedCube(double l, double w, double h, double round_r, double round_h = 0.001, double resolution = 200) : IScadObject
    {
        public double Length => l;
        public double Width => w;
        public double Height => h;
        public double RoundRadius => round_r;
        public double RoundHeight => round_h;
        public double Resolution => resolution;

        private Cube AdjustedCube => new Cube(
            Math.Max(0, l - 2 * round_r),
            Math.Max(0, w - 2 * round_r),
            h
        );

        private Cylinder RoundingCylinder => new Cylinder(round_r, round_h, resolution: resolution);

        public string OSCADMethod => new Minkowski(AdjustedCube, RoundingCylinder).OSCADMethod;
    }
}
