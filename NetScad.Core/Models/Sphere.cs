using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Sphere(double r, double resolution = 100) : IScadObject
    {
        public double Radius => r;
        public double Resolution => resolution;

        public string OSCADMethod => $"sphere(r = {r}, $fn = {resolution});";
    }
}
