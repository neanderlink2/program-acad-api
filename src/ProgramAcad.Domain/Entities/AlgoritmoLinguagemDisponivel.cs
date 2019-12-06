using System;

namespace ProgramAcad.Domain.Entities
{
    public class AlgoritmoLinguagemDisponivel
    {
        public AlgoritmoLinguagemDisponivel(Guid idAlgoritmo, LinguagensProgramacao idLinguagem)
        {
            IdAlgoritmo = idAlgoritmo;
            IdLinguagem = idLinguagem;
        }

        public Guid IdAlgoritmo { get; protected set; }
        public LinguagensProgramacao IdLinguagem { get; protected set; }

        public Algoritmo Algoritmo { get; set; }        
    }
}
