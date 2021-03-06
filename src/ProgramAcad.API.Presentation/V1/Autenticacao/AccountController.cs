﻿using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.V1.Autenticacao
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : ApiBaseController
    {
        private readonly IAuthAdminAppService _authAdminAppService;

        public AccountController(IAuthAdminAppService authAdminAppService, DomainNotificationManager notifyManager)
             : base(notifyManager)
        {
            _authAdminAppService = authAdminAppService;
        }

        [HttpGet("concluidos/algoritmo")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarAlgoritmoResolvidoDTO[]))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public IActionResult GetAlgoritmosResolvidos([FromServices]IAlgoritmoResolvidoRepository algoritmoResolvidoRepository)
        {
            var emailUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value.ToUpper();
            var algoritmosResolvidos = algoritmoResolvidoRepository
                .GetMany(x => x.Usuario.Email.ToUpper() == emailUsuario)
                .Include(x => x.Algoritmo)
                    .ThenInclude(x => x.NivelDificuldade)
                .Include(x => x.Algoritmo)
                    .ThenInclude(x => x.CasosDeTeste)
                        .ThenInclude((CasoTeste c) => c.ExecucoesTeste)
                .Select(x => new ListarAlgoritmoResolvidoDTO
                {
                    DataConclusao = x.DataConclusao,
                    PontosRecebidos = x.Algoritmo.NivelDificuldade.PontosReceber,
                    //PercentAcertos = (x.Algoritmo.CasosDeTeste.SelectMany(c => c.ExecucoesTeste).Count(x => x.Sucesso) / x.Algoritmo.CasosDeTeste.SelectMany(c => c.ExecucoesTeste).Count()) * 100,
                    LinguagemUtilizada = x.LinguagemProgramacao.ApiIdentifier,
                    NomeAlgoritmo = x.Algoritmo.Titulo,
                    DescricaoNivelDificuldade = x.Algoritmo.NivelDificuldade.Name,
                    NomeTurma = x.Algoritmo.TurmaPertencente.Nome,
                    NomeUsuario = x.Usuario.NomeCompleto,
                    Testes = x.Algoritmo.CasosDeTeste.SelectMany(c => c.ExecucoesTeste)
                        .Select(c => new ExecucaoCasoTesteDTO
                        {
                            IdAlgoritmo = x.IdAlgoritmo,
                            IdCasoTeste = c.IdCasoTeste,
                            IdUsuario = c.IdUsuario,
                            LinguagemUtilizada = x.LinguagemProgramacao.ApiIdentifier,
                            Sucesso = c.Sucesso,
                            TempoExecucao = Math.Round(c.TempoExecucao, 2)
                        })
                });

            return Response(algoritmosResolvidos);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarUsuarioDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> CreateAccount(CadastrarUsuarioDTO usuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                usuario.Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                usuario.NomeCompleto = User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name).Value;
                var usuarioCriado = await _authAdminAppService.RegisterUserExternalAsync(usuario);
                return Response(usuarioCriado);
            }
            var usuarioCriadoSenha = await _authAdminAppService.RegisterUserPasswordAsync(usuario);
            return Response(usuarioCriadoSenha);
        }

        [HttpPut("{idUsuario}/instrutor")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarUsuarioDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> CreateTeacherAccount(Guid idUsuario)
        {
            var usuarioAtualizado = await _authAdminAppService.UpgradeToTeacherAccount(idUsuario);
            return Response(usuarioAtualizado);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> UpdateUser(AtualizarUsuarioDTO usuario)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _authAdminAppService.UpdateUserAsync(email, usuario);
            return ResponseNoContent();
        }

        [HttpPatch("password")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> ChangePassword(TrocaSenhaDTO trocaSenha)
        {
            await _authAdminAppService.ChangePasswordAsync(trocaSenha);
            return ResponseNoContent();
        }
    }
}