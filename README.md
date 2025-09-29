# NetSCAD - Tools for OpenSCAD built with .NET

## Description
Contains a collection of tools and libraries to facilitate 3D modeling in OpenSCAD using .NET languages. This project aims to simplify the process of creating complex 3D models by providing reusable components and utilities. Currently, it includes libraries for automating the creation of custom Imperial and Metric axes. This allows for more precise 3D modeling used in 3D printing.

## Table of Contents
- [Libraries](#libraries)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Parameters](#parameters)
- [Measurements](#measurements)
- [Output](#output)
- [Examples](#examples)

## Libraries
NetSCAD includes the following libraries:
- **NetSCAD.Core**: Core functionalities and base classes for 3D axis modeling.
- **NetSCAD.Axis**: Automation of custom Imperial and Metric axes in OpenSCAD.

Core uses .NET Standard 2.1. The Axis console app is .NET 8.

## Prerequisites
You need to have the following installed: 

1. [OpenSCAD](https://openscad.org/downloads.html)
2. [.NET 8.0 SDK or later.](https://dotnet.microsoft.com/en-us/download)

## Installation

1. Clone the repo and add the **NetSCAD.Axis** and **NetSCAD.Core** libraries as references to your main .NET project.

2. Unless using the ``NetSCAD.Axis`` project included, create a ``Scad/Axis`` folder in your main project. Axes will be stored in this folder, and the ``axes.scad`` file will be where to call the modules from. References to each one, along with dimensions for axes are saved here.

3. If using VS 2022, you might need to create a blank ``axes.scad`` file outside of VS2022, and then put it in the ``Scad/Axis`` folder. The ``NetSCAD.Axis`` project already has the folder and file, so if using the ones included with the libraries, then this step can be ignored.

4. A ``sample.scad`` will be in the ``Scad`` folder, which shows an example of calling the various axis modules.

```bash
gh repo clone ogmarveler/NetSCAD
```

## Usage
Varying axes of different sizes, measurement types, colors, and combination of both metric and imperial measurements can be applied within the same SCAD project. Axes are managed in an aggregate SCAD file and are called as modules within your SCAD project. The libraries manage updates to existing axes as well as provide the ability for multiple axis types to be used in your project.

Example of generating new axis
```csharp
// Namespaces used for this module
using NetScad.Core.Measurements;
using NetScad.Core.SCAD.Modules;
using NetScad.Core.Utility;
using static NetScad.Core.Measurements.FractionalInch;
using static NetScad.Core.SCAD.Utility.AxisConfig;
using static NetScad.Core.SCAD.Models.Selector;

// Apply custom axis settings - will save to a SCAD file as a module
// For Metric, Axis will set measurements to 20mm, 10mm, 5mm, 1mm increments.
// For Imperial, Axis will be set to 1/4, 1/8, 1/16, and 1/32 inch increments.
// For larger measurements, adjust Min, Max, and Scale accordingly to keep axis readable.
// Cache if called multiple times
var axisSettings = new AxisSettings(
     outputDirectory: PathHelper.GetProjectRoot(), // ..NetScad.Axis/Scad/Axis
     backgroundType: BackgroundType.Dark, // Axis color will be white or black
     measureType: MeasureType.Imperial, // inches
     minX: 0, // for positive only axis, max must be positive
     maxX: Inch1.ToMm(3), // can set as inches
     minY: Inch1.ToMm(-3), // can set negative axis starting point
     maxY: 70, // can be set as millimeters, will adjust to 76.2mm if imperial measure type
     minZ: -Inch4.ToMm(3), // Fractional axes can be used (3/4")
     maxZ: 0 // for negative only axis, min must be negative
);
// Generates (or updates a previously saved axis) and stores it in an aggregate axes file
// The new axis can then be called as a module from your main SCAD project file
await GUI.SetAxis(axisSettings);
```

## Parameters
The main two functions used are a ``AxisSettings`` class that holds all the configurations details set by the user. The other is the create axis function, ``GUI.SetAxis``. The ``AxisSettings`` function can be called with the following parameters below, or it can be called with only the ``outputDirectory: PathHelper.GetProjectRoot()``. If being called without any coordinates, the default grid will output.

Default grid (no coordinates supplied): 300x300x300mm, Metric, Light Background (OpenSCAD)

```csharp
// Output Directory (Required):
string outputDirectory;

// X axis (Optional):
double minX;
double maxX;

// Y axis (Optional):
double minY;
double maxY;

// Z axis (Optional):
double minZ;
double maxZ;

// Measure Type (Optional):
MeasureType.Imperial;
MeasureType.Metric;

// Axis Color based on OpenSCAD Background (Optional):
BackgroundType.Dark;
BackgroundType.Light;
```

## Measurements
#### Metric: axis measurements are 20mm, 10mm, 5mm, 1mm increments.
#### Imperial: axis will are 1/4, 1/8, 1/16, 1/32 inch increments.

An ``enum FractionalInch`` selector allows for supplying Min and Max coordinates on all 3 axes (X, Y, and Z) to be input in 1/32 - 1 inch units. This makes it easier to not have to do the conversions before hand. The functions will automatically adjust supplied inputs to the nearest increment so that the axes display identical to how a ruler would evenly space increments. For example, if 150mm is supplied as an input, but imperial measurement type is selected, the function will adjust it to 152.4mm so that all increments are exact to the desired axes output. The same concept occurs with metric inputs supplied as well.

```csharp
Inch1.ToMm(6); // 6 inches or 152.4mm
Inch2.ToMm(1); // 1/2" or 12.7mm
Inch4.ToMm(3); // 3/4" or 19.05mm
```

## Output
The structure of the axis SCAD files are as follows. Only one reference in your main SCAD files needs to be made, and this is to the ``Axis/axes.scad`` file that stores all of the generated axis modules. You will need to apply the statement ``use <Axis/axes.scad>;`` in your main file, and then just simply call the axis module of your choice. The module can be called without any arguments, or with a different ``color`` and/or ``alpha`` parameters.

```
| Files                       | In Your SCAD File       | Optional parameters |
| --------------------------- |:-----------------------:| -------------------:|
| Axis/axes.scad              | use <Axis/axes.scad>;   |                     |
| Axis/custom__axis_name.scad | Get_Custom_Axis_Name(); | colorVal, alpha     |
```

## Examples
This is what the module of a generated axis looks like. 
The libraries allow for tailoring the X, Y, and Z axes to be different lengths and have different beginning and end points. 
This is primarily for use cases where objects may be very on one axis but not on another. 
Another feature is the ability to measure insets from screws, or anything that would need to be referenced for multi-part 3D prints.

```scad
// 3D Axis Module - Light Metric 300x300x300 Origin
// Calling Method: Get_Light_Metric_300x300x300_Origin();
// Settings: MeasureType=Metric, MinX=0, MaxX=300, MinY=0, MaxY=300, MinZ=0, MaxZ=300
include <light_metric_300x300x300_origin.scad>;
module Get_Light_Metric_300x300x300_Origin(colorVal = "Black", alpha = 1) {
  light_metric_300x300x300_origin(colorVal = colorVal, alpha = alpha);
}
```

### Imperial Axis 12in x 12in x 12in
![Imperial Axis 12x12x12](https://github.com/ogmarveler/NetSCAD/blob/117473f9584d88e811d80c64a007ab3d35684cfd/NetScad.Axis/Images/oscadAxis_imperial.PNG)

### Metric Axis 300mm x 300mm x 300mm
![Metric Axis 12x12x12](https://github.com/ogmarveler/NetSCAD/blob/117473f9584d88e811d80c64a007ab3d35684cfd/NetScad.Axis/Images/oscadAxis_metric.PNG)

### Imperial Different X,Y,Z Axis 3in x 3in x 4in
![Imperial Different X,Y,Z Axis 12x12x12](https://github.com/ogmarveler/NetSCAD/blob/117473f9584d88e811d80c64a007ab3d35684cfd/NetScad.Axis/Images/oscadAxis_imperial_custom.PNG)

### Metric Different X,Y,Z Axis 80mm x 80mm x 120mm
![Metric Different X,Y,Z Axis 12x12x12](https://github.com/ogmarveler/NetSCAD/blob/117473f9584d88e811d80c64a007ab3d35684cfd/NetScad.Axis/Images/oscadAxis_metric_custom.PNG)

