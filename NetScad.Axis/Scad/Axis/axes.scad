/******************************************************************************/
// Axes for reference
// Usage: axes(100);
// Options: unit="mm" or "in" (default is "mm")
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
/******************************************************************************/


// 3D Axis Module - Light Metric 300x300x300 Origin
// Calling Method: Get_Light_Metric_300x300x300_Origin();
// Settings: MeasureType=Metric, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <light_metric_300x300x300_origin.scad>;
module Get_Light_Metric_300x300x300_Origin(colorVal = "Black", alpha = 1) {
  light_metric_300x300x300_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Light Imperial 12x12x12 Origin
// Calling Method: Get_Light_Imperial_12x12x12_Origin();
// Settings: MeasureType=Imperial, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <light_imperial_12x12x12_origin.scad>;
module Get_Light_Imperial_12x12x12_Origin(colorVal = "Black", alpha = 1) {
  light_imperial_12x12x12_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Imperial 12x12x12 Origin
// Calling Method: Get_Dark_Imperial_12x12x12_Origin();
// Settings: MeasureType=Imperial, MinX=0, MaxX=304.8, MinY=0, MaxY=304.8, MinZ=0, MaxZ=304.8
include <dark_imperial_12x12x12_origin.scad>;
module Get_Dark_Imperial_12x12x12_Origin(colorVal = "White", alpha = 1) {
  dark_imperial_12x12x12_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Metric 300x300x300 Origin
// Calling Method: Get_Dark_Metric_300x300x300_Origin();
// Settings: MeasureType=Metric, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <dark_metric_300x300x300_origin.scad>;
module Get_Dark_Metric_300x300x300_Origin(colorVal = "White", alpha = 1) {
  dark_metric_300x300x300_origin(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Imperial 3x6x1 Start 0xNeg3xNeg1
// Calling Method: Get_Dark_Imperial_3x6x1_Start_0xNeg3xNeg1();
// Settings: MeasureType=Imperial, MinX=0, MaxX=76.2, MinY=-76.2, MaxY=76.2, MinZ=-19.05, MaxZ=0
include <dark_imperial_3x6x1_start_0xneg3xneg1.scad>;
module Get_Dark_Imperial_3x6x1_Start_0xNeg3xNeg1(colorVal = "White", alpha = 1) {
  dark_imperial_3x6x1_start_0xneg3xneg1(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Imperial 6x6x4 Start Neg3xNeg3xNeg1
// Calling Method: Get_Dark_Imperial_6x6x4_Start_Neg3xNeg3xNeg1();
// Settings: MeasureType=Imperial, MinX=-76.2, MaxX=76.2, MinY=-76.2, MaxY=76.2, MinZ=-19.05, MaxZ=76.2
include <dark_imperial_6x6x4_start_neg3xneg3xneg1.scad>;
module Get_Dark_Imperial_6x6x4_Start_Neg3xNeg3xNeg1(colorVal = "White", alpha = 1) {
  dark_imperial_6x6x4_start_neg3xneg3xneg1(colorVal = colorVal, alpha = alpha);
}

// 3D Axis Module - Dark Metric 160x160x100 Start Neg80xNeg80xNeg20
// Calling Method: Get_Dark_Metric_160x160x100_Start_Neg80xNeg80xNeg20();
// Settings: MeasureType=Metric, MinX=-80, MaxX=80, MinY=-80, MaxY=80, MinZ=-20, MaxZ=80
include <dark_metric_160x160x100_start_neg80xneg80xneg20.scad>;
module Get_Dark_Metric_160x160x100_Start_Neg80xNeg80xNeg20(colorVal = "White", alpha = 1) {
  dark_metric_160x160x100_start_neg80xneg80xneg20(colorVal = colorVal, alpha = alpha);
}

