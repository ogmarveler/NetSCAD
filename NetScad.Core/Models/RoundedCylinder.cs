using NetScad.Core.Interfaces;
using System;

namespace NetScad.Core.Models
{
    public partial class RoundedCylinder(double r, double h, double round_r, double round_h = 0.001, double? r1 = null, double? r2 = null, double resolution = 200) : IScadObject
    {
        public double Radius => r;
        public double Height => h;
        public double RoundRadius => round_r;
        public double RoundHeight => round_h;
        public double? Radius1 => r1;
        public double? Radius2 => r2;
        public double Resolution => resolution;

        private Cylinder AdjustedCylinder => new Cylinder(
            Math.Max(0, r - round_r),
            h,
            r1 != null ? Math.Max(0, r1.Value - round_r) : null,
            r2 != null ? Math.Max(0, r2.Value - round_r) : null,
            resolution
        );

        private Cylinder RoundingCylinder => new Cylinder(round_r, round_h, resolution: resolution);

        public string OSCADMethod => new Minkowski(AdjustedCylinder, RoundingCylinder).OSCADMethod;
    }
}
