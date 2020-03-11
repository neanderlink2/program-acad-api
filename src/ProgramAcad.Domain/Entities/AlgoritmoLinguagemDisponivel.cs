using System;

namespace ProgramAcad.Domain.Entities
{
    public class AlgoritmoLinguagemDisponivel
    {
        public AlgoritmoLinguagemDisponivel(Guid idAlgoritmo, int idLinguagem)
        {
            IdAlgoritmo = idAlgoritmo;
            IdLinguagem = idLinguagem;
        }

        public Guid IdAlgoritmo { get; protected set; }
        public int IdLinguagem { get; protected set; }

        public LinguagemProgramacao LinguagemProgramacao { get; set; }
        public Algoritmo Algoritmo { get; set; }        
    }
}
