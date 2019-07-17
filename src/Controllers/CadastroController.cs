using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.modelo.entidades;
using src.modelo.interfaces;
using src.modelo.viewmodels;

namespace src.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class CadastroController : Controller
    {
        private string perfilDeAcesso = "PrestadorDeServico";
        private readonly IPrestadorDeServicoRepositorio _prestadorDeServicoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CadastroController(IPrestadorDeServicoRepositorio prestadorDeServicoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _prestadorDeServicoRepositorio = prestadorDeServicoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        [Route("SolicitarCadastro")]
        [Produces("application/json")]
        public async Task<IActionResult> SolicitarCadastro([FromBody] PreCadastroRequest message)
        {
            if (ModelState.IsValid)
            {

                var prestadorDeServico = new PrestadorDeServico(message.Nome, message.Sobrenome, message.Email, message.Telefone, message.Cidade, message.Servico);
                var usuarioNoBanco = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorEmail(message.Email);

                if (usuarioNoBanco != null && usuarioNoBanco.Ativo)
                {
                    return BadRequest("Prestador de Serviço já cadastrado!");
                }

                if (usuarioNoBanco == null)
                {
                    await _prestadorDeServicoRepositorio.Gravar(prestadorDeServico);
                }
                else
                {
                    usuarioNoBanco.AlterarInformacoesPessoais(prestadorDeServico);
                    await _prestadorDeServicoRepositorio.Atualizar(usuarioNoBanco);
                }

                return Ok();
            }
            return BadRequest("Formulario Inválido!");
        }

        [HttpPost]
        [Route("FinalizarSolicitacao")]
        [Produces("multipart/form-data")]
        public async Task<IActionResult> FinalizarSolicitacao([FromForm]string rg, [FromForm]string cpf, [FromForm]string email, [FromForm]string telefone, [FromForm]string numero, [FromForm]string cep, [FromForm]string bairro, [FromForm]string rua, [FromForm]int servico, [FromForm] IFormFile documento, [FromForm] IFormFile selfie)
        {
            if (ModelState.IsValid)
            {
                byte[] documentoArray = null;
                byte[] selfieArray = null;

                if (documento == null || selfie == null) return BadRequest("Anexos inválidos!");

                if (string.IsNullOrWhiteSpace(rg) || string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(email)) return BadRequest("Dados inválidos!");

                if (string.IsNullOrWhiteSpace(telefone) || string.IsNullOrWhiteSpace(numero) || string.IsNullOrWhiteSpace(bairro)) return BadRequest("Dados inválidos!");

                if (string.IsNullOrWhiteSpace(cep) || string.IsNullOrWhiteSpace(rua)) return BadRequest("Dados inválidos!");

                using (var memoryStream = new MemoryStream())
                {
                    await documento.CopyToAsync(memoryStream);
                    documentoArray = memoryStream.ToArray();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await selfie.CopyToAsync(memoryStream);
                    selfieArray = memoryStream.ToArray();
                }

                var prestadorDeServico = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorEmail(email);
                if (prestadorDeServico != null)
                {
                    prestadorDeServico.AlterarEndereco(cep, rua, bairro, numero);
                    prestadorDeServico.AlterarDocumentos(documentoArray, selfieArray);
                    prestadorDeServico.AlterarDados(rg, cpf);
                    prestadorDeServico.DefinirServico(servico);
                }

                await _prestadorDeServicoRepositorio.Atualizar(prestadorDeServico);
                return Ok();
            }
            return BadRequest("Formulario Inválido!");
        }

        [HttpGet]
        [Route("ListarCadastrosPendentes")]
        public async Task<IActionResult> ListarCadastrosPendentes()
        {
            var cadastrosPendentes = await _prestadorDeServicoRepositorio.BuscarCadastrosPendentesDeAprovacao();
            var saida = cadastrosPendentes.Select(x => new {
                Id = x.Id,
                Nome = x.Nome,
                Sobrenome = x.Sobrenome,
                Email = x.Email,
                ServicoId = x.ServicoId
            });
            return Json(saida);
        }


        [HttpGet]
        [Route("ObterCadastro")]
        [Produces("application/json")]
        public async Task<IActionResult> ObterCadastro(int id)
        {
            if (id > 0)
            {
                var prestadorDeServico = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorId(id);

                if (prestadorDeServico != null)
                {
                    var saida = new{
                        Id = prestadorDeServico.Id,
                        Nome = prestadorDeServico.Nome,
                        Sobrenome = prestadorDeServico.Sobrenome,
                        Bairro = prestadorDeServico.Bairro,
                        Cep = prestadorDeServico.Cep,
                        Cidade = prestadorDeServico.Cidade,
                        Cpf = prestadorDeServico.Cpf,
                        Email = prestadorDeServico.Email,
                        Numero = prestadorDeServico.Numero,
                        Rg = prestadorDeServico.Rg,
                        Rua = prestadorDeServico.Rua,
                        Servico = prestadorDeServico.Servico.Descricao,
                        Telefone = prestadorDeServico.Telefone
                    };
                    return Json(saida);
                }
            }
            return BadRequest("Dados Inválidos!");
        }

        [HttpPost]
        [Route("AprovarCadastro")]
        [Produces("application/json")]
        public async Task<IActionResult> AprovarCadastro([FromBody] AprovacaoRequest message)
        {
            if (ModelState.IsValid)
            {
                var prestadorDeServico = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorId(message.Id);
                if (prestadorDeServico != null)
                {
                    if (message.IntencaoDeAprovacao)
                    {
                        prestadorDeServico.AlterarSituacaoCadastral(message.IntencaoDeAprovacao);

                        if (prestadorDeServico.Usuario != null)
                        {
                            prestadorDeServico.Usuario.AlterarPerfil(perfilDeAcesso);
                        }
                        else
                        {
                            var usuario = new Usuario(prestadorDeServico.Nome, prestadorDeServico.Email, prestadorDeServico.Email, "PrestadorDeServico");
                            prestadorDeServico.DefinirUsuario(usuario);
                        }

                        await _prestadorDeServicoRepositorio.Atualizar(prestadorDeServico);
                        return Ok();
                    }else{
                        await _prestadorDeServicoRepositorio.Remover(prestadorDeServico);
                        return Ok();
                    }
                }
            }
            return BadRequest("Dados Inválidos.");
        }

        [HttpGet]
        [Route("VisualizarDocumento")]
        public async Task<IActionResult> VisualizarDocumento(int id)
        {
            if (id > 0)
            {
                var prestadorDeServico = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorId(id);

                if (prestadorDeServico != null)
                    return File(prestadorDeServico.Documento, "image/jpeg");
            }
            return BadRequest("Dados Inválidos!");
        }

        [HttpGet]
        [Route("VisualizarSelfie")]
        public async Task<IActionResult> VisualizarSelfie(int id)
        {
            if (id > 0)
            {
                var prestadorDeServico = await _prestadorDeServicoRepositorio.ObterPrestadorDeServicoPorId(id);

                if (prestadorDeServico != null)
                    return File(prestadorDeServico.Selfie, "image/jpeg");
            }
            return BadRequest("Dados Inválidos!");
        }
    }
}