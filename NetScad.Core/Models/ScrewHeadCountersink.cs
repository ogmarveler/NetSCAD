using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class ScrewHeadCountersink(ScrewSize screwSize, double resolution = 100) : IScadObject
    {
        public ScrewSize ScrewSize => screwSize;
        public double Resolution => resolution;

        private Cylinder CountersinkCylinder => new Cylinder(screwSize.ScrewHeadRadius * 2, screwSize.CountersunkHeight, resolution: resolution);

        public string OSCADMethod => CountersinkCylinder.OSCADMethod;
    }
}
