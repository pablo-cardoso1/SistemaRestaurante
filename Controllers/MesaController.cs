using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRestaurante.Data;
using SistemaRestaurante.DTOs;
using SistemaRestaurante.Models;

namespace SistemaRestaurante.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class MesaController : Controller
{
    private readonly RestauranteContext _context;
    
    public MesaController(RestauranteContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MesaRespostaDTO>>> ListarMesas()
    {
        var mesas = await _context.Mesas.AsNoTracking().Select(m => new MesaRespostaDTO
        {
            Id = m.Id,
            Numero = m.Numero,
            Capacidade = m.Capacidade,
            Status = m.Status
        }).ToListAsync();

        return Ok(mesas);

    }
    
    
    [HttpGet("{id:int}", Name = "ObterMesa")]
    public async Task<ActionResult<MesaRespostaDTO>> ObterMesa(int id)
    {
        var mesa = await _context.Mesas.AsNoTracking().Where(m => m.Id == id).Select(m => new MesaRespostaDTO
        {
            Id = m.Id,
            Numero = m.Numero,
            Capacidade = m.Capacidade,
            Status = m.Status
        }).FirstOrDefaultAsync();
        
        if (mesa == null)
            return NotFound();
        return Ok(mesa);
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<MesaRespostaDTO>> CriarMesa([FromBody] MesaCriacaoDTO dto)
    {
        bool mesaExiste = await _context.Mesas.AnyAsync(m => m.Numero == dto.Numero);
        if (mesaExiste)
            return BadRequest("Mesa ja cadastrada");

        var mesa = new Mesa
        {
            Numero = dto.Numero, 
            Capacidade = dto.Capacidade,
            Status = "disponível"
        };

        _context.Mesas.Add(mesa);
        await _context.SaveChangesAsync();

        var resposta = new MesaRespostaDTO
        {
            Id = mesa.Id,
            Numero = mesa.Numero,
            Capacidade = mesa.Capacidade,
            Status = mesa.Status
        };

        return CreatedAtAction(nameof(ListarMesas), new { id = mesa.Id }, resposta);
    }
    
    [HttpPatch("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AtualizarMesa(int id, [FromBody] MesaAtualizacaoDTO dto)
    {
        var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.Id == id);
        if (mesa == null)
            return NotFound();
       
        if (mesa.Numero != dto.Numero)
        {
            bool numeroExiste = await _context.Mesas.AnyAsync(m => m.Numero == dto.Numero && m.Id != id);
            if (numeroExiste)
                return BadRequest("Já existe uma mesa com esse número.");
        }

        mesa.Numero = dto.Numero;
        mesa.Capacidade = dto.Capacidade;
        mesa.Status = dto.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RemoverMesa(int id)
    {
        var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.Id == id);
        if (mesa == null)
            return NotFound();

        
        if (mesa.Status == "reservada")
            return BadRequest("Não é possível remover uma mesa reservada.");

        _context.Mesas.Remove(mesa);
        await _context.SaveChangesAsync(); 
        return NoContent();
    }
}