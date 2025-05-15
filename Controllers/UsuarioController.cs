using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaRestaurante.Data;
using SistemaRestaurante.DTOs;
using SistemaRestaurante.Models;

namespace SistemaRestaurante.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class UsuarioController : Controller
{
    private readonly RestauranteContext _context;
    private readonly IConfiguration _configuration;
    
    public UsuarioController(RestauranteContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
    {
        // validação automatica via data anotations
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // verifica se o email ja foi cadastrado
        bool emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
        if (emailExiste)
            return BadRequest("Email ja cadastrado");
        
        // hash da senha com Identity
        var senhaHash = new PasswordHasher<Usuario>();

        var usuario = new Usuario
        {

            Nome = dto.Nome,
            Email = dto.Email,
            Role = "cliente"

        };
        
        usuario.SenhaHash = senhaHash.HashPassword(usuario, dto.Senha);
        
        // add usuario ao contexto e salva no banco
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        
        //dto de resposta
        var resposta = new UsuarioRespostaDTO()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role
        };
        
        //returno 201 created usando o dto de resposta
        return CreatedAtAction(nameof(Registrar), new { id = usuario.Id }, resposta);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        // busca o usuario pelo email
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (usuario == null)
            return BadRequest("Email ou senha invalidos");
        
        // verifica se a senha esta correta
        var senhaHasher = new PasswordHasher<Usuario>();
        var resultado = senhaHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);
        if (resultado == PasswordVerificationResult.Failed)
            return BadRequest(new { mensagem = "Email ou senha invalidos" });
        
        // gerar o jwt
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtConfig = _configuration.GetSection("Jwt");
        var chave = Encoding.ASCII.GetBytes(jwtConfig["Key"]); // chave de criptografia
        var tokenDescriptor = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role)
            }),

            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = jwtConfig["Issuer"],
            Audience = jwtConfig["Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        
        // retorna o token e dados do usuario
        return Ok(new
        {
            token = tokenString,
            usuario = new UsuarioRespostaDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role
            }
        });
    }
    
}