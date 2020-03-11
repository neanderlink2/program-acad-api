using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Domain.Contracts.Repositories;
using System;
using Xunit;

namespace ProgramAcad.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var repository = ServicesBuilder.GetServices().GetRequiredService<IAlgoritmoRepository>();

            Assert.NotNull(repository);
        }
    }
}
