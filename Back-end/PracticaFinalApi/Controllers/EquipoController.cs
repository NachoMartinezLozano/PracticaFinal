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
        public async Task<ActionResult<IEnumerable<EquipoResponseDTO>>> GetEquipos()
        {
            try
            {
                var equipos = await _context.Equipos
                    .Include(e => e.Operacion)
                    .Include(e => e.Agentes)
                    .Select(e => new EquipoResponseDTO
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Especialidad = e.Especialidad,
                        OperacionId = e.OperacionId,
                        OperacionNombre = e.Operacion != null ? e.Operacion.Nombre : null,
                        Agentes = e.Agentes
                    })
                    .ToListAsync();

                return Ok(equipos);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetEquipos: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoResponseDTO>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos
                .Include(e => e.Operacion)
                .Include(e => e.Agentes)
                .Select(e => new EquipoResponseDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Especialidad = e.Especialidad,
                    OperacionId = e.OperacionId,
                    OperacionNombre = (e.Operacion != null && e.Id == id) ? e.Operacion.Nombre : null,
                    Agentes = e.Agentes
                })
                .FirstOrDefaultAsync(e => e.Id == id);
            if (equipo == null)
            {
                return NotFound($"Equipo con ID {id} no encontrado.");
            }

            return equipo;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EquipoResponseDTO>>> PostEquipo(EquipoItemDTO equipo)
        {
            if (equipo == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            var newEq = EquipoDTOToEquipo(equipo);

            if (equipo.OperacionId.HasValue)
            {
                var operacion = await _context.Operaciones
                    .Include(o => o.Equipos)
                    .FirstOrDefaultAsync(o => o.Id == equipo.OperacionId);
                
                if (operacion == null)
                {
                    return BadRequest($"Operación con ID {equipo.OperacionId} no encontrada.");
                }

                // Agregar el equipo a la lista de la operación
                operacion. Equipos ??= new List<EquipoItem>();
                operacion.Equipos.Add(newEq);
            }

            _context.Equipos.Add(newEq);
            await _context.SaveChangesAsync();

            return await GetEquipos();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<EquipoResponseDTO>>> PutEquipo(int id, EquipoItem equipo)
        {
            if (id != equipo.Id)
            {
                return BadRequest("El ID del equipo no coincide con el ID en la URL.");
            }

            var existingEquipo = await _context.Equipos
                .Include(e => e.Operacion)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (existingEquipo == null)
            {
                return NotFound($"Equipo con ID {id} no encontrado.");
            }

            // Actualizar los campos del equipo
            existingEquipo.Id = equipo.Id;
            existingEquipo.Nombre = equipo.Nombre;
            existingEquipo.Especialidad = equipo.Especialidad;

            // Manejar el cambio de OperacionId
            if (existingEquipo.OperacionId != equipo.OperacionId)
            {
                // Remover el equipo de la operación anterior, si existe
                if (existingEquipo.OperacionId.HasValue)
                {
                    var oldOperacion = await _context.Operaciones
                        .Include(o => o.Equipos)
                        .FirstOrDefaultAsync(o => o.Id == existingEquipo.OperacionId);
                    
                    if (oldOperacion != null)
                    {
                        oldOperacion.Equipos?.Remove(existingEquipo);
                    }
                }

                // Asignar a la nueva operación, si existe
                if (equipo.OperacionId.HasValue)
                {
                    var newOperacion = await _context.Operaciones
                        .Include(o => o.Equipos)
                        .FirstOrDefaultAsync(o => o.Id == equipo.OperacionId);
                    
                    if (newOperacion == null)
                    {
                        return BadRequest($"Operación con ID {equipo.OperacionId} no encontrada.");
                    }

                    newOperacion.Equipos ??= new List<EquipoItem>();
                    newOperacion.Equipos.Add(existingEquipo);
                }

                existingEquipo.OperacionId = equipo.OperacionId;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EquipoExists(id))
            {
                return NotFound($"Equipo con ID {id} no encontrado.");
            }

            return await GetEquipos();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }

        public static EquipoItem EquipoDTOToEquipo(EquipoItemDTO eq)
        {
            return new EquipoItem
            {
                Nombre = eq.Nombre,
                Especialidad = eq.Especialidad,
                OperacionId = eq.OperacionId
            };
        }
    }
}