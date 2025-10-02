// Axes module:
use <Axes/axes.scad>;

// Uncomment one of the following to display the desired axis style and scale

// Metric 1mm increments
//Get_Dark_Metric_300x300x300_Origin();
//Get_Light_Metric_300x300x300_Origin();

// 1/32 inch increments
Get_Dark_Imperial_12x12x12_Origin();
//Get_Light_Imperial_12x12x12_Origin();

// Basic shapes for scale reference - 1 inch (25.4mm) cube
  for (y = [25.4:25.4:50.8])
	color("purple", 0.25) translate ([0, y, 0]) cube ([25.4, 25.4, 25.4]);
  for (z = [0:25.4:76.2])
	color("purple", 0.25) translate ([0, 0, 25.4 - z]) cube ([25.4, 25.4, 25.4]);