using NetScad.Core.SCAD.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static NetScad.Core.SCAD.Modules.Axis;
using static NetScad.Core.SCAD.Utility.AxisConfig;

namespace NetScad.Core.SCAD.Modules
{
    public static class GUI
    {
        // OpenSCAD GUI axis - Saves into main axes file that can be called from your main SCAD project file
        public static async Task SetAxis(AxisSettings axisSettings, bool addToExistOutputFile = false, CancellationToken cancellationToken = default)
        {
            // Validate and configure axis settings
            // Stateless - always re-check settings
            var _axisSettings = CheckAxisSettings(axisSettings);
            var _customAxis = ConfigureAxisModule(_axisSettings);
            var _moduleNameLower = _customAxis.ModuleName.ToLower();
            // Write to file - import into output SCAD file
            var _axisFilePath = Path.Combine(_customAxis.Settings.OutputFolder, $"Axis/{_moduleNameLower}.scad");
            // Since this is a code-driven module, always overwrite since it will be regenerated
            await Output.WriteToSCAD(content: _customAxis.AxisModule, filePath: _axisFilePath, overWrite: true, cancellationToken: cancellationToken);
            var axisColor = _customAxis.Settings.OpenScadColor.ToString().ToLower();
            // Store in Axes reference file
            if (!addToExistOutputFile) return;
            var _moduleComment = $"// 3D Axis Module - {_customAxis.ModuleName.Replace("_", " ")}\n" +
                $"// Calling Method: Get_{_customAxis.CallingMethod}\n" +
                $"// Settings: MeasureType={_customAxis.Settings.MeasureType}, MinX={_customAxis.Settings.MinX}, MaxX={_customAxis.Settings.MaxX}, MinY={_customAxis.Settings.MinY}, " +
                $"MaxY={_customAxis.Settings.MaxY}, MinZ={_customAxis.Settings.MinZ}, MaxZ={_customAxis.Settings.MaxZ}";
            var _scadFilePath = Path.Combine(_customAxis.Settings.OutputFolder, $"Axis/axes.scad");
            var _importStatement = $"{_moduleComment}\n" +
                $"include <{_moduleNameLower}.scad>;\n" +
                $"module Get_{_customAxis.ModuleName}(colorVal = \"{(_customAxis.Settings.BackgroundType == Selector.BackgroundType.Light ? Selector.OpenScadColor.Black : Selector.OpenScadColor.White)}\", " +
                $"alpha = {axisSettings.AxisColorAlpha}) {{\n" +
                $"  {_moduleNameLower}(colorVal = colorVal, alpha = alpha);\n" +
                $"}}\n\n";
            await Output.AppendToSCAD(content: _importStatement, filePath: _scadFilePath, cancellationToken: cancellationToken);
        }
    }
}