using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.UnitTests.Fakers;
using Xunit;

namespace ProgramAcad.UnitTests
{
    public class UsuarioTests
    {
        [Fact]
        public void Criar_Usuario_Sucesso()
        {
            var repository = ServicesBuilder.GetServices().GetRequiredService<IUsuarioRepository>();
            var uow = ServicesBuilder.GetServices().GetRequiredService<IUnitOfWork>();
            var usuario = UsuarioFaker.CreateEntity().Generate();

            repository.AddAsync(usuario);
            uow.Commit().Wait();

            Assert.NotEqual(0, repository.Count());
        }
    }
}
