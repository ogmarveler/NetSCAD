namespace NetScad.Core.Models
{
    public partial class ScrewSize(double screwRadius, double screwHeadRadius, double threadedInsertRadius, double clearanceHoleRadius, double countersunkHeight)
    {
        public double ScrewRadius => screwRadius;
        public double ScrewHeadRadius => screwHeadRadius;
        public double ThreadedInsertRadius => threadedInsertRadius;
        public double ClearanceHoleRadius => clearanceHoleRadius;
        public double CountersunkHeight => countersunkHeight;
    }
}
