using System.Collections.Generic;

namespace NetScad.Core
{
    internal class AwgCable
    {
        public int Awg { get; }
        public double ConductorRadiusMm { get; }
        public double InsulatedRadiusMm { get; }
        public double ConductorRadiusInch { get; }
        public double InsulatedRadiusInch { get; }

        public AwgCable(int awg, double conductorRadiusMm, double insulatedRadiusMm, double conductorRadiusInch, double insulatedRadiusInch)
        {
            Awg = awg;
            ConductorRadiusMm = conductorRadiusMm;
            InsulatedRadiusMm = insulatedRadiusMm;
            ConductorRadiusInch = conductorRadiusInch;
            InsulatedRadiusInch = insulatedRadiusInch;
        }
    }

    internal static class AwgData
    {
        internal static List<AwgCable> Cables { get; } = new List<AwgCable>
        {
            new AwgCable(22, 0.643, 1.27, 0.0253, 0.0500),
            new AwgCable(20, 0.812, 1.42, 0.0320, 0.0559),
            new AwgCable(18, 1.024, 1.68, 0.0403, 0.0661),
            new AwgCable(16, 1.291, 2.03, 0.0508, 0.0799),
            new AwgCable(14, 1.628, 2.41, 0.0641, 0.0949),
            new AwgCable(12, 2.053, 2.95, 0.0808, 0.1161),
            new AwgCable(10, 2.588, 3.61, 0.1019, 0.1421),
            new AwgCable(8, 3.264, 4.50, 0.1285, 0.1772),
            new AwgCable(6, 4.115, 5.69, 0.1620, 0.2240),
            new AwgCable(4, 5.189, 7.01, 0.2043, 0.2760),
            new AwgCable(2, 6.544, 8.89, 0.2576, 0.3500),
            new AwgCable(1, 7.348, 9.78, 0.2893, 0.3850),
            new AwgCable(1_0, 8.253, 10.8, 0.3249, 0.4252),
            new AwgCable(2_0, 9.266, 11.9, 0.3648, 0.4685),
            new AwgCable(3_0, 10.41, 13.2, 0.4096, 0.5197),
            new AwgCable(4_0, 11.68, 14.7, 0.4598, 0.5787)
        };
    }
}
