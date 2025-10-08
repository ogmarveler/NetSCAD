using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class ScrewBoss(ScrewSize screwSize, double h, double resolution = 100) : IScadObject
    {
        public ScrewSize ScrewSize => screwSize;
        public double Height => h;
        public double Resolution => resolution;

        private Cylinder BossCylinder => new Cylinder(screwSize.ClearanceHoleRadius * 2, h, resolution: resolution);

        public string OSCADMethod => BossCylinder.OSCADMethod;
    }
}
