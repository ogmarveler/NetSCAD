/******************************************************************************/
// Axes for reference
// Usage: axes(100);
// Options: unit="mm" or "in" (default is "mm")
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
/******************************************************************************/



// 3D Axis Module - Dark Imperial 3x3x4 Start 0x0xNeg2
// Calling Method: Get_Dark_Imperial_3x3x4_Start_0x0xNeg2(colorVal, alpha);
// Settings: MeasureType=Imperial, MinX=0, MaxX=76.2, MinY=0, MaxY=76.2, MinZ=-50.8, MaxZ=50.8
include <dark_imperial_3x3x4_start_0x0xneg2.scad>;
module Get_Dark_Imperial_3x3x4_Start_0x0xNeg2(colorVal = "White", alpha = 1) {
  dark_imperial_3x3x4_start_0x0xneg2(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Imperial 12x12x12 Origin
// Calling Method: Get_Dark_Imperial_12x12x12_Origin(colorVal, alpha);
// Settings: MeasureType=Imperial, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <dark_imperial_12x12x12_origin.scad>;
module Get_Dark_Imperial_12x12x12_Origin(colorVal = "White", alpha = 1) {
  dark_imperial_12x12x12_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light Imperial 12x12x12 Origin
// Calling Method: Get_Light_Imperial_12x12x12_Origin(colorVal, alpha);
// Settings: MeasureType=Imperial, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <light_imperial_12x12x12_origin.scad>;
module Get_Light_Imperial_12x12x12_Origin(colorVal = "Black", alpha = 1) {
  light_imperial_12x12x12_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light Metric 300x300x300 Origin
// Calling Method: Get_Light_Metric_300x300x300_Origin(colorVal, alpha);
// Settings: MeasureType=Metric, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <light_metric_300x300x300_origin.scad>;
module Get_Light_Metric_300x300x300_Origin(colorVal = "Black", alpha = 1) {
  light_metric_300x300x300_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Metric 300x300x300 Origin
// Calling Method: Get_Dark_Metric_300x300x300_Origin(colorVal, alpha);
// Settings: MeasureType=Metric, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <dark_metric_300x300x300_origin.scad>;
module Get_Dark_Metric_300x300x300_Origin(colorVal = "White", alpha = 1) {
  dark_metric_300x300x300_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Metric 80x80x120 Start 0x0xNeg60
// Calling Method: Get_Dark_Metric_80x80x120_Start_0x0xNeg60(colorVal, alpha);
// Settings: MeasureType=Metric, MinX=0, MaxX=80, MinY=0, MaxY=80, MinZ=-60, MaxZ=60
include <dark_metric_80x80x120_start_0x0xneg60.scad>;
module Get_Dark_Metric_80x80x120_Start_0x0xNeg60(colorVal = "White", alpha = 1) {
  dark_metric_80x80x120_start_0x0xneg60(colorVal = colorVal, alpha = alpha);
}

