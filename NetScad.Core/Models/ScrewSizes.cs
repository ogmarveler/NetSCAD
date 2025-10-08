namespace NetScad.Core.Models
{
    public static class ScrewSizes
    {
        public static readonly ScrewSize M2 = new ScrewSize(
            screwRadius: 1.0,           // M2 screw radius (2mm diameter / 2)
            screwHeadRadius: 2.0,       // Typical countersunk head radius
            threadedInsertRadius: 1.2,   // Threaded insert radius
            clearanceHoleRadius: 1.15,  // Clearance hole radius
            countersunkHeight: 1.2      // Countersunk head height (~0.6 × 2mm)
        );

        public static readonly ScrewSize M2_5 = new ScrewSize(
            screwRadius: 1.25,          // M2.5 screw radius (2.5mm diameter / 2)
            screwHeadRadius: 2.5,       // Typical countersunk head radius
            threadedInsertRadius: 1.5,   // Threaded insert radius
            clearanceHoleRadius: 1.45,  // Clearance hole radius
            countersunkHeight: 1.5      // Countersunk head height (~0.6 × 2.5mm)
        );

        public static readonly ScrewSize M3 = new ScrewSize(
            screwRadius: 1.5,           // M3 screw radius (3mm diameter / 2)
            screwHeadRadius: 3.0,       // Typical countersunk head radius
            threadedInsertRadius: 1.8,   // Threaded insert radius
            clearanceHoleRadius: 1.75,  // Clearance hole radius
            countersunkHeight: 1.8      // Countersunk head height (~0.6 × 3mm)
        );

        public static readonly ScrewSize M4 = new ScrewSize(
            screwRadius: 2.0,           // M4 screw radius (4mm diameter / 2)
            screwHeadRadius: 4.0,       // Typical countersunk head radius
            threadedInsertRadius: 2.4,   // Threaded insert radius
            clearanceHoleRadius: 2.3,   // Clearance hole radius
            countersunkHeight: 2.4      // Countersunk head height (~0.6 × 4mm)
        );

        public static readonly ScrewSize M5 = new ScrewSize(
            screwRadius: 2.5,           // M5 screw radius (5mm diameter / 2)
            screwHeadRadius: 5.0,       // Typical countersunk head radius
            threadedInsertRadius: 3.0,   // Threaded insert radius
            clearanceHoleRadius: 2.9,   // Clearance hole radius
            countersunkHeight: 3.0      // Countersunk head height (~0.6 × 5mm)
        );

        public static readonly ScrewSize M6 = new ScrewSize(
            screwRadius: 3.0,           // M6 screw radius (6mm diameter / 2)
            screwHeadRadius: 6.0,       // Typical countersunk head radius
            threadedInsertRadius: 3.6,   // Threaded insert radius
            clearanceHoleRadius: 3.5,   // Clearance hole radius
            countersunkHeight: 3.6      // Countersunk head height (~0.6 × 6mm)
        );

        public static readonly ScrewSize M8 = new ScrewSize(
            screwRadius: 4.0,           // M8 screw radius (8mm diameter / 2)
            screwHeadRadius: 8.0,       // Typical countersunk head radius
            threadedInsertRadius: 4.8,   // Threaded insert radius
            clearanceHoleRadius: 4.7,   // Clearance hole radius
            countersunkHeight: 4.8      // Countersunk head height (~0.6 × 8mm)
        );

        public static readonly ScrewSize M10 = new ScrewSize(
            screwRadius: 5.0,           // M10 screw radius (10mm diameter / 2)
            screwHeadRadius: 10.0,      // Typical countersunk head radius
            threadedInsertRadius: 6.0,   // Threaded insert radius
            clearanceHoleRadius: 5.9,   // Clearance hole radius
            countersunkHeight: 6.0      // Countersunk head height (~0.6 × 10mm)
        );
    }
}
