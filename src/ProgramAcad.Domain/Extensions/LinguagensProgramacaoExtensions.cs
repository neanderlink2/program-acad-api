using ProgramAcad.Domain.Entities;
using System;

namespace ProgramAcad.Domain.Extensions
{
    public static class LinguagensProgramacaoExtensions
    {

        public static LinguagensProgramacao GetLinguagemProgramacaoFromCompiler(this string linguagem) => linguagem switch
        {
            var comp when comp == "python" || comp == "python3" => LinguagensProgramacao.Python,
            "csharp" => LinguagensProgramacao.CSharp,            
            "c" => LinguagensProgramacao.C,
            "java" => LinguagensProgramacao.Java,
            "nodejs" => LinguagensProgramacao.JavaScript,
            _ => throw new InvalidOperationException("Linguagem de programação não reconhecida.")
        };
    }
}
