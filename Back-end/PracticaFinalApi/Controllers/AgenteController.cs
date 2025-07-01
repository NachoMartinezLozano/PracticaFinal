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
                .Include(e => e.Equipo)
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
        public async Task<ActionResult<IEnumerable<AgenteItem>>> PostAgente(AgenteItem agente)
        {
            if (agente == null)
            {
                return BadRequest();
            }

            var ag = new AgenteItem
            {
                Nombre = agente.Nombre,
                Rango = agente.Rango,
                Activo = agente.Activo,
                EquipoId = agente.EquipoId,
                Equipo = agente.Equipo
            };

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
    }
}