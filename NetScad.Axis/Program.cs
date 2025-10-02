// Namespaces used for this module
using NetScad.Core.Measurements;
using NetScad.Axis.SCAD.Modules;
using NetScad.Core.Utility;
using static NetScad.Core.Measurements.FractionalInch;
using static NetScad.Axis.SCAD.Utility.AxisConfig;
using static NetScad.Core.Measurements.Selector;

// Apply custom axis settings - will save to a SCAD file as a module
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
// All parameters are optional, so only new AxisSettings() needs to be called to generate axes.
// Cache if called multiple times
var axisSettings = new AxisSettings(
     outputDirectory: PathHelper.GetProjectRoot(), // ..NetScad.Axis/Scad/Axis
     backgroundType: BackgroundType.Dark, // Axis color will be white, or black if Light background type selected
     measureType: UnitSystem.Imperial, // inches
     minX: 0, // for positive only axis, max must be positive
     maxX: Inch1.ToMm(5), // can set as inches
     minY: Inch1.ToMm(-5), // can set negative axis starting point
     maxY: 70, // can be set as millimeters, will adjust to 76.2mm if imperial measure type selected
     minZ: -Inch4.ToMm(5), // Fractional axes can be used (3/4")
     maxZ: 0 // for negative only axis, min must be negative
);
// Generates (or updates a previously saved axis) and stores it in an aggregate axes file
// The new axis can then be called as a module from your main SCAD project file
_ = await GUI.SetAxis(axisSettings);
