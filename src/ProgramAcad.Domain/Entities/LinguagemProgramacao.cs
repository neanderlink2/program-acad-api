using ProgramAcad.Common.Models;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class LinguagemProgramacao : Enumeration
    {
        protected LinguagemProgramacao()
        {

        }

        public LinguagemProgramacao(int id, string nome, string descricao, int numCompilador, string apiId) : base(id, nome)
        {
            Descricao = descricao;
            NumCompilador = numCompilador;
            ApiIdentifier = apiId;
        }

        public string Descricao { get; set; }
        public int NumCompilador { get; set; }
        public string ApiIdentifier { get; set; }

        public ICollection<AlgoritmoLinguagemDisponivel> AlgoritmosDessaLinguagem { get; set; }
        public ICollection<ExecucaoTeste> ExecucoesDessaLinguagem { get; set; }
        public ICollection<AlgoritmoResolvido> AlgoritmosResolvidosDessaLinguagem { get; set; }

        public static LinguagemProgramacao CSharp = new LinguagemProgramacaoCSharp();
        public static LinguagemProgramacao Python = new LinguagemProgramacaoPython();
        public static LinguagemProgramacao Java = new LinguagemProgramacaoJava();
        public static LinguagemProgramacao C = new LinguagemProgramacaoC();
        public static LinguagemProgramacao JavaScript = new LinguagemProgramacaoJavaScript();

        private class LinguagemProgramacaoCSharp : LinguagemProgramacao
        {
            public LinguagemProgramacaoCSharp() : base(1, "C#", "C# é uma linguagem de programação, multiparadigma, de tipagem forte, desenvolvida pela Microsoft como parte da plataforma .NET.", 3, "csharp") { }
        }

        private class LinguagemProgramacaoPython : LinguagemProgramacao
        {
            public LinguagemProgramacaoPython() : base(2, "Python", "Python é uma linguagem de programação de alto nível,[4] interpretada, de script, imperativa, orientada a objetos, funcional, de tipagem dinâmica e forte.", 3, "python3") { }
        }

        private class LinguagemProgramacaoC : LinguagemProgramacao
        {
            public LinguagemProgramacaoC() : base(3, "C", "C é uma linguagem de programação compilada de propósito geral, estruturada, imperativa, procedural e padronizada por Organização Internacional para Padronização (ISO).", 2, "c") { }
        }

        private class LinguagemProgramacaoJava : LinguagemProgramacao
        {
            public LinguagemProgramacaoJava() : base(4, "Java", "Java é uma linguagem de programação orientada a objetos desenvolvida na década de 90 por uma equipe de programadores chefiada por James Gosling, na empresa Sun Microsystems.", 3, "java") { }
        }

        private class LinguagemProgramacaoJavaScript : LinguagemProgramacao
        {
            public LinguagemProgramacaoJavaScript() : base(5, "JavaScript", "JavaScript (frequentemente abreviado como JS) é uma linguagem de programação interpretada estruturada, de script em alto nível com tipagem dinâmica fraca e multi-paradigma.", 3, "nodejs") { }
        }
    }
}
