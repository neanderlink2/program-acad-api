using FluentValidation;

namespace ProgramAcad.Common.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> Senha<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotNull().WithMessage("A senha é obrigatória.")
                .Matches("[A-Z]").WithMessage("A senha deve conter uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter um número.")
                .Matches("[!@#$%&*)(]").WithMessage("A senha deve conter um caracter especial.")
                .MinimumLength(8).WithMessage("A senha deve possuir no mínimo 8 caracteres.");
        }

        public static IRuleBuilderOptions<T, string> Sexo<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .MinimumLength(1).WithMessage("O sexo deve possuir no mínimo 1 caracter.")
                .MaximumLength(1).WithMessage("o sexo deve possuir no máximo 1 caracter.")
                .Must(x => x.ToUpper().Equals("M") || x.ToUpper().Equals("F") || x.ToUpper().Equals("U"))
                    .WithMessage("Sexo não identificado.");
        }

        public static IRuleBuilderOptions<T, string> Cep<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .MinimumLength(9).WithMessage("O CEP deve possuir no mínimo 9 caracter.")
                .MaximumLength(12).WithMessage("o CEP deve possuir no máximo 12 caracter.")
                .Matches(@"[0-9]{5}-[\d]{3}")
                    .WithMessage(x => $"O CEP '{x}' não é válido.");
        }

        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .MinimumLength(11).WithMessage("O CPF deve possuir no mínimo 11 caracter.")
                .MaximumLength(15).WithMessage("o CPF deve possuir no máximo 15 caracter.")
                .Matches(@"\d{3}[.\s]?\d{3}[.\s]?\d{3}[-.\s]?\d{2}")
                    .WithMessage(x => $"O CPF '{x}' não é válido.");
        }

        public static IRuleBuilderOptions<T, string> Url<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .Matches(@"^(?:(?:https?|ftp)://)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\x{00a1}-\x{ffff}0-9]+-?)*[a-z\x{00a1}-\x{ffff}0-9]+)(?:\.(?:[a-z\x{00a1}-\x{ffff}0-9]+-?)*[a-z\x{00a1}-\x{ffff}0-9]+)*(?:\.(?:[a-z\x{00a1}-\x{ffff}]{2,})))(?::\d{2,5})?(?:/[^\s]*)?$");
        }
    }
}
