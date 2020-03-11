using ProgramAcad.Common.Models;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class NivelDificuldade : Enumeration
    {
        protected NivelDificuldade()
        {

        }

        public NivelDificuldade(int id, int nivel, string descricao, int pontosReceber)
        {
            Id = id;
            Nivel = nivel;
            Name = descricao;
            PontosReceber = pontosReceber;
        }

        public int Nivel { get; protected set; }
        public int PontosReceber { get; protected set; }

        public ICollection<Algoritmo> AlgoritmosDesseNivel { get; set; }

        public static NivelDificuldade MuitoFacil = new NivelDificuldadeMuitoFacil();
        public static NivelDificuldade Facil = new NivelDificuldadeFacil();
        public static NivelDificuldade Medio = new NivelDificuldadeMedio();
        public static NivelDificuldade Dificil = new NivelDificuldadeDificil();
        public static NivelDificuldade MuitoDificil = new NivelDificuldadeMuitoDificil();

        private class NivelDificuldadeMuitoFacil : NivelDificuldade
        {
            public NivelDificuldadeMuitoFacil() : base(1, 1, "Muito f�cil", 5) { }
        }

        private class NivelDificuldadeFacil : NivelDificuldade
        {
            public NivelDificuldadeFacil() : base(2, 2, "F�cil", 15) { }
        }

        private class NivelDificuldadeMedio : NivelDificuldade
        {
            public NivelDificuldadeMedio() : base(3, 3, "M�dio", 40) { }
        }

        private class NivelDificuldadeDificil : NivelDificuldade
        {
            public NivelDificuldadeDificil() : base(4, 4, "Dif�cil", 70) { }
        }

        private class NivelDificuldadeMuitoDificil : NivelDificuldade
        {
            public NivelDificuldadeMuitoDificil() : base(5, 5, "Muito dif�cil", 150) { }
        }
    }
}
