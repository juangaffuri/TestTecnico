using System;
using System.Collections.Generic;

#nullable disable

namespace TestTecnico.Models
{
    public partial class TipoEmpleado
    {
        public TipoEmpleado()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdTipoEmpleado { get; set; }
        public string Descripcion { get; set; }
        public double? PorcentajeAdelanto { get; set; }
        public int? MaximoAdelanto { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
