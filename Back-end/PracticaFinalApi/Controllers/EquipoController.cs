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
    [Route("api/Equipo")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EquipoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipoItem>>> GetEquipos()
        {
            try
            {
                var equipos = await _context.Equipos
                    .Include(e => e.Operacion) // Incluye la operación relacionada con el equipo
                    .Include(e => e.Agentes) // Incluye los agentes relacionados con el equipo
                    .ToListAsync();
                return Ok(equipos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los equipos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoItem>> GetEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Operacion) // Incluye la operación relacionada con el equipo
                    .Include(e => e.Agentes) // Incluye los agentes relacionados con el equipo
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (equipo == null)
                {
                    return NotFound($"Equipo con ID {id} no encontrado.");
                }

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el equipo: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EquipoItem>> PostEquipo(EquipoItem equipo)
        {
            try
            {
                if (equipo == null)
                {
                    return BadRequest("El equipo no puede ser nulo.");
                }

                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id }, equipo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el equipo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id, EquipoItem equipo)
        {
            if (id != equipo.Id)
            {
                return BadRequest("El ID del equipo no coincide con el ID proporcionado.");
            }

            var existingEquipo = await _context.Equipos.FindAsync(id);
            if (existingEquipo == null)
            {
                return NotFound($"Equipo con ID {id} no encontrado.");
            }

            try
            {
                existingEquipo.Nombre = equipo.Nombre;
                existingEquipo.Especialidad = equipo.Especialidad;
                existingEquipo.OperacionId = equipo.OperacionId;
                existingEquipo.Agentes = equipo.Agentes;
                existingEquipo.Operacion = equipo.Operacion;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
                {
                    return NotFound($"Equipo con ID {id} no encontrado.");
                }
            }
            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }
    }
}