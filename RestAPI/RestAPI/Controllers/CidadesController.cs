using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Repository;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class CidadesController : Controller
    {
       [HttpGet("paises/{idPais}/estados/{idEstado}/cidades")]
       public List<Cidade> GetCidades(
           [FromQuery] string? nome,
           [FromQuery, Range(1, 1000000)] int fromPopulacao,
           [FromRoute, Required] int idPais,
           [FromRoute, Required] int idEstado
           )
        {
            var resultado = CidadeRepository.Cidades.Where(cidade => cidade.IdPais == idPais && 
                cidade.IdEstado == idEstado).ToList();

            if (!string.IsNullOrEmpty(nome))
                resultado = resultado.Where(cidade => cidade.Nome == nome).ToList();

            if (fromPopulacao > 0)
                resultado = resultado.Where(cidade => cidade.Populacao >= fromPopulacao).ToList();
            return resultado;
        }
    }
}
