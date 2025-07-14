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
    [Route("api/Agente")]
    [ApiController]

    public class AgenteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AgenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgenteResponseDTO>>> GetAgentes()
        {
            try
            {
                var agente = await _context.Agentes
                .Include(a => a.Equipo)
                .Select(a => new AgenteResponseDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Rango = a.Rango,
                    Activo = a.Activo,
                    EquipoId = a.EquipoId,
                    EquipoNombre = a.Equipo != null ? a.Equipo.Nombre : null
                })
                .ToListAsync();

                return Ok(agente);
            } catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetAgentes: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgenteResponseDTO>> GetAgente(int id)
        {
            var agente = await _context.Agentes.
                Include(a => a.Equipo)
                .Select(a => new AgenteResponseDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Rango = a.Rango,
                    Activo = a.Activo,
                    EquipoId = a.EquipoId,
                    EquipoNombre = (a.Equipo != null && a.Id == id) ? a.Equipo.Nombre : null
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agente == null)
            {
                return NotFound($"Agente con ID {id} no encontrado.");
            }
            
            return agente;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<AgenteResponseDTO>>> PostAgente(AgenteItemDTO agente)
        {
            if (agente == null)
            {
                return BadRequest();
            }

            var ag = AgenteDTOToAgente(agente);

            if (agente.EquipoId.HasValue)
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Agentes)
                    .FirstOrDefaultAsync(e => e.Id == agente.EquipoId);
                
                if (equipo == null)
                {
                    return BadRequest($"Equipo con ID {agente.EquipoId} no encontrado.");
                }

                equipo.Agentes ??= new List<AgenteItem>();
                equipo.Agentes.Add(ag);
            }

            _context.Agentes.Add(ag);
            await _context.SaveChangesAsync();

            return await GetAgentes();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<AgenteResponseDTO>>> PutAgente(int id, AgenteItem agente)
        {
            if (id != agente.Id)
            {
                return BadRequest();
            }

            var ag = await _context.Agentes.FindAsync(id);
            if (ag == null)
            {
                return NotFound();
            }

            ag.Id = agente.Id;
            ag.Nombre = agente.Nombre;
            ag.Rango = agente.Rango;
            ag.Activo = agente.Activo;
            ag.EquipoId = agente.EquipoId;
            ag.Equipo = agente.Equipo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AgenteExists(id))
            {
                return NotFound();
            }
            return await GetAgentes();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgente(int id)
        {
            var agente = await _context.Agentes.FindAsync(id);
            if (agente == null)
            {
                return NotFound();
            }

            _context.Agentes.Remove(agente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgenteExists(int id)
        {
            return _context.Agentes.Any(a => a.Id == id);
        }

        public static AgenteItem AgenteDTOToAgente(AgenteItemDTO ag)
        {
            return new AgenteItem
            {
                Nombre = ag.Nombre,
                Rango = ag.Rango,
                Activo = ag.Activo,
                EquipoId = ag.EquipoId
            };
        }
    }
}