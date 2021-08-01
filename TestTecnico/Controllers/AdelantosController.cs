using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTecnico.Helpers;
using TestTecnico.Models;
using TestTecnico.ViewModel;

namespace TestTecnico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdelantosController : ControllerBase
    {
        private readonly DBContextTest _context;

        public AdelantosController(DBContextTest context)
        {
            _context = context;
        }

        // GET: api/Adelantos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adelanto>>> GetAdelantos()
        {
            return await _context.Adelantos.ToListAsync();
        }

        // GET: api/Adelantos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adelanto>> GetAdelanto(string id)
        {
            var adelanto = await _context.Adelantos.FindAsync(id);

            if (adelanto == null)
            {
                return NotFound();
            }

            return adelanto;
        }

        // PUT: api/Adelantos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdelanto(string id, Adelanto adelanto)
        {
            if (id != adelanto.IdAdelanto)
            {
                return BadRequest();
            }

            _context.Entry(adelanto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdelantoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Adelantos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Adelanto>> PostAdelanto(AdelantoViewModel adelanto)
        {
            var emp = _context.Empleados.FirstOrDefault(x=>x.Legajo == adelanto.Legajo);
            if (emp == null)
            {
                return NotFound(new ResponseError()
                {
                    CodigoError = "errEmpleadoNoExiste",
                    Detalle = "El legajo del empleado no existe."
                });
                
            }
            //verificamos el tope de adelantos segun el tipo, que por default son 2 para todos
            var cantAdelantosActivos = emp.Adelantos.Where(a => !a.Cancelado).Count();
            var cantAdelantosPermitidos = emp.IdTipoEmpleadoNavigation.MaximoAdelanto;
            if (cantAdelantosActivos + 1 > cantAdelantosPermitidos)
            {
                return StatusCode(412, new ResponseError()
                {
                    CodigoError = "errMaximoAdelantosPermitidos",
                    Detalle = $"No se puede completar el adelanto. El empleado posee {cantAdelantosActivos} sin cancelar."
                });
            }
            //verificamos el monto, que no supere el % según el tipo de usuario
            var maximoAdelanto = emp.Sueldo * emp.IdTipoEmpleadoNavigation.PorcentajeAdelanto / 100;
            if (adelanto.Monto> maximoAdelanto)
            {
                return StatusCode(412, new ResponseError()
                {
                    CodigoError = "errMontoMaximoAdelanto",
                    Detalle = $"El monto ingresado excede el permitido según el tipo de usuario y sueldo: {maximoAdelanto}"
                });
            }

            var a = adelanto.ToModel();
            _context.Adelantos.Add(a);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdelantoExists(adelanto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdelanto", new { id = a.IdAdelanto }, a);
        }

        // DELETE: api/Adelantos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Adelanto>> DeleteAdelanto(string id)
        {
            var adelanto = await _context.Adelantos.FindAsync(id);
            if (adelanto == null)
            {
                return NotFound();
            }

            _context.Adelantos.Remove(adelanto);
            await _context.SaveChangesAsync();

            return adelanto;
        }

        private bool AdelantoExists(string id)
        {
            return _context.Adelantos.Any(e => e.IdAdelanto == id);
        }
    }
}
