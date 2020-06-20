using ProgramAcad.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramAcad.Domain.Entities
{
    public class Algoritmo
    {
        public Algoritmo(Guid idTurma, string titulo, string htmlDescricao, int idNivelDificuldade, DateTime dataCriacao)
        {
            Id = Guid.NewGuid();
            IsAtivo = true;
            IdTurma = idTurma;
            DataCriacao = dataCriacao;
            Titulo = titulo;
            HtmlDescricao = htmlDescricao;
            IdNivelDificuldade = idNivelDificuldade;

            CasosDeTeste = new List<CasoTeste>();
            LinguagensPermitidas = new List<AlgoritmoLinguagemDisponivel>();
            AlgoritmosResolvidos = new List<AlgoritmoResolvido>();
        }

        public Guid Id { get; protected set; }
        public Guid IdTurma { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public string Titulo { get; protected set; }
        public string HtmlDescricao { get; protected set; }
        public int IdNivelDificuldade { get; protected set; }
        public bool IsAtivo { get; protected set; }

        public NivelDificuldade NivelDificuldade { get; set; }
        public Turma TurmaPertencente { get; set; }

        public ICollection<CasoTeste> CasosDeTeste { get; set; }
        public ICollection<AlgoritmoLinguagemDisponivel> LinguagensPermitidas { get; set; }
        public ICollection<AlgoritmoResolvido> AlgoritmosResolvidos { get; set; }

        public void EditAlgoritmo(string titulo, string htmlDescricao, int idNivelDificuldade)
        {
            Titulo = titulo;
            HtmlDescricao = htmlDescricao;
            IdNivelDificuldade = idNivelDificuldade;
        }

        public void Deactivate()
        {
            IsAtivo = false;
        }

        public void SetLinguagensProgramacao(IEnumerable<string> identificadoresLinguagem)
        {
            LinguagensPermitidas = ObterLinguagensFromApiIdentifier(identificadoresLinguagem).ToList();
        }

        private IEnumerable<AlgoritmoLinguagemDisponivel> ObterLinguagensFromApiIdentifier(IEnumerable<string> linguagens)
        {
            //transforma o Enum em IEnumerable
            var enumLinguagens = Enumeration.GetAll<LinguagemProgramacao>();
            if (enumLinguagens.Any(x => linguagens.Contains(x.ApiIdentifier)))
            {
                foreach (var item in linguagens)
                {
                    var linguagem = enumLinguagens.FirstOrDefault(x => x.ApiIdentifier.Equals(item));
                    //Para cada linguagem solicitada, Busca o primeiro valor que está contido dentro do array de linguagens.
                    if (linguagem != default)
                    {
                        yield return new AlgoritmoLinguagemDisponivel(Id, linguagem.Id);
                    }
                }
            }
            //Se não possuir nenhuma linguagem, retornar Enumerable vazio.
            yield break;
        }
    }
}
