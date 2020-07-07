using ProgramAcad.Common.Models;
using ProgramAcad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAcad.Services.Modules.Algoritmos.DTOs
{
    public class UsuarioConcluiuAlgoritmoDTO
    {
        public Guid IdAlgoritmo { get; set; }
        public string NomeAlgoritmo { get; set; }
        public string NomeUsuario { get; set; }
        public string NicknameUsuario { get; set; }
        public int IdLinguagem { get; set; }
        public DateTime DataConclusao { get; set; }
        public string NomeLinguagem => Enumeration.FromValue<LinguagemProgramacao>(IdLinguagem).Name;
        public string ApiIdentifierLinguagem => Enumeration.FromValue<LinguagemProgramacao>(IdLinguagem).ApiIdentifier;
    }
}
