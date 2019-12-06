using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class NivelDificuldade
    {
        public NivelDificuldade(int id, int nivel, string descricao, int pontosReceber)
        {
            Id = id;
            Nivel = nivel;
            Descricao = descricao;
            PontosReceber = pontosReceber;
        }

        public int Id { get; protected set; }
        public int Nivel { get; protected set; }
        public string Descricao { get; protected set; }
        public int PontosReceber { get; protected set; }

        public ICollection<Algoritmo> AlgoritmosDesseNivel { get; set; }        
    }
}
