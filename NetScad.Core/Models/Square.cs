using NetScad.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetScad.Core.Models
{
    public partial class Square(double w, double h, bool center = false) : IScadObject
    {
        public double Width => w;
        public double Height => h;
        public bool Center => center;

        public string OSCADMethod => $"square([{w}, {h}]{(center ? $", center = {center.ToString().ToLower()}" : "")});";
    }
}
