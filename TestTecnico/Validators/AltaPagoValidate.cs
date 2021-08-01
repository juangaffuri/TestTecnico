using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.ViewModel;

namespace TestTecnico.Validators
{
    public class AltaPagoValidate : AbstractValidator<PagoViewModel>
    {
        public AltaPagoValidate()
        {
            RuleFor(x => x.Monto).NotEmpty().WithMessage("El campo Monto es obligatorio").NotNull().WithMessage("El campo Monto es obligatorio").GreaterThanOrEqualTo(0).WithMessage("El Monto debe ser mayor a cero");
        }
    }
}
