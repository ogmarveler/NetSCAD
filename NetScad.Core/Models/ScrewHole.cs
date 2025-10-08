using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class ScrewHole(ScrewSize screwSize, double h, double resolution = 100) : IScadObject
    {
        public ScrewSize ScrewSize => screwSize;
        public double Height => h;
        public double Resolution => resolution;

        private Cylinder HoleCylinder => new Cylinder(screwSize.ClearanceHoleRadius, h, resolution: resolution);

        public string OSCADMethod => HoleCylinder.OSCADMethod;
    }
}
