using System;
using System.Collections.Generic;
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
            try
            {
                var operaciones = await _context.Operaciones
                    .Include(o => o.Equipos) // Incluye los equipos relacionados con la operación
                    .ToListAsync();
                return Ok(operaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las operaciones: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperacionItem>> GetOperacion(int id)
        {
            try
            {
                var operacion = await _context.Operaciones
                    .Include(o => o.Equipos) // Incluye los equipos relacionados con la operación
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (operacion == null)
                {
                    return NotFound($"Operación con ID {id} no encontrada.");
                }

                return Ok(operacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la operación: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OperacionItem>> PostOperacion(OperacionItem operacion)
        {
            try
            {
                if (operacion == null)
                {
                    return BadRequest("Operación no puede ser nula.");
                }

                _context.Operaciones.Add(operacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOperacion), new { id = operacion.Id }, operacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la operación: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacion(int id, [FromBody] OperacionItem operacion)
        {
            try
            {
                if (id != operacion.Id)
                {
                    return BadRequest("El ID de la operación en la URL no coincide con el ID del cuerpo de la solicitud.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingOperacion = await _context.Operaciones.FindAsync(id);
                if (existingOperacion == null)
                {
                    return NotFound($"No se encontró la operación con ID {id}.");
                }

                existingOperacion.Nombre = operacion.Nombre;
                existingOperacion.Estado = operacion.Estado;
                existingOperacion.FechaInicio = operacion.FechaInicio;
                existingOperacion.FechaFinal = operacion.FechaFinal;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la operación: {ex.Message}");
            }
        }
    }
}