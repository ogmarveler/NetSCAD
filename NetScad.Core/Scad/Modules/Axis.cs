using NetScad.Core.Measurements;
using System;
using static NetScad.Core.SCAD.Utility.BlockStatement;
using static NetScad.Core.SCAD.Utility.AxisConfig;
using static NetScad.Core.SCAD.Models.Primitive;
using NetScad.Core.SCAD.Models;
using NetScad.Core.Core.Measurements;

namespace NetScad.Core.SCAD.Modules
{
    public class Axis
    {
        public static AxisSettings CheckAxisSettings(AxisSettings axisSettings)
        {
            if (axisSettings is not null)
            {
                // X Axis
                // Default to 0-300mm if range is zero
                if (Math.Abs(axisSettings.MinX - axisSettings.MaxX) == 0)
                {
                    axisSettings.MinX = 0;
                    axisSettings.MaxX = 300;
                }
                // Use range if min and max are above or below 0 axis points - move to 0 axis point
                else if (axisSettings.MinX > 0 && axisSettings.MaxX > 0)
                {
                    axisSettings.MaxX = Math.Abs(axisSettings.MaxX - axisSettings.MinX);
                    axisSettings.MinX = 0;
                }
                else if (axisSettings.MinX < 0 && axisSettings.MaxX < 0)
                {
                    axisSettings.MinX = Math.Abs(axisSettings.MaxX - axisSettings.MinX) * -1;
                    axisSettings.MaxX = 0;
                }
                // Use range if min and max inverted default to min and max
                else if (axisSettings.MaxX < axisSettings.MinX)
                {
                    if (axisSettings.MaxX < 0) // MaxnX = 0, MinX --> -X
                    {
                        axisSettings.MinX = Math.Abs(axisSettings.MaxX - axisSettings.MinX) * -1;
                        axisSettings.MaxX = 0;
                    }
                    else // MinX = 0, MaxX --> +X
                    {
                        axisSettings.MaxX = Math.Abs(axisSettings.MaxX - axisSettings.MinX);
                        axisSettings.MinX = 0;
                    }
                }

                // Y Axis
                // Default to 0-300mm if range is zero
                if (Math.Abs(axisSettings.MinY - axisSettings.MaxY) == 0)
                {
                    axisSettings.MinY = 0;
                    axisSettings.MaxY = 300;
                }
                // Use range if min and max are above or below 0 axis points - move to 0 axis point
                else if (axisSettings.MinY > 0 && axisSettings.MaxY > 0)
                {
                    axisSettings.MaxY = Math.Abs(axisSettings.MaxY - axisSettings.MinY);
                    axisSettings.MinY = 0;
                }
                else if (axisSettings.MinY < 0 && axisSettings.MaxY < 0)
                {
                    axisSettings.MinY = Math.Abs(axisSettings.MaxY - axisSettings.MinY) * -1;
                    axisSettings.MaxY = 0;
                }
                // Use range if min and max inverted default to min and max
                else if (axisSettings.MaxY < axisSettings.MinY)
                {
                    if (axisSettings.MaxY < 0) // MaxnY = 0, MinY --> -Y
                    {
                        axisSettings.MinY = Math.Abs(axisSettings.MaxY - axisSettings.MinY) * -1;
                        axisSettings.MaxY = 0;
                    }
                    else // MinY = 0, MaxY --> +Y
                    {
                        axisSettings.MaxY = Math.Abs(axisSettings.MaxY - axisSettings.MinY);
                        axisSettings.MinY = 0;
                    }
                }

                // Z Axis
                // Default to 0-300mm if range is zero
                if (Math.Abs(axisSettings.MinZ - axisSettings.MaxZ) == 0)
                {
                    axisSettings.MinZ = 0;
                    axisSettings.MaxZ = 300;
                }
                // Use range if min and max are above or below 0 axis points - move to 0 axis point
                else if (axisSettings.MinZ > 0 && axisSettings.MaxZ > 0)
                {
                    axisSettings.MaxZ = Math.Abs(axisSettings.MaxZ - axisSettings.MinZ);
                    axisSettings.MinZ = 0;
                }
                else if (axisSettings.MinZ < 0 && axisSettings.MaxZ < 0)
                {
                    axisSettings.MinZ = Math.Abs(axisSettings.MaxZ - axisSettings.MinZ) * -1;
                    axisSettings.MaxZ = 0;
                }
                // Use range if min and max inverted default to min and max
                else if (axisSettings.MaxZ < axisSettings.MinZ)
                {
                    if (axisSettings.MaxZ < 0) // MaxnZ = 0, MinZ --> -Z
                    {
                        axisSettings.MinZ = Math.Abs(axisSettings.MaxZ - axisSettings.MinZ) * -1;
                        axisSettings.MaxZ = 0;
                    }
                    else // MinZ = 0, MaxZ --> +Z
                    {
                        axisSettings.MaxZ = Math.Abs(axisSettings.MaxZ - axisSettings.MinZ);
                        axisSettings.MinZ = 0;
                    }
                }

                // For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
                // For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
                // For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
                var precision = axisSettings.MeasureType == Selector.MeasureType.Imperial ? FractionalInch.Inch4.ToMillimeters(1) : 1;

                axisSettings.MinX = AdjustCoordinate(coordinate: axisSettings.MinX, increment: axisSettings.IncrementX, precision: precision);
                axisSettings.MaxX = AdjustCoordinate(coordinate: axisSettings.MaxX, increment: axisSettings.IncrementX, precision: precision);

                axisSettings.MinY = AdjustCoordinate(coordinate: axisSettings.MinY, increment: axisSettings.IncrementY, precision: precision);
                axisSettings.MaxY = AdjustCoordinate(coordinate: axisSettings.MaxY, increment: axisSettings.IncrementY, precision: precision);

                axisSettings.MinZ = AdjustCoordinate(coordinate: axisSettings.MinZ, increment: axisSettings.IncrementZ, precision: precision);
                axisSettings.MaxZ = AdjustCoordinate(coordinate: axisSettings.MaxZ, increment: axisSettings.IncrementZ, precision: precision);
            }
            else
            {
                axisSettings = new AxisSettings();
            }

            return axisSettings;
        }

        public static CustomAxis ConfigureAxisModule(AxisSettings axisSettings)
        {
            // Object to hold the module, color, and labels
            // Create unique module name based on settings
            var xAxis = Math.Abs(axisSettings.MaxX - axisSettings.MinX);
            var yAxis = Math.Abs(axisSettings.MaxY - axisSettings.MinY);
            var zAxis = Math.Abs(axisSettings.MaxZ - axisSettings.MinZ);

            // Labels based on measurement type and total cubic size
            var xLabel = axisSettings.MeasureType == Selector.MeasureType.Imperial ? $"{Math.Round(xAxis / Inch.Inch.ToMillimeters(1), 0)}" : $"{xAxis}";
            var yLabel = axisSettings.MeasureType == Selector.MeasureType.Imperial ? $"{Math.Round(yAxis / Inch.Inch.ToMillimeters(1), 0)}" : $"{yAxis}";
            var zLabel = axisSettings.MeasureType == Selector.MeasureType.Imperial ? $"{Math.Round(zAxis / Inch.Inch.ToMillimeters(1), 0)}" : $"{zAxis}";

            // Labels based on measurement type and axis start point
            var xStart = axisSettings.MeasureType == Selector.MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinX),0) : axisSettings.MinX;
            var yStart = axisSettings.MeasureType == Selector.MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinY), 0) : axisSettings.MinY;
            var zStart = axisSettings.MeasureType == Selector.MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinZ), 0) : axisSettings.MinZ;
            var startLabel = $"_Start_{xStart}x{yStart}x{zStart}".Replace("-", "Neg");
            // Create the Axis Module
            var axisModule = new CustomAxis();
            var unit = axisSettings.MeasureType == Selector.MeasureType.Imperial ? "in" : "mm";
            var scale = axisSettings.MeasureType == Selector.MeasureType.Imperial ? Inch.Inch.ToMillimeters(1) : 1;
            var axisColor = axisSettings.OpenScadColor.ToString().ToLower();
            axisModule.ModuleName = $"{axisSettings.BackgroundType}_{axisSettings.MeasureType}_{xLabel}x{yLabel}x{zLabel}";
            if (xStart == 0 && yStart == 0 && zStart == 0)
            {
                axisModule.ModuleName += "_Origin";
            }
            else
            {
                axisModule.ModuleName += startLabel;
            }
            axisModule.CallingMethod = $"{axisModule.ModuleName}(colorVal, alpha);";

            // Create the Axis Module - user defined settings
            axisModule.AxisModule = $"// {axisModule.ModuleName} {axisSettings.MeasureType} {AxisModuleFormats.ModuleComments}\n" +
                $"module {axisModule.ModuleName.ToLower()}(colorVal, alpha) {{\n" +
                $"    color(colorVal, alpha) {{\n" +  // Wrap all in color
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX, axisSettings.MaxX])}\n{{   {AxisModuleFormats.XOffsetMarker}   }}\n" +  // X Axis Marker
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY, axisSettings.MaxY])}\n{{   {AxisModuleFormats.YOffsetMarker}   }}\n" +  // Y Axis Marker
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ, axisSettings.MaxZ])}\n{{   {AxisModuleFormats.ZOffsetMarker}   }}\n" +  // Z Axis Marker
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX2, axisSettings.MaxX])}\n{{   {AxisModuleFormats.XOffsetMarker2}   }}\n" +  // X Axis Marker 2
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY2, axisSettings.MaxY])}\n{{   {AxisModuleFormats.YOffsetMarker2}   }}\n" +  // Y Axis Marker 2
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ2, axisSettings.MaxZ])}\n{{   {AxisModuleFormats.ZOffsetMarker2}   }}\n" +  // Z Axis Marker 2
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX3, axisSettings.MaxX])}\n{{   {AxisModuleFormats.XOffsetMarker3}   }}\n" +  // X Axis Marker 3
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY3, axisSettings.MaxY])}\n{{   {AxisModuleFormats.YOffsetMarker3}   }}\n" +  // Y Axis Marker 3
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ3, axisSettings.MaxZ])}\n{{   {AxisModuleFormats.ZOffsetMarker3}   }}\n" +  // Z Axis Marker 3
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX4, axisSettings.MaxX])}\n{{   {AxisModuleFormats.XOffsetMarker4}   }}\n" +  // X Axis Marker 4
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY4, axisSettings.MaxY])}\n{{   {AxisModuleFormats.YOffsetMarker4}   }}\n" +  // Y Axis Marker 4
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ4, axisSettings.MaxZ])}\n{{   {AxisModuleFormats.ZOffsetMarker4}   }}\n" +  // Z Axis Marker 4
                $"             // Axis Labels\n" + // Create the Axis Markers - main marker used for measurements, half and quarter markers for visual reference
                $"             unit = \"{unit}\";\n" + // Set unit for labels
                $"             scale = {scale};\n" + // Set scale for labels
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinX, axisSettings.IncrementX, axisSettings.MaxX])}\n{{   {AxisModuleFormats.XOffsetLabel}   }}\n" +  // X Axis Label
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinY, axisSettings.IncrementY, axisSettings.MaxY])}\n{{   {AxisModuleFormats.YOffsetLabel}   }}\n" +  // Y Axis Label
                $"             {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinZ, axisSettings.IncrementZ, axisSettings.MaxZ])}\n{{   {AxisModuleFormats.ZOffsetLabel}   }}\n" +  // Z Axis Label
                $"  }}\n" +
                $"}}\n" +
                $"// End of {axisModule.ModuleName} Module\n";

            // Include the settings used to create the axis for reference
            axisModule.Settings = axisSettings;
            return axisModule;
        }
    }
}