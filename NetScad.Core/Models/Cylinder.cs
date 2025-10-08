using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Cylinder(double r, double h, double? r1 = null, double? r2 = null, double resolution = 100) : IScadObject
    {
        public double Radius => r;
        public double? Radius1 => r1;
        public double? Radius2 => r2;
        public double Height => h;
        public double Resolution => resolution;

        private string Params => r1 == null && r2 == null ? $"r = {r}" : $"r1 = {r1 ?? r}, r2 = {r2 ?? r}";

        public string OSCADMethod => $"cylinder(h = {h}, {Params}, $fn = {resolution});";
    }
}
