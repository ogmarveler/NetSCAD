using NetScad.Core.Interfaces;
using System;

namespace NetScad.Core.Models
{
    public partial class RoundedSphere(double r, double round_r, double round_h = 0.001, double resolution = 200) : IScadObject
    {
        public double Radius => r;
        public double RoundRadius => round_r;
        public double RoundHeight => round_h;
        public double Resolution => resolution;

        private Sphere AdjustedSphere => new Sphere(
            Math.Max(0, r - round_r),
            resolution
        );

        private Cylinder RoundingCylinder => new Cylinder(round_r, round_h, resolution: resolution);

        public string OSCADMethod => new Minkowski(AdjustedSphere, RoundingCylinder).OSCADMethod;
    }
}
