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
    public class PagosController : ControllerBase
    {
        private readonly DBContextTest _context;

        public PagosController(DBContextTest context)
        {
            _context = context;
        }

        // GET: api/Pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        // GET: api/Pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // PUT: api/Pagos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IdPago)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Pago>> PostPago(PagoViewModel pago)
        {
            var adelanto = await _context.Adelantos.FindAsync(pago.IdAdelanto);
            if (adelanto == null)
            {
                return NotFound(new ResponseError()
                {
                    CodigoError = "errAdelantoNoExiste",
                    Detalle = "No existe un adelanto para el pago"
                }) ;
            }
            var acumPagos = _context.Pagos.Where(p => p.IdAdelanto == pago.IdAdelanto).Sum(s => s.MontoPago);
            if (acumPagos + pago.Monto > adelanto.Monto)
            {
                return StatusCode(412, new ResponseError()
                {
                    CodigoError = "errMontoSuperiorAlAdelanto",
                    Detalle = $"El monto ingresado supera el monto del adelanto: {adelanto.Monto}"
                });
            }
            else if (acumPagos + pago.Monto == adelanto.Monto)
            {
                _context.Entry(adelanto).State = EntityState.Modified;
                adelanto.FechaCancelacion = DateTime.Now;
                adelanto.Cancelado = true;
            }
            var p = pago.ToModel();
            _context.Pagos.Add(p);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = p.IdPago }, p);
        }

        // DELETE: api/Pagos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pago>> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return pago;
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPago == id);
        }
    }
}
