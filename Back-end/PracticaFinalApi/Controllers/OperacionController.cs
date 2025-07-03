using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaFinalApi.Models;
namespace PracticaFinalApi.Controllers
{
    [Route("api/Operacion")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OperacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperacionItem>>> GetOperaciones()
        {

            return await _context.Operaciones
                //.Include(o => o.Equipos) // Incluye los equipos relacionados con la operación
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperacionItem>> GetOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
            {
                return NotFound($"Operación con ID {id} no encontrada.");
            }

            return operacion;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OperacionItem>>> PostOperacion(OperacionItem operacion)
        {
            if (operacion == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            var op = new OperacionItem
            {
                Nombre = operacion.Nombre,
                Estado = operacion.Estado,
                FechaInicio = operacion.FechaInicio,
                FechaFinal = operacion.FechaFinal,
                //Equipos = operacion.Equipos
            };

            _context.Operaciones.Add(op);
            await _context.SaveChangesAsync();

            return await GetOperaciones();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<OperacionItem>>> PutOperacion(int id, OperacionItem operacion)
        {
            if (id != operacion.Id)
            {
                return BadRequest("El ID de la operación no coincide con el ID proporcionado en la URL.");
            }

            var op = await _context.Operaciones.FindAsync(id);
            if (op == null)
            {
                return NotFound($"Operación con ID {id} no encontrada.");
            }

            op.Id = operacion.Id;
            op.Nombre = operacion.Nombre;
            op.Estado = operacion.Estado;
            op.FechaInicio = operacion.FechaInicio;
            op.FechaFinal = operacion.FechaFinal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!OperacionExists(id))
            {
                return NotFound($"Operación con ID {id} no encontrada.");
            }
            return await GetOperaciones();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            var operacion = await _context.Operaciones.FindAsync(id);
            if (operacion == null)
            {
                return NotFound();
            }

            _context.Operaciones.Remove(operacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool OperacionExists(int id)
        {
            return _context.Operaciones.Any(e => e.Id == id);
        }
    }
}