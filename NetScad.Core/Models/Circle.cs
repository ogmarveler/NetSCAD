using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Circle(double r, double resolution = 100) : IScadObject
    {
        public double Radius => r;
        public double Resolution => resolution;

        public string OSCADMethod => $"circle(r = {r}, $fn = {resolution});";
    }
}
