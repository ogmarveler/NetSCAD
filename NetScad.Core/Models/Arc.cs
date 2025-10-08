using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Arc(double r, double start, double end, double resolution = 100) : IScadObject
    {
        public double Radius => r;
        public double Start => start;
        public double End => end;
        public double Resolution => resolution;

        public string OSCADMethod => $"arc(r = {r}, start = {start}, end = {end}, $fn = {resolution});";
    }
}
