using NetScad.Core.Core.Measurements;
using NetScad.Core.Measurements;
using NetScad.Core.Utility;
using System;
using System.Text;
using static NetScad.Core.SCAD.Models.Primitive;
using static NetScad.Core.SCAD.Utility.AxisConfig;
using static NetScad.Core.SCAD.Utility.BlockStatement;
using static NetScad.Core.SCAD.Models.Selector;

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
                var precision = axisSettings.MeasureType == MeasureType.Imperial ? FractionalInch.Inch4.ToMm(1) : 1;

                axisSettings.MinX = AdjustCoordinate(coordinate: axisSettings.MinX, increment: axisSettings.IncrementX, precision: precision);
                axisSettings.MaxX = AdjustCoordinate(coordinate: axisSettings.MaxX, increment: axisSettings.IncrementX, precision: precision);

                axisSettings.MinY = AdjustCoordinate(coordinate: axisSettings.MinY, increment: axisSettings.IncrementY, precision: precision);
                axisSettings.MaxY = AdjustCoordinate(coordinate: axisSettings.MaxY, increment: axisSettings.IncrementY, precision: precision);

                axisSettings.MinZ = AdjustCoordinate(coordinate: axisSettings.MinZ, increment: axisSettings.IncrementZ, precision: precision);
                axisSettings.MaxZ = AdjustCoordinate(coordinate: axisSettings.MaxZ, increment: axisSettings.IncrementZ, precision: precision);
            }
            else
            {
                axisSettings = new AxisSettings(
                    outputDirectory: PathHelper.GetProjectRoot()
                    );
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
            var xLabel = axisSettings.MeasureType == MeasureType.Imperial ? $"{Math.Round(xAxis / Inch.Inch.ToMm(1), 0)}" : $"{xAxis}";
            var yLabel = axisSettings.MeasureType == MeasureType.Imperial ? $"{Math.Round(yAxis / Inch.Inch.ToMm(1), 0)}" : $"{yAxis}";
            var zLabel = axisSettings.MeasureType == MeasureType.Imperial ? $"{Math.Round(zAxis / Inch.Inch.ToMm(1), 0)}" : $"{zAxis}";

            // Labels based on measurement type and axis start point
            var xStart = axisSettings.MeasureType == MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinX), 0) : axisSettings.MinX;
            var yStart = axisSettings.MeasureType == MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinY), 0) : axisSettings.MinY;
            var zStart = axisSettings.MeasureType == MeasureType.Imperial ? Math.Round(Conversion.MmToInches(axisSettings.MinZ), 0) : axisSettings.MinZ;
            var startLabel = $"_Start_{xStart}x{yStart}x{zStart}".Replace("-", "Neg");
            // Create the Axis Module
            var axisModule = new CustomAxis();
            var unit = axisSettings.MeasureType == MeasureType.Imperial ? "in" : "mm";
            var scale = axisSettings.MeasureType == MeasureType.Imperial ? Inch.Inch.ToMm(1) : 1;
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
            axisModule.CallingMethod = $"{axisModule.ModuleName}();";

            var sb = new StringBuilder();
            sb.AppendLine($"// {axisModule.ModuleName} {axisSettings.MeasureType} {AxisModuleFormats.ModuleComments}");
            sb.AppendLine($"module {axisModule.ModuleName.ToLower()}(colorVal, alpha) {{");
            sb.AppendLine($"    color(colorVal, alpha) {{"); // Wrap all in color
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX, axisSettings.MaxX])}{{   {AxisModuleFormats.XOffsetMarker}   }}");  // X Axis Marker
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY, axisSettings.MaxY])}{{   {AxisModuleFormats.YOffsetMarker}   }}");  // Y Axis Marker
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ, axisSettings.MaxZ])}{{   {AxisModuleFormats.ZOffsetMarker}   }}");  // Z Axis Marker
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX2, axisSettings.MaxX])}{{   {AxisModuleFormats.XOffsetMarker2}   }}");  // X Axis Marker 2
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY2, axisSettings.MaxY])}{{   {AxisModuleFormats.YOffsetMarker2}   }}");  // Y Axis Marker 2
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ2, axisSettings.MaxZ])}{{   {AxisModuleFormats.ZOffsetMarker2}   }}");  // Z Axis Marker 2
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX3, axisSettings.MaxX])}{{   {AxisModuleFormats.XOffsetMarker3}   }}");  // X Axis Marker 3
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY3, axisSettings.MaxY])}{{   {AxisModuleFormats.YOffsetMarker3}   }}");  // Y Axis Marker 3
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ3, axisSettings.MaxZ])}{{   {AxisModuleFormats.ZOffsetMarker3}   }}");  // Z Axis Marker 3
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "x", range: [axisSettings.MinX, axisSettings.IncrementX4, axisSettings.MaxX])}{{   {AxisModuleFormats.XOffsetMarker4}   }}");  // X Axis Marker 4
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "y", range: [axisSettings.MinY, axisSettings.IncrementY4, axisSettings.MaxY])}{{   {AxisModuleFormats.YOffsetMarker4}   }}");  // Y Axis Marker 4
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "z", range: [axisSettings.MinZ, axisSettings.IncrementZ4, axisSettings.MaxZ])}{{   {AxisModuleFormats.ZOffsetMarker4}   }}");  // Z Axis Marker 4
            sb.AppendLine($"         // Axis Labels"); // Create the Axis Markers - main marker used for measurements, half and quarter markers for visual reference
            sb.AppendLine($"         unit = \"{unit}\";"); // Set unit for labels
            sb.AppendLine($"         scale = {scale};\n"); // Set scale for labels
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinX, axisSettings.IncrementX, axisSettings.MaxX])}{{   {AxisModuleFormats.XOffsetLabel}   }}");  // X Axis Label
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinY, axisSettings.IncrementY, axisSettings.MaxY])}{{   {AxisModuleFormats.YOffsetLabel}   }}");  // Y Axis Label
            sb.AppendLine($"         {GetIterationHeader(scope: Iteration.For, iterator: "i", range: [axisSettings.MinZ, axisSettings.IncrementZ, axisSettings.MaxZ])}{{   {AxisModuleFormats.ZOffsetLabel}   }}");  // Z Axis Label
            sb.AppendLine($"  }}");
            sb.AppendLine($"}}");
            sb.AppendLine($"// End of {axisModule.ModuleName} Module");

            axisModule.AxisModule = sb.ToString();

            // Include the settings used to create the axis for reference
            axisModule.Settings = axisSettings;
            return axisModule;
        }
    }
}