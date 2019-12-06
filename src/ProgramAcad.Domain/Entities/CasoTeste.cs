using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class CasoTeste
    {
        public CasoTeste(string entradaEsperada, string saidaEsperada, int tempoMaximoDeExecucao, Guid idAlgoritmo)
        {
            Id = Guid.NewGuid();
            EntradaEsperada = entradaEsperada;
            SaidaEsperada = saidaEsperada;
            TempoMaximoDeExecucao = tempoMaximoDeExecucao;
            IdAlgoritmo = idAlgoritmo;
        }

        public Guid Id { get; protected set; }
        public string EntradaEsperada { get; protected set; }
        public string SaidaEsperada { get; protected set; }
        public int TempoMaximoDeExecucao { get; protected set; }
        public Guid IdAlgoritmo { get; protected set; }

        public Algoritmo AlgoritmoVinculado { get; set; }
        public ICollection<ExecucaoTeste> ExecucoesTeste { get; set; }
    }
}
