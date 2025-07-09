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
        public async Task<ActionResult<IEnumerable<OperacionResponseDTO>>> GetOperaciones()
        {
            try
            {
                var operaciones = await _context.Operaciones
                    .Include(o => o.Equipos) // Incluye los equipos relacionados
                    .Select(o => new OperacionResponseDTO
                    {
                        Id = o.Id,
                        Nombre = o.Nombre,
                        Estado = o.Estado,
                        FechaInicio = o.FechaInicio,
                        FechaFinal = o.FechaFinal,
                        NombresEquipos = o.Equipos != null ? o.Equipos.Select(e => e.Nombre ?? string.Empty).ToList() : new List<string>()
                    })
                    .ToListAsync();

                return Ok(operaciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetOperaciones: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperacionResponseDTO>> GetOperacion(int id)
        {
            var operacion = await _context.Operaciones
                .Include(o => o.Equipos)
                .Select(o => new OperacionResponseDTO
                {
                    Id = o.Id,
                    Nombre = o.Nombre,
                    Estado = o.Estado,
                    FechaInicio = o.FechaInicio,
                    FechaFinal = o.FechaFinal,
                    NombresEquipos = o.Equipos != null ? o.Equipos.Select(e => e.Nombre ?? string.Empty).ToList() : new List<string>()
                })
                .FirstOrDefaultAsync(o => o.Id == id);

            if (operacion == null)
            {
                return NotFound($"Operación con ID {id} no encontrada.");
            }

            return operacion;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OperacionResponseDTO>>> PostOperacion(OperacionItem operacion)
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
                Equipos = new List<EquipoItem>()
            };

            if (operacion.Equipos != null && operacion.Equipos.Any())
            {
                var equipos = await _context.Equipos
                    .Where(e => operacion.Equipos.Select(eq => eq.Id).Contains(e.Id))
                    .ToListAsync();
                op.Equipos.AddRange(equipos);
            }

            _context.Operaciones.Add(op);
            await _context.SaveChangesAsync();

            return await GetOperaciones();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<OperacionResponseDTO>>> PutOperacion(int id, OperacionItem operacion)
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