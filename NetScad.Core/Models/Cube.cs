using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Cube(double l, double w, double h) : IScadObject
    {
        public double Length => l;
        public double Width => w;
        public double Height => h;

        public string OSCADMethod => $"cube([{l}, {w}, {h}]);";
    }
}
