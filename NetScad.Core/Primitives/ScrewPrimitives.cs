using NetScad.Core.Interfaces;
using NetScad.Core.Models;
using System;

namespace NetScad.Core.Primitives
{
    public enum ScrewPrimitives
    {
        ScrewHole,           // Cylinder for screw clearance hole
        ScrewBoss,           // Cylinder for boss around screw hole (2x clearance hole radius)
        ScrewHeadCountersink // Cylinder for screw head countersink (2x screw head radius)
    }

    public static class ScrewPrimitivesExtensions
    {
        public static IScadObject ToScadObject(this ScrewPrimitives self, params object[] parameters)
        {
            switch (self)
            {
                case ScrewPrimitives.ScrewHole:
                    if (parameters.Length < 2 || parameters.Length > 3) throw new ArgumentException("ScrewHole requires 2-3 parameters: screwSize (ScrewSize), h (double), [resolution (double)]");
                    var screwSizeHole = (ScrewSize)parameters[0];
                    double hHole = (double)parameters[1];
                    double resHole = parameters.Length > 2 ? (double)parameters[2] : 100;
                    return new ScrewHole(screwSizeHole, hHole, resHole);
                case ScrewPrimitives.ScrewBoss:
                    if (parameters.Length < 2 || parameters.Length > 3) throw new ArgumentException("ScrewBoss requires 2-3 parameters: screwSize (ScrewSize), h (double), [resolution (double)]");
                    var screwSizeBoss = (ScrewSize)parameters[0];
                    double hBoss = (double)parameters[1];
                    double resBoss = parameters.Length > 2 ? (double)parameters[2] : 100;
                    return new ScrewBoss(screwSizeBoss, hBoss, resBoss);
                case ScrewPrimitives.ScrewHeadCountersink:
                    if (parameters.Length < 1 || parameters.Length > 2) throw new ArgumentException("ScrewHeadCountersink requires 1-2 parameters: screwSize (ScrewSize), [resolution (double)]");
                    var screwSizeCS = (ScrewSize)parameters[0];
                    double resCS = parameters.Length > 1 ? (double)parameters[1] : 100;
                    return new ScrewHeadCountersink(screwSizeCS, resCS);
                default:
                    throw new ArgumentException("Unknown ScrewPrimitives type");
            }
        }
    }
}