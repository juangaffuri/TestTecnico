using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.ViewModel;

namespace TestTecnico.Validators
{
    public class AltaAdelantoValidate: AbstractValidator<AdelantoViewModel>
    {
        public AltaAdelantoValidate()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El campo Id es obligatorio.").Matches("^([0-9]){5}([a-zA-Z]){5}$").WithMessage("Número de Id incorrecto.");
            RuleFor(x => x.Monto).NotEmpty().WithMessage("El campo Monto es obligatorio").NotNull().WithMessage("El campo Monto es obligatorio").GreaterThanOrEqualTo(0).WithMessage("El Monto debe ser mayor a cero");
        }

    }
}
