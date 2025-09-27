// Namespaces used for this module
using NetScad.Core.Measurements;
using NetScad.Core.SCAD.Models;
using NetScad.Core.SCAD.Modules;
using static NetScad.Core.SCAD.Utility.AxisConfig;

// Apply custom axis settings - will save to a SCAD file as a module
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
// All parameters are optional, so only new AxisSettings() needs to be called to generate axes.
var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
var axisSettings = new AxisSettings(
     backgroundType: Selector.BackgroundType.Light,
     measureType: Selector.MeasureType.Metric,
     minX: 0, // can set as mm
     maxX: 25.4, // can set as mm
     minY: FractionalInch.Inch1.ToMillimeters(0), // can be set as inches
     maxY: FractionalInch.Inch1.ToMillimeters(3), // negative axis only can be applied (insets, etc.)
     minZ: -50.8,
     maxZ: FractionalInch.Inch1.ToMillimeters(2) // fractional axes can be used (2")
);

// Generates (or updates a previously saved axis) and stores it in an aggregate axes file
// The new axis can then be called as a module from your main SCAD project file
GUI.SetAxis(axisSettings, addToExistOutputFile: true).GetAwaiter().GetResult();
