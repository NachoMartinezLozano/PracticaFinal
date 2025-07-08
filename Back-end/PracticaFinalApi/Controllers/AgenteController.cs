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
        public async Task<ActionResult<IEnumerable<AgenteItem>>> GetAgentes()
        {
            return await _context.Agentes
                //.Include(e => e.Equipo)
                .OrderBy(x => x.Nombre)
                .Select(x => x)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgenteItem>> GetAgente(int id)
        {
            var agente = await _context.Agentes.FindAsync(id);
            if (agente == null)
            {
                return NotFound();
            }
            return agente;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<AgenteItem>>> PostAgente(AgenteItemDTO agente)
        {
            if (agente == null)
            {
                return BadRequest();
            }

            var ag = AgenteDTOToAgente(agente);

            _context.Agentes.Add(ag);
            await _context.SaveChangesAsync();

            return await GetAgentes();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<AgenteItem>>> PutAgente(int id, AgenteItem agente)
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
            //ag.Equipo = agente.Equipo;

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