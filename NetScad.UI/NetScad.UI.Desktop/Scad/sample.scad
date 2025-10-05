// Axes module:
use <Axes/axes.scad>;

// Uncomment one of the following to display the desired axis style and scale

// Metric 1mm increments
//Get_Light_300x300x300_MM_Orig_0x0x0();
//Get_Dark_300x300x300_MM_Orig_0x0x0();
//Get_Light_100x80x120_MM_Orig_N40x0xN40();
//Get_Dark_100x80x120_MM_Orig_N40x0xN40();
//Get_Dark_480x480x480_MM_Orig_N160xN160xN160();
//Get_Light_480x480x480_MM_Orig_N160xN160xN160();


// 1/32 inch increments
//Get_Dark_12x12x12_Inch_Orig_0x0x0();
//Get_Light_12x12x12_Inch_Orig_0x0x0();
Get_Dark_3x3x4_Inch_Orig_N1x0xN1();
//Get_Light_3x3x4_Inch_Orig_N1x0xN1();
//Get_Dark_18x18x18_Inch_Orig_N6xN6xN6();
//Get_Light_18x18x18_Inch_Orig_N6xN6xN6();


// Basic shapes for scale reference - 1 inch (25.4mm) cube
for (x = [-25.4:25.4:25.4])
	color("darkred", 1) translate ([x, 25.4, 25.4]) cube ([25.4, 25.4, 25.4]);

for (y = [-25.4:25.4:25.4])
	color("darkred", 1) translate ([0, y+25.4,25.4]) cube ([25.4, 25.4, 25.4]);

for (z = [0:25.4:76.2])
	color("purple", 1) translate ([0, 25.4, 50.8 - z]) cube ([25.4, 25.4, 25.4]);