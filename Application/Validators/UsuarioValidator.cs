using Application.Objects.Requests.Usuario;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class UsuarioValidator
    {
        public class UsuarioCadastroValidator : AbstractValidator<UsuarioCadastroRequest>
        {
            public UsuarioCadastroValidator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email em formato inválido");
                RuleFor(x => x.Email).Must((x, email) => ValidarEmail(email)).WithMessage("Email em formato inválido");
                RuleFor(x => x.Senha).NotEmpty().WithMessage("Senha nula é inválida");
                RuleFor(x => x.Senha).Must((x, senha) => senha == x.ConfirmacaoSenha).WithMessage("As senhas não são iguais");
            }
        }

        public class UsuarioLoginValidator : AbstractValidator<UsuarioLoginRequest>
        {
            public UsuarioLoginValidator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email em formato inválido");
                RuleFor(x => x.Senha).NotEmpty().WithMessage("Email em formato inválido");
                RuleFor(x => x.Email).Must((x, email) => ValidarEmail(email)).WithMessage("Email em formato inválido");
            }

        }


        private static bool ValidarEmail(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }

            return false;
        }

    }
}
