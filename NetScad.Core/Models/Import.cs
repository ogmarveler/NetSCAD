using NetScad.Core.Interfaces;

namespace NetScad.Core.Models
{
    public partial class Import(string file, int convexity = 1) : IScadObject
    {
        public string File => file;
        public int Convexity => convexity;

        public string OSCADMethod => $"import(\"{file}\", convexity = {convexity});";
    }
}
