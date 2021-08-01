using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTecnico.Models;

namespace TestTecnico.ViewModel
{
    public class EmpleadoViewModel
    {
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public double Sueldo { get; set; }
        public int TipoEmpleado { get; set; }
        public Empleado ToModel()
        {
            return new Empleado()
            {
                Legajo = Legajo,
                Nombre = Nombre,
                Apellido = Apellido,
                Dni = DNI,
                Sueldo = Sueldo,
                IdTipoEmpleado = TipoEmpleado
            };
        }
        
    }
}
