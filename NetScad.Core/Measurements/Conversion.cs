using NetScad.Core.Measurements;

namespace NetScad.Core.Core.Measurements
{
    public static class Conversion
    {
        // Convert inches to mm
        public static double InchToMm(double inch)
        {
            return Inch.Inch.ToMm((int)inch);
        }

        // Convert mm to inches
        public static double MmToInches(double mm)
        {
            return mm * 0.0393700787;
        }

        // Convert a radius in mm to a ratio for rounding edges in OpenSCAD
        public static double RoundEdgeHeight(double radius)
        {
            return radius / 50;
        }

        // Calculate rounding radius from width of main object
        public static double RoundFromWidth(double radius)
        {
            return radius * .05f;
        }

        // Convert a radius in mm to a Minkowski offset in OpenSCAD
        public static double MinkowskiOffsets(double radius)
        {
            return radius * 2;
        }
    }
}
