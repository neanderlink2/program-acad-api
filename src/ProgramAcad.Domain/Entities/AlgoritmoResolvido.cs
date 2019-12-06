using System;

namespace ProgramAcad.Domain.Entities
{
    public class AlgoritmoResolvido
    {
        public AlgoritmoResolvido(Guid idUsuario, Guid idAlgoritmo, LinguagensProgramacao idLinguagem, 
            DateTime dataConclusao)
        {
            IdUsuario = idUsuario;
            IdAlgoritmo = idAlgoritmo;
            IdLinguagem = idLinguagem;
            DataConclusao = dataConclusao;
        }

        public Guid IdUsuario { get; protected set; }
        public Guid IdAlgoritmo { get; protected set; }
        public LinguagensProgramacao IdLinguagem { get; protected set; }
        public DateTime DataConclusao { get; protected set; }

        public Usuario Usuario { get; set; }
        public Algoritmo Algoritmo { get; set; }
    }
}
