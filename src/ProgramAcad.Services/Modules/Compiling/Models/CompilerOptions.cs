using Newtonsoft.Json;

namespace ProgramAcad.Services.Modules.Compiling.Models
{
    public class CompilerOptions
    {
        public CompilerOptions(string clientId, string clientSecret, string script, string linguagem,
            string versao, string entradas)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Script = script;
            Linguagem = linguagem;
            Versao = versao;
            Entradas = entradas;
        }

        /// <summary>
        /// Código identificador para utilizar a API
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; protected set; }

        /// <summary>
        /// Código secreto para utilizar a API.
        /// </summary>
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; protected set; }

        /// <summary>
        /// Código que será executa.
        /// </summary>
        [JsonProperty("script")]
        public string Script { get; protected set; }

        /// <summary>
        /// Linguagem de programação
        /// </summary>
        [JsonProperty("language")]
        public string Linguagem { get; protected set; }

        /// <summary>
        /// Versão do compilador
        /// </summary>
        [JsonProperty("versionIndex")]
        public string Versao { get; protected set; }

        /// <summary>
        /// Entradas para o algoritmo. Cada entrada deve estar separada por \n.
        /// </summary>
        [JsonProperty("stdin")]
        public string Entradas { get; protected set; }
    }
}
