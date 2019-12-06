using FluentValidation;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;

namespace ProgramAcad.Services.Modules.CasosTeste.Commands.Validations
{
    public class CriarCasosTesteValidator : AbstractValidator<CriarCasoTesteDTO>
    {
        public CriarCasosTesteValidator()
        {

        }

        public void ValidateEntradaEsperada()
        {
            RuleFor(x => x.EntradaEsperada)
                .NotNull().WithMessage("Entrada esperada deve possuir um valor.");
        }

        public void ValidateSaidaEsperada()
        {
            RuleFor(x => x.EntradaEsperada)
                .NotNull().WithMessage("Saída esperada deve possuir um valor.");
        }

        public void ValidateTempoExecucao()
        {
            RuleFor(x => x.TempoMaximoExecucao)
                .NotNull().WithMessage("Tempo máximo de execução deve possuir um valor.")
                .GreaterThan(0).WithMessage("Tempo máximo de execução deve ser maior do que zero.");
        }
    }
}
