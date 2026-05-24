using FluentValidation;
using MindFit_Intelligence_Backend.DTOs.Responsables;

namespace MindFit_Intelligence_Backend.Validators.Responsables
{
    public class ResponsableInsertDtoValidator : AbstractValidator<ResponsableInsertDto>
    {
        public ResponsableInsertDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede superar los 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.");

            RuleForEach(x => x.IdGrupos)
                .GreaterThan(0).WithMessage("Los IDs de grupo deben ser mayores a 0.");
        }
    }
}
