using NetScad.Core.Measurements;
using NetScad.Core.SCAD.Models;
using System;
using System.IO;

namespace NetScad.Core.SCAD.Utility
{
    public class AxisConfig
    {
        public static double AdjustCoordinate(
        double coordinate, double increment, double scale = 1.0, double precision = 1.0)
        {
            // Get quotient and remainder
            if (increment == 0 || scale == 0)
                return coordinate;

            double scaledCoord = coordinate * scale;
            double remainder = Math.Round(scaledCoord % increment,5);
            if (remainder == 0)
                return coordinate; // Already valid
            // Round up to the next increment
            double adjustedScaled = 0;
            if (scaledCoord > 0)
                adjustedScaled = scaledCoord + increment - remainder;  // Positive axis <-
            else
                adjustedScaled = scaledCoord - increment - remainder; // Negative axis ->
            // Adjust back to original scale and round to specified precision
            return Math.Round(adjustedScaled,5);
        }

        public static bool ValidateDivision(double axisLength, double increment, double scale = 1.0)
        {
            // Ensure non-zero values to avoid division by zero
            if (increment == 0 || scale == 0)
                return false;

            // Apply scale to axis length
            double scaledLength = axisLength * scale;

            // Check if scaled length is divisible by increment
            return scaledLength % increment == 0;
        }

        public static (bool IsValid, double AdjustedIncrement) SuggestIncrement(double axisLength, double increment, double scale = 1.0)
        {
            if (increment == 0 || scale == 0)
                return (false, increment);

            double scaledLength = axisLength * scale;

            // If divisible, return original increment
            if (scaledLength % increment == 0)
                return (true, increment);

            // Suggest the nearest increment that divides evenly
            double adjusted = Math.Ceiling(scaledLength / increment) * increment;
            return (false, adjusted / scale); // Adjust back to original scale
        }

        public static class AxisModuleFormats
        {
            public const string XOffsetMarker = "if(x != 0)\n translate([x - .2, -8.75, -.01]) cube([0.2, 8.75, 0.02]);";
            public const string YOffsetMarker = "if(y != 0)\n translate([-8.75, y - .2, -.01]) cube([8.75, 0.2, 0.02]);";
            public const string ZOffsetMarker = "if(z != 0)\n translate([-7.5, -7.5, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 7.5]);";
            public const string XOffsetLabel = "if(i != 0)\n translate([i - 0.875, -10, -.01]) linear_extrude(0.02) rotate(270) text(str(i/scale, unit), size=2);";
            public const string YOffsetLabel = "if(i != 0)\n translate([-10, i + 0.875, -.01]) linear_extrude(0.02) rotate(180) text(str(i/scale, unit), size=2);";
            public const string ZOffsetLabel = "if(i != 0)\n translate([-8.75, -8.75, i - .875]) rotate([0,45,135]) linear_extrude(0.02) rotate(90) text(str(i/scale, unit), size=1.75);";
            public const string XOffsetMarker2 = "if(x != 0)\n translate([x - .2, -5, -.01]) cube([0.2, 5, 0.02]);";
            public const string YOffsetMarker2 = "if(y != 0)\n translate([-5, y - .2, -.01]) cube([5, 0.2, 0.02]);";
            public const string ZOffsetMarker2 = "if(z != 0)\n translate([-3.75, -3.75, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 5]);";
            public const string XOffsetMarker3 = "if(x != 0)\n translate([x - .2, -2.5, -.01]) cube([0.2, 2.5, 0.02]);";
            public const string YOffsetMarker3 = "if(y != 0)\n translate([-2.5, y - .2, -.01]) cube([2.5, 0.2, 0.02]);";
            public const string ZOffsetMarker3 = "if(z != 0)\n translate([-1.75, -1.75, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 2.5]);";
            public const string XOffsetMarker4 = "if(x != 0)\n translate([x - .2, -1.25, -.01]) cube([0.2, 1.25, 0.02]);";
            public const string YOffsetMarker4 = "if(y != 0)\n translate([-1.25, y - .2, -.01]) cube([1.25, 0.2, 0.02]);";
            public const string ZOffsetMarker4 = "if(z != 0)\n translate([-.875, -.875, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 1.25]);";
            public const string ModuleComments = "NetScad.Core Axis Module\n" +
                "// Creates a 3D axis with labeled measurements along the X, Y, and Z axes.\n" +
                "// Parameters:\n" +
                "// - MeasureType: 'Metric' for millimeters or 'Imperial' for inches (default: Metric)\n" +
                "// - IncrementX, IncrementY, IncrementZ: Spacing between labels on each axis (default: 1.5875mm)\n" +
                "// - MinX, MaxX: Minimum and maximum values for the X axis (default: 0 to 300mm)\n" +
                "// - MinY, MaxY: Minimum and maximum values for the Y axis (default: 0 to 300mm)\n" +
                "// - MinZ, MaxZ: Minimum and maximum values for the Z axis (default: 0 to 300mm)\n";
        }

        public class AxisSettings(Selector.BackgroundType backgroundType = Selector.BackgroundType.Light, Selector.MeasureType measureType = Selector.MeasureType.Metric, double minX = 0, double minY = 0, double minZ = 0, double maxX = 300, double maxY = 300, double maxZ = 300, double axisColorAlpha = 1, Selector.OpenScadColor? axisColor = null, string? outputFolder = null)
        {
            public Selector.BackgroundType BackgroundType { get; set; } = backgroundType;
            public Selector.MeasureType MeasureType { get; set; } = measureType;
            public double MinX { get; set; } = minX;
            public double MaxX { get; set; } = maxX; // Default to 300mm (12 inches)
            public double MinY { get; set; } = minY;
            public double MaxY { get; set; } = maxY; // Default to 300mm (12 inches)
            public double MinZ { get; set; } = minZ;
            public double MaxZ { get; set; } = maxZ; // Default to 300mm (12 inches)
            public Selector.OpenScadColor OpenScadColor { get; set; } = (Selector.OpenScadColor)(axisColor is not null ? axisColor :
                backgroundType == Selector.BackgroundType.Light ? Selector.OpenScadColor.Black : Selector.OpenScadColor.White); // Default color if light or dark background
            public double AxisColorAlpha { get; set; } = axisColorAlpha;
            public string OutputFolder { get; set; } = outputFolder ?? Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            // Non-editable properties
            public double IncrementX => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch4.ToMillimeters(1) : 20;
            public double IncrementY => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch4.ToMillimeters(1) : 20;
            public double IncrementZ => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch4.ToMillimeters(1) : 20;
            public double IncrementX2 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch8.ToMillimeters(1) : 10;
            public double IncrementY2 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch8.ToMillimeters(1) : 10;
            public double IncrementZ2 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch8.ToMillimeters(1) : 10;
            public double IncrementX3 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch16.ToMillimeters(1) : 5;
            public double IncrementY3 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch16.ToMillimeters(1) : 5;
            public double IncrementZ3 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch16.ToMillimeters(1) : 5;
            public double IncrementX4 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch32.ToMillimeters(1) : 1;
            public double IncrementY4 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch32.ToMillimeters(1) : 1;
            public double IncrementZ4 => MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch32.ToMillimeters(1) : 1;
            public string Unit => MeasureType == Selector.MeasureType.Metric ? "mm" : "in";
        }

        public class CustomAxis
        {
            public string AxisModule { get; set; } = string.Empty;
            public AxisSettings Settings { get; set; } = new AxisSettings();
            public string CallingMethod { get; set; } = string.Empty;
            public string ModuleName { get; set; } = string.Empty;
        }
    }
}
