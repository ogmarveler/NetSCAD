using NetScad.Core.Interfaces;
using System.Linq;

namespace NetScad.Core.Models
{
    public partial class ForLoop(string loopExpression, params IScadObject[] body) : IScadObject
    {
        public string LoopExpression => loopExpression;
        public IScadObject[] Body => body;

        public string OSCADMethod => $"for({loopExpression}) {{ {string.Join("\n", body.Select(c => c.OSCADMethod))} }};";
    }
}
