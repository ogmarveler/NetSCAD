using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Line(double x1, double y1, double x2, double y2) : IScadObject
    {
        public double X1 => x1;
        public double Y1 => y1;
        public double X2 => x2;
        public double Y2 => y2;

        public string OSCADMethod => $"line([{x1}, {y1}], [{x2}, {y2}]);";
    }
}
