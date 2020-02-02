using Newtonsoft.Json;
using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class SolicitarAcessoTurmaDTO
    {
        public string EmailUsuario { get; set; }
        public Guid IdTurma { get; set; }
        [JsonIgnore]
        public DateTime? DataIngresso { get; set; }
    }
}
