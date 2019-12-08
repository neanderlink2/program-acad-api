using ProgramAcad.Common.Attributes;
using System.ComponentModel;

namespace ProgramAcad.Domain.Entities
{
    public enum LinguagensProgramacao
    {
        [Description("csharp")]
        [CompilerType("3")]
        CSharp = 1,

        [Description("python3")]
        [CompilerType("3")]
        Python = 2,

        [Description("c")]
        [CompilerType("2")]
        C = 3,

        [Description("java")]
        [CompilerType("3")]
        Java = 4,

        [Description("nodejs")]
        [CompilerType("3")]
        JavaScript = 5
    }
}
