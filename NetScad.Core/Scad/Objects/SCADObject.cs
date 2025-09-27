using NetScad.Core.SCAD.Utility;
using System.Collections.Generic;

namespace NetScad.Core.SCAD.Objects
{
    public static class SCADObject
    {
        public static string Module(string name, string content = "", List<string>? parameters = null)
        {
            return $"{BlockStatement.GetModule(name: name, parameters: parameters, content: content)}\n";
        }
    }
}
