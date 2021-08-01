using System;
using System.Collections.Generic;

#nullable disable

namespace TestTecnico.Models
{
    public partial class Pago
    {
        public int IdPago { get; set; }
        public string IdAdelanto { get; set; }
        public double MontoPago { get; set; }
        public DateTime FechaPago { get; set; }

        public virtual Adelanto IdAdelantoNavigation { get; set; }
    }
}
