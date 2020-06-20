using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class CasoTeste
    {
        public CasoTeste(IEnumerable<string> entradaEsperada, IEnumerable<string> saidaEsperada, int tempoMaximoDeExecucao, Guid idAlgoritmo)
        {
            Id = Guid.NewGuid();
            EntradaEsperada = entradaEsperada;
            SaidaEsperada = saidaEsperada;
            TempoMaximoDeExecucao = tempoMaximoDeExecucao;
            IdAlgoritmo = idAlgoritmo;

            ExecucoesTeste = new List<ExecucaoTeste>();
        }

        public Guid Id { get; protected set; }
        public IEnumerable<string> EntradaEsperada { get; protected set; }
        public IEnumerable<string> SaidaEsperada { get; protected set; }
        public int TempoMaximoDeExecucao { get; protected set; }
        public Guid IdAlgoritmo { get; protected set; }

        public Algoritmo AlgoritmoVinculado { get; set; }
        public ICollection<ExecucaoTeste> ExecucoesTeste { get; set; }

        public void Update(IEnumerable<string> entradaEsperada, IEnumerable<string> saidaEsperada, int tempoMaximoDeExecucao)
        {
            EntradaEsperada = entradaEsperada;
            SaidaEsperada = saidaEsperada;
            TempoMaximoDeExecucao = tempoMaximoDeExecucao;
        }
    }
}
