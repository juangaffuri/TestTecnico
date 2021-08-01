using System;
using System.Collections.Generic;

#nullable disable

namespace TestTecnico.Models
{
    public partial class Adelanto
    {
        public Adelanto()
        {
            Pagos = new HashSet<Pago>();
        }

        public string IdAdelanto { get; set; }
        public int? IdEmpleado { get; set; }
        public double Monto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public bool Cancelado { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
