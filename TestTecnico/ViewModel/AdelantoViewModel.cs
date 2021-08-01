using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.Models;

namespace TestTecnico.ViewModel
{
    public class AdelantoViewModel
    {
        public string Id { get; set; }
        public int Legajo { get; set; }
        public double Monto { get; set; }
        public Adelanto ToModel()
        {
            return new Adelanto()
            {
                IdAdelanto = Id,
                IdEmpleado = Legajo,
                Monto = Monto,
                FechaAlta = DateTime.Now
            };
        }
    }
}
