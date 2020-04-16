using System;

namespace ProgramAcad.Domain.Entities
{
    public class AlgoritmoResolvido
    {
        public AlgoritmoResolvido(Guid idUsuario, Guid idAlgoritmo, int idLinguagem, 
            DateTime dataConclusao)
        {
            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            IdAlgoritmo = idAlgoritmo;
            IdLinguagem = idLinguagem;
            DataConclusao = dataConclusao;
        }

        public Guid Id { get; protected set; }
        public Guid IdUsuario { get; protected set; }
        public Guid IdAlgoritmo { get; protected set; }
        public int IdLinguagem { get; protected set; }
        public DateTime DataConclusao { get; protected set; }

        public Usuario Usuario { get; set; }
        public Algoritmo Algoritmo { get; set; }
        public LinguagemProgramacao LinguagemProgramacao { get; set; }
    }
}
