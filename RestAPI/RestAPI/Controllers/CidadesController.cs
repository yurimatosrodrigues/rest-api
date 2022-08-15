using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Repository;

namespace RestAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class CidadesController : Controller
    {
       [HttpGet("paises/{idPais}/estados/{idEstado}/cidades")]
       public List<Cidade> GetCidades([FromQuery] string nome)
        {
            var resultado = CidadeRepository.Cidades;
            if (!string.IsNullOrEmpty(nome))
            {
                resultado = CidadeRepository.Cidades.Where(cidade => cidade.Nome == nome).ToList();
            }
            return resultado;
        }
    }
}
