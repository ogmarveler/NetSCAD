namespace NetScad.Core.Measurements
{
    using System;

    public static class VolumeConverter
    {
        private const double Cm3ToIn3 = 1.0 / 16.387064;  // cm³ to in³
        private const double In3ToCm3 = 16.387064;        // in³ to cm³
        private const double M3ToFt3 = 35.314666711658;   // m³ to ft³
        private const double Ft3ToM3 = 1.0 / 35.314666711658; // ft³ to m³

        // Existing methods...
        public static double ConvertCm3ToIn3(double cm3) => cm3 * Cm3ToIn3;
        public static double ConvertM3ToFt3(double m3) => m3 * M3ToFt3;

        // New reverse methods
        public static double ConvertIn3ToCm3(double in3) => in3 * In3ToCm3;
        public static double ConvertFt3ToM3(double ft3) => ft3 * Ft3ToM3;

        // Updated generic method
        public enum VolumeUnit { Cm3, M3, In3, Ft3 } // Expanded enum
        public enum TargetUnit { Cm3, M3, In3, Ft3 } // Renamed for clarity

        public static double Convert(double value, VolumeUnit from, TargetUnit to)
        {
            return (from, to) switch
            {
                (VolumeUnit.Cm3, TargetUnit.In3) => ConvertCm3ToIn3(value),
                (VolumeUnit.M3, TargetUnit.Ft3) => ConvertM3ToFt3(value),
                (VolumeUnit.In3, TargetUnit.Cm3) => ConvertIn3ToCm3(value),
                (VolumeUnit.Ft3, TargetUnit.M3) => ConvertFt3ToM3(value),
                _ => throw new ArgumentException("Unsupported unit pair")
            };
        }
    }
}
