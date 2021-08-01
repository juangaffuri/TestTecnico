using System;
using System.Collections.Generic;

#nullable disable

namespace TestTecnico.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Adelantos = new HashSet<Adelanto>();
        }

        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public double? Sueldo { get; set; }
        public int IdTipoEmpleado { get; set; }

        public virtual TipoEmpleado IdTipoEmpleadoNavigation { get; set; }
        public virtual ICollection<Adelanto> Adelantos { get; set; }
    }
}
