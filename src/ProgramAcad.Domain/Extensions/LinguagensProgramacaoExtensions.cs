using ProgramAcad.Domain.Entities;
using System;

namespace ProgramAcad.Domain.Extensions
{
    public static class LinguagensProgramacaoExtensions
    {

        public static LinguagemProgramacao GetLinguagemProgramacaoFromCompiler(this string linguagem) => linguagem switch
        {
            var comp when comp == "python" || comp == "python3" => LinguagemProgramacao.Python,
            "csharp" => LinguagemProgramacao.CSharp,            
            "c" => LinguagemProgramacao.C,
            "java" => LinguagemProgramacao.Java,
            "nodejs" => LinguagemProgramacao.JavaScript,
            _ => throw new InvalidOperationException("Linguagem de programação não reconhecida.")
        };
    }
}
