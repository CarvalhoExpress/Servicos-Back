using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using src.modelo.entidades;
using src.modelo.interfaces;
using src.modelo.viewmodels;

namespace src.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private const string perfil = "Cliente";
        SigningConfigurations signingConfigurations;
        TokenConfigurations tokenConfigurations;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AccountController(IUsuarioRepositorio usuarioRepositorio, [FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
        {
            _usuarioRepositorio = usuarioRepositorio;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginRequest message)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario(message.Email, message.Password);
                var auth = await _usuarioRepositorio.BuscarUsuario(usuario.Email, usuario.Password);
                if (auth != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity
                    (
                        new GenericIdentity(auth.Email, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(ClaimTypes.Name, auth.Email),
                            new Claim("Id", auth.Id.ToString()),
                            new Claim(ClaimTypes.Role, auth.PerfilDeAcesso),
                        }
                    );

                    DateTime dataCriacao = DateTime.Now;

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = null
                    });
                    var token = handler.WriteToken(securityToken);

                    var resultado = new
                    {
                        user = new 
                        {
                            _id = auth.Id,
                            name = auth.Nome,
                            document = auth.PerfilDeAcesso,
                            email = auth.Email
                        },
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        token = token,
                        message = "OK"
                    };
                    return Json(resultado);
                }
                return BadRequest("Dados Inválidos!");
            }
            return BadRequest("Formulario Inválido!");
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody]CadastroRequest message)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioRepositorio.BuscarUsuarioPorEmail(message.Email);
                if (usuario != null)
                {
                    return BadRequest("Email já cadastrado!");
                }

                usuario = new Usuario(message.UserName, message.Email, message.PasswordGroup.Password, perfil);
                await _usuarioRepositorio.GravarUsuario(usuario);
                return Ok();
            }
            return BadRequest("Formulario Inválido!");
        }
    }
}