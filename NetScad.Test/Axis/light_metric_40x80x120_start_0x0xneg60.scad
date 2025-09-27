// Light_Metric_40x80x120_Start_0x0xNeg60 Metric NetScad.Core Axis Module
// Creates a 3D axis with labeled measurements along the X, Y, and Z axes.
// Parameters:
// - MeasureType: 'Metric' for millimeters or 'Imperial' for inches (default: Metric)
// - IncrementX, IncrementY, IncrementZ: Spacing between labels on each axis (default: 1.5875mm)
// - MinX, MaxX: Minimum and maximum values for the X axis (default: 0 to 300mm)
// - MinY, MaxY: Minimum and maximum values for the Y axis (default: 0 to 300mm)
// - MinZ, MaxZ: Minimum and maximum values for the Z axis (default: 0 to 300mm)

module light_metric_40x80x120_start_0x0xneg60(colorVal, alpha) {
    color(colorVal, alpha) {
             for (x = [0:20:40])
{   if(x != 0)
 translate([x - .2, -8.75, -.01]) cube([0.2, 8.75, 0.02]);   }
             for (y = [0:20:80])
{   if(y != 0)
 translate([-8.75, y - .2, -.01]) cube([8.75, 0.2, 0.02]);   }
             for (z = [-60:20:60])
{   if(z != 0)
 translate([-7.5, -7.5, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 7.5]);   }
             for (x = [0:10:40])
{   if(x != 0)
 translate([x - .2, -5, -.01]) cube([0.2, 5, 0.02]);   }
             for (y = [0:10:80])
{   if(y != 0)
 translate([-5, y - .2, -.01]) cube([5, 0.2, 0.02]);   }
             for (z = [-60:10:60])
{   if(z != 0)
 translate([-3.75, -3.75, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 5]);   }
             for (x = [0:5:40])
{   if(x != 0)
 translate([x - .2, -2.5, -.01]) cube([0.2, 2.5, 0.02]);   }
             for (y = [0:5:80])
{   if(y != 0)
 translate([-2.5, y - .2, -.01]) cube([2.5, 0.2, 0.02]);   }
             for (z = [-60:5:60])
{   if(z != 0)
 translate([-1.75, -1.75, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 2.5]);   }
             for (x = [0:1:40])
{   if(x != 0)
 translate([x - .2, -1.25, -.01]) cube([0.2, 1.25, 0.02]);   }
             for (y = [0:1:80])
{   if(y != 0)
 translate([-1.25, y - .2, -.01]) cube([1.25, 0.2, 0.02]);   }
             for (z = [-60:1:60])
{   if(z != 0)
 translate([-.875, -.875, z-.01]) rotate([90, 45, 135]) cube([0.2, 0.02, 1.25]);   }
             // Axis Labels
             unit = "mm";
             scale = 1;
             for (i = [0:20:40])
{   if(i != 0)
 translate([i - 0.875, -10, -.01]) linear_extrude(0.02) rotate(270) text(str(i/scale, unit), size=2);   }
             for (i = [0:20:80])
{   if(i != 0)
 translate([-10, i + 0.875, -.01]) linear_extrude(0.02) rotate(180) text(str(i/scale, unit), size=2);   }
             for (i = [-60:20:60])
{   if(i != 0)
 translate([-8.75, -8.75, i - .875]) rotate([0,45,135]) linear_extrude(0.02) rotate(90) text(str(i/scale, unit), size=1.75);   }
  }
}
// End of Light_Metric_40x80x120_Start_0x0xNeg60 Module
