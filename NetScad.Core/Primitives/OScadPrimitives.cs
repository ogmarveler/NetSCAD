using NetScad.Core.Interfaces;
using NetScad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace NetScad.Core.Primitives
{
    public enum OScad3D
    {
        Cube,       // Rectangular prism
        Cylinder,   // Cylinders, cones
        Sphere,     // Spherical solid
        Polyhedron, // Custom 3D shape from points and faces
        Surface,    // 3D heightmap from 2D data
        RoundedCube, // Cube with rounded edges on x/y axes
        RoundedCylinder, // Cylinder with rounded edges
        RoundedSphere, // Sphere with rounded edges
        RoundedPolyhedron, // Polyhedron with rounded edges
        RoundedSurface // Surface with rounded edges       
    }

    public enum OScad2D
    {
        Square,    // Rectangular 2D shape
        Circle,    // Circular 2D shape
        Polygon    // Custom 2D shape from points and paths
    }

    public enum OScad1D
    {
        Line,      // Straight line between two points
        Arc        // Curved line segment
    }

    public enum OScadSpecial
    {
        Text,      // 2D or 3D text
        Import     // Import external 2D/3D model files
    }

    public enum OScadDimension
    {
        D1,
        D2,
        D3,
        Special
    }

    public enum OScadTransform
    {
        Translate,
        Rotate,
        Scale,
        Mirror,
        Resize,
        Multmatrix
    }

    public enum OScadBooleanOperation
    {
        Union,
        Difference,
        Intersection,
        Minkowski,
        Hull
    }

    public enum OScadIteration
    {
        For
    }

    public static class ScadExtensions
    {
        public static IScadObject ToScadObject(this OScad3D self, params object[] parameters)
        {
            switch (self)
            {
                case OScad3D.Cube:
                    if (parameters.Length != 3) throw new ArgumentException("Cube requires 3 parameters: length, width, height");
                    return new Cube((double)parameters[0], (double)parameters[1], (double)parameters[2]);
                case OScad3D.Cylinder:
                    var len = parameters.Length;
                    if (len < 2 || len > 5) throw new ArgumentException("Cylinder requires 2-5 parameters: r, h, [r1], [r2], [resolution]");
                    double r = (double)parameters[0];
                    double h = (double)parameters[1];
                    double? r1 = len > 2 ? (double?)parameters[2] : null;
                    double? r2 = len > 3 ? (double?)parameters[3] : null;
                    double res = len > 4 ? (double)parameters[4] : 100;
                    return new Cylinder(r, h, r1, r2, res);
                case OScad3D.Sphere:
                    if (parameters.Length > 2) throw new ArgumentException("Sphere requires 1-2 parameters: r, [resolution]");
                    double sr = (double)parameters[0];
                    double sres = parameters.Length > 1 ? (double)parameters[1] : 100;
                    return new Sphere(sr, sres);
                case OScad3D.Polyhedron:
                    if (parameters.Length != 3) throw new ArgumentException("Polyhedron requires 3 parameters: points (List<List<double>>), faces (List<List<int>>), [convexity]");
                    var points = (List<List<double>>)parameters[0];
                    var faces = (List<List<int>>)parameters[1];
                    int conv = parameters.Length > 2 ? (int)parameters[2] : 1;
                    return new Polyhedron(points, faces, conv);
                case OScad3D.Surface:
                    if (parameters.Length < 1 || parameters.Length > 3) throw new ArgumentException("Surface requires 1-3 parameters: file (string), [center (bool)], [convexity (int)]");
                    string file = (string)parameters[0];
                    bool center = parameters.Length > 1 ? (bool)parameters[1] : false;
                    int sconv = parameters.Length > 2 ? (int)parameters[2] : 1;
                    return new Surface(file, center, sconv);
                case OScad3D.RoundedCube:
                    if (parameters.Length < 4 || parameters.Length > 6) throw new ArgumentException("RoundedCube requires 4-6 parameters: length, width, height, round_r, [round_h], [resolution]");
                    double l = (double)parameters[0];
                    double w = (double)parameters[1];
                    double hCube = (double)parameters[2];
                    double round_r = (double)parameters[3];
                    double round_h = parameters.Length > 4 ? (double)parameters[4] : 0.001;
                    double cubeRes = parameters.Length > 5 ? (double)parameters[5] : 200;
                    return new RoundedCube(l, w, hCube, round_r, round_h, cubeRes);
                case OScad3D.RoundedCylinder:
                    if (parameters.Length < 3 || parameters.Length > 7) throw new ArgumentException("RoundedCylinder requires 3-7 parameters: r, h, round_r, [round_h], [r1], [r2], [resolution]");
                    double rc = (double)parameters[0];
                    double hc = (double)parameters[1];
                    double round_rc = (double)parameters[2];
                    double round_hc = parameters.Length > 3 ? (double)parameters[3] : 0.001;
                    double? r1c = parameters.Length > 4 ? (double?)parameters[4] : null;
                    double? r2c = parameters.Length > 5 ? (double?)parameters[5] : null;
                    double cylRes = parameters.Length > 6 ? (double)parameters[6] : 200;
                    return new RoundedCylinder(rc, hc, round_rc, round_hc, r1c, r2c, cylRes);
                default:
                    throw new ArgumentException("Unknown OScad3D type");
            }
        }

        public static IScadObject ToScadObject(this OScad2D self, params object[] parameters)
        {
            switch (self)
            {
                case OScad2D.Square:
                    if (parameters.Length < 2 || parameters.Length > 3) throw new ArgumentException("Square requires 2-3 parameters: width, height, [center (bool)]");
                    double w = (double)parameters[0];
                    double h = (double)parameters[1];
                    bool center = parameters.Length > 2 ? (bool)parameters[2] : false;
                    return new Square(w, h, center);
                case OScad2D.Circle:
                    if (parameters.Length > 2) throw new ArgumentException("Circle requires 1-2 parameters: r, [resolution]");
                    double cr = (double)parameters[0];
                    double cres = parameters.Length > 1 ? (double)parameters[1] : 100;
                    return new Circle(cr, cres);
                case OScad2D.Polygon:
                    if (parameters.Length < 1 || parameters.Length > 3) throw new ArgumentException("Polygon requires 1-3 parameters: points (List<List<double>>), [paths (List<List<int>>)], [convexity (int)]");
                    var ppoints = (List<List<double>>)parameters[0];
                    List<List<int>>? ppaths = parameters.Length > 1 ? (List<List<int>>?)parameters[1] : null;
                    int pconv = parameters.Length > 2 ? (int)parameters[2] : 1;
                    return new Polygon(ppoints, ppaths, pconv);
                default:
                    throw new ArgumentException("Unknown OScad2D type");
            }
        }

        public static IScadObject ToScadObject(this OScad1D self, params object[] parameters)
        {
            switch (self)
            {
                case OScad1D.Line:
                    if (parameters.Length != 4) throw new ArgumentException("Line requires 4 parameters: x1, y1, x2, y2");
                    return new Line((double)parameters[0], (double)parameters[1], (double)parameters[2], (double)parameters[3]);
                case OScad1D.Arc:
                    if (parameters.Length < 3 || parameters.Length > 4) throw new ArgumentException("Arc requires 3-4 parameters: r, start, end, [resolution]");
                    double ar = (double)parameters[0];
                    double start = (double)parameters[1];
                    double end = (double)parameters[2];
                    double ares = parameters.Length > 3 ? (double)parameters[3] : 100;
                    return new Arc(ar, start, end, ares);
                default:
                    throw new ArgumentException("Unknown OScad1D type");
            }
        }

        public static IScadObject ToScadObject(this OScadSpecial self, params object[] parameters)
        {
            switch (self)
            {
                case OScadSpecial.Text:
                    if (parameters.Length < 1 || parameters.Length > 10) throw new ArgumentException("Text requires 1-10 parameters: text (string), [size], [font], [halign], [valign], [spacing], [direction], [language], [script], [resolution]");
                    string ttext = (string)parameters[0];
                    double tsize = parameters.Length > 1 ? (double)parameters[1] : 10;
                    string? tfont = parameters.Length > 2 ? (string?)parameters[2] : null;
                    string thalign = parameters.Length > 3 ? (string)parameters[3] : "left";
                    string tvalign = parameters.Length > 4 ? (string)parameters[4] : "baseline";
                    double tspacing = parameters.Length > 5 ? (double)parameters[5] : 1;
                    string tdirection = parameters.Length > 6 ? (string)parameters[6] : "ltr";
                    string? tlanguage = parameters.Length > 7 ? (string?)parameters[7] : null;
                    string? tscript = parameters.Length > 8 ? (string?)parameters[8] : null;
                    double tres = parameters.Length > 9 ? (double)parameters[9] : 100;
                    return new Text(ttext, tsize, tfont, thalign, tvalign, tspacing, tdirection, tlanguage, tscript, tres);
                case OScadSpecial.Import:
                    if (parameters.Length < 1 || parameters.Length > 2) throw new ArgumentException("Import requires 1-2 parameters: file (string), [convexity]");
                    string ifile = (string)parameters[0];
                    int iconv = parameters.Length > 1 ? (int)parameters[1] : 1;
                    return new Import(ifile, iconv);
                default:
                    throw new ArgumentException("Unknown OScadSpecial type");
            }
        }

        public static IScadObject ToScadObject(this OScadTransform self, params object[] parameters)
        {
            int fixedCount;
            switch (self)
            {
                case OScadTransform.Translate:
                    fixedCount = 3;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Translate requires at least 3 parameters: x, y, z, [children...]");
                    double tx = (double)parameters[0];
                    double ty = (double)parameters[1];
                    double tz = (double)parameters[2];
                    var tchildren = parameters.Skip(fixedCount).Cast<IScadObject>().ToArray();
                    return new Translate(tx, ty, tz, tchildren);
                case OScadTransform.Rotate:
                    fixedCount = 3;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Rotate requires at least 3 parameters: ax, ay, az, [children...]");
                    double rax = (double)parameters[0];
                    double ray = (double)parameters[1];
                    double raz = (double)parameters[2];
                    var rchildren = parameters.Skip(fixedCount).Cast<IScadObject>().ToArray();
                    return new Rotate(rax, ray, raz, rchildren);
                case OScadTransform.Scale:
                    fixedCount = 3;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Scale requires at least 3 parameters: sx, sy, sz, [children...]");
                    double sx = (double)parameters[0];
                    double sy = (double)parameters[1];
                    double sz = (double)parameters[2];
                    var schildren = parameters.Skip(fixedCount).Cast<IScadObject>().ToArray();
                    return new Scale(sx, sy, sz, schildren);
                case OScadTransform.Mirror:
                    fixedCount = 3;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Mirror requires at least 3 parameters: mx, my, mz, [children...]");
                    double mx = (double)parameters[0];
                    double my = (double)parameters[1];
                    double mz = (double)parameters[2];
                    var mchildren = parameters.Skip(fixedCount).Cast<IScadObject>().ToArray();
                    return new Mirror(mx, my, mz, mchildren);
                case OScadTransform.Resize:
                    fixedCount = 3;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Resize requires at least 3 parameters: rx, ry, rz, [auto (bool)], [children...]");
                    double rx = (double)parameters[0];
                    double ry = (double)parameters[1];
                    double rz = (double)parameters[2];
                    int childStart = fixedCount;
                    bool auto = false;
                    if (parameters.Length > fixedCount && parameters[fixedCount] is bool)
                    {
                        auto = (bool)parameters[fixedCount];
                        childStart++;
                    }
                    var reschildren = parameters.Skip(childStart).Cast<IScadObject>().ToArray();
                    return new Resize(rx, ry, rz, auto, reschildren);
                case OScadTransform.Multmatrix:
                    fixedCount = 1;
                    if (parameters.Length < fixedCount) throw new ArgumentException("Multmatrix requires at least 1 parameter: matrix (List<List<double>>), [children...]");
                    var matrix = (List<List<double>>)parameters[0];
                    var mmchildren = parameters.Skip(fixedCount).Cast<IScadObject>().ToArray();
                    return new Multmatrix(matrix, mmchildren);
                default:
                    throw new ArgumentException("Unknown OScadTransform type");
            }
        }

        public static IScadObject ToScadObject(this OScadBooleanOperation self, params object[] parameters)
        {
            var children = parameters.Cast<IScadObject>().ToArray();
            switch (self)
            {
                case OScadBooleanOperation.Union:
                    return new Union(children);
                case OScadBooleanOperation.Difference:
                    return new Difference(children);
                case OScadBooleanOperation.Intersection:
                    return new Intersection(children);
                case OScadBooleanOperation.Minkowski:
                    return new Minkowski(children);
                case OScadBooleanOperation.Hull:
                    return new Hull(children);
                default:
                    throw new ArgumentException("Unknown OScadBooleanOperation type");
            }
        }

        public static IScadObject ToScadObject(this OScadIteration self, params object[] parameters)
        {
            switch (self)
            {
                case OScadIteration.For:
                    if (parameters.Length < 1) throw new ArgumentException("For requires at least 1 parameter: loopExpression (string), [body...]");
                    string loopExpr = (string)parameters[0];
                    var body = parameters.Skip(1).Cast<IScadObject>().ToArray();
                    return new ForLoop(loopExpr, body);
                default:
                    throw new ArgumentException("Unknown OScadIteration type");
            }
        }
    }
}