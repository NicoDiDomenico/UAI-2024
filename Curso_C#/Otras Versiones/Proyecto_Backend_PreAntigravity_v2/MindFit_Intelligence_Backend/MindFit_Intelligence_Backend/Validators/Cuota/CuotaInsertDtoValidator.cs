using FluentValidation;
using MindFit_Intelligence_Backend.DTOs.Cuota;

namespace MindFit_Intelligence_Backend.Validators.Cuota
{
    public class CuotaInsertDtoValidator : AbstractValidator<CuotaInsertDto>
    {
        public CuotaInsertDtoValidator()
        {
            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage("El IdUsuario es obligatorio.")
                .GreaterThan(0).WithMessage("El IdUsuario debe ser mayor a 0.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");
        }
    }
}
