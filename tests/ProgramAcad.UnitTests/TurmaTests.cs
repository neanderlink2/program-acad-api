using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;
using ProgramAcad.Services.Modules.Turmas.Commands;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using ProgramAcad.UnitTests.Fakers;
using System.Linq;
using Xunit;

namespace ProgramAcad.UnitTests
{
    public class TurmaTests
    {
        [Fact]
        public void Criar_UmaTurma_Sucesso()
        {
            var services = ServicesBuilder.GetServices();
            var usuarioRepository = services.GetRequiredService<IUsuarioRepository>();
            var admin = usuarioRepository.GetSingleAsync(x => x.Nickname == "admin");
            admin.Wait();
            var command = services.GetRequiredService<CriarTurmaCommand>();
            var turma = TurmaFaker.CreateCriarTurmaDTO(admin.Result.Email);
            command.ExecuteAsync(turma).Wait();

            var turmaRepository = services.GetRequiredService<ProgramAcadDataContext>();
            Assert.Equal(1, turmaRepository.Set<Turma>().Count());
        }

        [Fact]
        public void Criar_VariasTurma_Sucesso()
        {
            var services = ServicesBuilder.GetServices();
            var usuarioRepository = services.GetRequiredService<IUsuarioRepository>();
            var admin = usuarioRepository.GetSingleAsync(x => x.Nickname == "admin");
            admin.Wait();
            var command = services.GetRequiredService<CriarTurmaCommand>();

            var turmas = TurmaFaker.CreateCriarTurmaDTO(admin.Result.Email).Generate(5);

            foreach (var turma in turmas)
            {
                command.ExecuteAsync(turma).Wait();
            }

            var turmaRepository = services.GetRequiredService<ITurmaRepository>();
            Assert.Equal(5, turmaRepository.Count());
        }

        [Fact]
        public void Criar_UmaTurma_NomeInvalido()
        {
            var services = ServicesBuilder.GetServices();
            var usuarioRepository = services.GetRequiredService<IUsuarioRepository>();
            var admin = usuarioRepository.GetSingleAsync(x => x.Nickname == "admin");
            admin.Wait();
            var turmaRepository = services.GetRequiredService<ITurmaRepository>();
            var command = services.GetRequiredService<CriarTurmaCommand>();

            var turma = TurmaFaker.CreateCriarTurmaDTO(admin.Result.Email).Generate();
            turma.NomeTurma = "a";

            command.ExecuteAsync(turma).Wait();

            Assert.Equal(0, turmaRepository.Count());

            var notifications = services.GetRequiredService<DomainNotificationManager>();
            Assert.True(notifications.HasNotifications());
        }

        [Fact]
        public void Editar_Turma_Sucesso()
        {
            var services = ServicesBuilder.GetServices();
            var usuarioRepository = services.GetRequiredService<IUsuarioRepository>();
            var admin = usuarioRepository.GetSingleAsync(x => x.Nickname == "admin");
            admin.Wait();
            var command = services.GetRequiredService<CriarTurmaCommand>();
            var turma = TurmaFaker.CreateCriarTurmaDTO(admin.Result.Email).Generate();
            command.ExecuteAsync(turma).Wait();

            var turmaRepository = services.GetRequiredService<ITurmaRepository>();

            Assert.Equal(1, turmaRepository.Count());

            var turmaSalva = turmaRepository.GetSingle(x => x.Nome.ToUpper() == turma.NomeTurma.ToUpper());

            var turmaEditada = new EditarTurmaDTO
            {
                CapacidadeAlunos = 250,
                DataHoraTermino = turmaSalva.DataTermino,
                UrlImagem = turmaSalva.UrlImagemTurma,
                NomeTurma = "Turma editada",
                Id = turmaSalva.Id
            };

            var commandEdit = services.GetRequiredService<EditarTurmaCommand>();
            commandEdit.ExecuteAsync(turmaEditada).Wait();

            var turmaEditadaSalva = turmaRepository.GetSingle(x => x.Nome.ToUpper() == "TURMA EDITADA");

            Assert.Equal(1, turmaRepository.Count());
            Assert.Equal("Turma editada", turmaEditadaSalva.Nome);
        }
    }
}