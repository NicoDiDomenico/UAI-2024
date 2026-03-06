using FluentValidation;
using MindFit_Intelligence_Backend.DTOs.Socios;

namespace MindFit_Intelligence_Backend.Validators.Socios
{
    public class SocioUpdateDtoValidator : AbstractValidator<SocioUpdateDto>
    {
        public SocioUpdateDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede superar los 100 caracteres.");

            RuleForEach(x => x.IdGrupos)
                .GreaterThan(0).WithMessage("Los IDs de grupo deben ser mayores a 0.");
        }
    }
}
