using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.Models;

namespace TestTecnico.ViewModel
{
    public class PagoViewModel
    {
        public string IdAdelanto { get; set; }
        public double Monto { get; set; }
        public Pago ToModel()
        {
            return new Pago()
            {
                IdAdelanto = IdAdelanto,
                MontoPago = Monto,
                FechaPago = DateTime.Now,
            };
        }
    }
}
