using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            await Cidades();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int idCidade, string nome, int estado, int pais, int populacao)
        {
            Cidade cidade = new Cidade()
            {
                Id = idCidade,
                Nome = nome,
                Populacao = populacao,
            };

            using (var httpClient = new HttpClient())
            {
                StringContent conteudo = new StringContent(JsonSerializer.Serialize(cidade),
                    Encoding.UTF8, "application/json");
                try
                {
                    using (var resposta = await httpClient.PostAsync(
                        $"https://localhost:7073/paises/{pais}/estados/{estado}/cidades", conteudo))
                    {
                        if (resposta.StatusCode == HttpStatusCode.Created)
                        {
                            //Sucesso
                            ViewBag.MensagemGravacao = "Gravado com sucesso!";
                        }
                        else
                        {
                            ViewBag.MensagemGravacao = "Ocorreu um erro.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.MensagemGravacao = $"Ocorreu um erro : {ex.Message}";
                }
            }
            return await Index();
        }

        [HttpGet]
        public async Task<IActionResult> Cidades()
        {
            List<Cidade> Cidades = new List<Cidade>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var resposta = await httpClient.GetAsync(
                           $"https://localhost:7073/paises/55/estados/11/cidades"))
                    {
                        if (resposta.StatusCode == HttpStatusCode.OK)
                        {
                            var conteudo = resposta.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(conteudo))
                                Cidades = JsonSerializer.Deserialize<List<Cidade>>(conteudo,
                                    new JsonSerializerOptions
                                    {
                                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                    })!;
                        }
                        else
                        {
                            ViewBag.ErroBusca = $"Ocorreu um erro ao buscar as cidades. StatusCode: {resposta.StatusCode.ToString()}.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErroBusca = $"Ocorreu um erro: {ex.Message}";
            }
            return View(Cidades);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}