using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.Models;
using TestTecnico.ViewModel;

namespace TestTecnico.Validators
{
    public class AltaEmpleadoValidate: AbstractValidator<EmpleadoViewModel>
    {
        public AltaEmpleadoValidate()
        {
            RuleFor(x => x.DNI).NotEmpty().WithMessage("El campo DNI es obligatorio.").Matches("^[0-9]*$").WithMessage("Número de documento inválido.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El campo Nombre es obligatorio").NotNull().WithMessage("El campo Nombre es obligatorio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El campo Apellido es obligatorio").NotNull().WithMessage("El campo Apellido es obligatorio");
            RuleFor(x => x.Legajo).NotEmpty().WithMessage("El campo Legajo es obligatorio").NotNull().WithMessage("El campo Legajo es obligatorio").GreaterThan(0).WithMessage("El legajo no puede ser menor o igual a cero");
            RuleFor(x => x.TipoEmpleado).NotEmpty().WithMessage("El campo Tipo Empleado es obligatorio").NotNull().WithMessage("El campo Tipo Empleado es obligatorio");
        }
    }
}
