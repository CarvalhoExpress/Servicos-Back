using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using src.modelo.interfaces;

namespace src.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class PrestadorDeServicoController : Controller
    {
        private readonly IPrestadorDeServicoRepositorio _prestadorDeServicoRepositorio;

        public PrestadorDeServicoController(IPrestadorDeServicoRepositorio prestadorDeServicoRepositorio)
        {
            _prestadorDeServicoRepositorio = prestadorDeServicoRepositorio;
        }

        [HttpGet]
        [Route("ListarPrestadoresDeServicos")]
        public async Task<IActionResult> ListarPrestadoresDeServicos(string busca = "")
        {
            var cadastrosPendentes = await _prestadorDeServicoRepositorio.BuscarCadastrosAtivos(busca);
            var saida = cadastrosPendentes.Select(x =>  new {
                Id = x.Id,
                Nome = x.Nome,
                Sobrenome = x.Sobrenome,
                Telefone = x.Telefone,
                Email = x.Email,
                Servico = x.Servico.Descricao
            }).ToList();
            return Json(saida);
        }
    }
}