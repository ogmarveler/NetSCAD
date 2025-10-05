/******************************************************************************/
// Axes for reference
// Usage: use <Axes/axes.scad>;  // add to your main file;
// Options: unit="mm" or "in" (default is "mm")
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
/******************************************************************************/

// 3D Axis Module - Light 300x300x300 MM Orig 0x0x0
// Calling Method: Get_Get_Light_300x300x300_MM_Orig_0x0x0();
// Settings: UnitSystem=Metric, BackgroundType=Light, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <light_300x300x300_mm_orig_0x0x0.scad>;
module Get_Light_300x300x300_MM_Orig_0x0x0(colorVal = "Black", alpha = 1) {
  light_300x300x300_mm_orig_0x0x0(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 300x300x300 MM Orig 0x0x0
// Calling Method: Get_Get_Dark_300x300x300_MM_Orig_0x0x0();
// Settings: UnitSystem=Metric, BackgroundType=Dark, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <dark_300x300x300_mm_orig_0x0x0.scad>;
module Get_Dark_300x300x300_MM_Orig_0x0x0(colorVal = "White", alpha = 1) {
  dark_300x300x300_mm_orig_0x0x0(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light 12x12x12 Inch Orig 0x0x0
// Calling Method: Get_Get_Light_12x12x12_Inch_Orig_0x0x0();
// Settings: UnitSystem=Imperial, BackgroundType=Light, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <light_12x12x12_inch_orig_0x0x0.scad>;
module Get_Light_12x12x12_Inch_Orig_0x0x0(colorVal = "Black", alpha = 1) {
  light_12x12x12_inch_orig_0x0x0(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 12x12x12 Inch Orig 0x0x0
// Calling Method: Get_Get_Dark_12x12x12_Inch_Orig_0x0x0();
// Settings: UnitSystem=Imperial, BackgroundType=Dark, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <dark_12x12x12_inch_orig_0x0x0.scad>;
module Get_Dark_12x12x12_Inch_Orig_0x0x0(colorVal = "White", alpha = 1) {
  dark_12x12x12_inch_orig_0x0x0(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 3x3x4 Inch Orig N1x0xN1
// Calling Method: Get_Get_Dark_3x3x4_Inch_Orig_N1x0xN1();
// Settings: UnitSystem=Imperial, BackgroundType=Dark, MinX=-25.4, MaxX=50.8, MinY=0, MaxY=76.2, MinZ=-25.4, MaxZ=76.2
include <dark_3x3x4_inch_orig_n1x0xn1.scad>;
module Get_Dark_3x3x4_Inch_Orig_N1x0xN1(colorVal = "White", alpha = 1) {
  dark_3x3x4_inch_orig_n1x0xn1(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light 3x3x4 Inch Orig N1x0xN1
// Calling Method: Get_Get_Light_3x3x4_Inch_Orig_N1x0xN1();
// Settings: UnitSystem=Imperial, BackgroundType=Light, MinX=-25.4, MaxX=50.8, MinY=0, MaxY=76.2, MinZ=-25.4, MaxZ=76.2
include <light_3x3x4_inch_orig_n1x0xn1.scad>;
module Get_Light_3x3x4_Inch_Orig_N1x0xN1(colorVal = "Black", alpha = 1) {
  light_3x3x4_inch_orig_n1x0xn1(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light 100x80x120 MM Orig N40x0xN40
// Calling Method: Get_Get_Light_100x80x120_MM_Orig_N40x0xN40();
// Settings: UnitSystem=Metric, BackgroundType=Light, MinX=-40, MaxX=60, MinY=0, MaxY=80, MinZ=-40, MaxZ=80
include <light_100x80x120_mm_orig_n40x0xn40.scad>;
module Get_Light_100x80x120_MM_Orig_N40x0xN40(colorVal = "Black", alpha = 1) {
  light_100x80x120_mm_orig_n40x0xn40(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 100x80x120 MM Orig N40x0xN40
// Calling Method: Get_Get_Dark_100x80x120_MM_Orig_N40x0xN40();
// Settings: UnitSystem=Metric, BackgroundType=Dark, MinX=-40, MaxX=60, MinY=0, MaxY=80, MinZ=-40, MaxZ=80
include <dark_100x80x120_mm_orig_n40x0xn40.scad>;
module Get_Dark_100x80x120_MM_Orig_N40x0xN40(colorVal = "White", alpha = 1) {
  dark_100x80x120_mm_orig_n40x0xn40(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 18x18x18 Inch Orig N6xN6xN6
// Calling Method: Get_Get_Dark_18x18x18_Inch_Orig_N6xN6xN6();
// Settings: UnitSystem=Imperial, BackgroundType=Dark, MinX=-152.4, MaxX=304.8, MinY=-152.4, MaxY=304.8, MinZ=-152.4, MaxZ=304.8
include <dark_18x18x18_inch_orig_n6xn6xn6.scad>;
module Get_Dark_18x18x18_Inch_Orig_N6xN6xN6(colorVal = "White", alpha = 1) {
  dark_18x18x18_inch_orig_n6xn6xn6(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light 18x18x18 Inch Orig N6xN6xN6
// Calling Method: Get_Get_Light_18x18x18_Inch_Orig_N6xN6xN6();
// Settings: UnitSystem=Imperial, BackgroundType=Light, MinX=-152.4, MaxX=304.8, MinY=-152.4, MaxY=304.8, MinZ=-152.4, MaxZ=304.8
include <light_18x18x18_inch_orig_n6xn6xn6.scad>;
module Get_Light_18x18x18_Inch_Orig_N6xN6xN6(colorVal = "Black", alpha = 1) {
  light_18x18x18_inch_orig_n6xn6xn6(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark 480x480x480 MM Orig N160xN160xN160
// Calling Method: Get_Get_Dark_480x480x480_MM_Orig_N160xN160xN160();
// Settings: UnitSystem=Metric, BackgroundType=Dark, MinX=-160, MaxX=320, MinY=-160, MaxY=320, MinZ=-160, MaxZ=320
include <dark_480x480x480_mm_orig_n160xn160xn160.scad>;
module Get_Dark_480x480x480_MM_Orig_N160xN160xN160(colorVal = "White", alpha = 1) {
  dark_480x480x480_mm_orig_n160xn160xn160(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light 480x480x480 MM Orig N160xN160xN160
// Calling Method: Get_Get_Light_480x480x480_MM_Orig_N160xN160xN160();
// Settings: UnitSystem=Metric, BackgroundType=Light, MinX=-160, MaxX=320, MinY=-160, MaxY=320, MinZ=-160, MaxZ=320
include <light_480x480x480_mm_orig_n160xn160xn160.scad>;
module Get_Light_480x480x480_MM_Orig_N160xN160xN160(colorVal = "Black", alpha = 1) {
  light_480x480x480_mm_orig_n160xn160xn160(colorVal = colorVal, alpha = alpha);
}

