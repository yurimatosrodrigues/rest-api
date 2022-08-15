using RestAPI.Models;
using System.Text;
using System.Text.Json;

namespace RestAPI.Repository
{
    public class CidadeRepository
    {
        private static List<Cidade> cidades;
        private const string EnderecoJson = "C:\\repos\\rest\\RestAPI\\RestAPI\\cidades.json";

        public static List<Cidade> Cidades {
            get { 
                if(cidades == null)
                {
                    string jsonString = File.ReadAllText(EnderecoJson);

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        cidades = JsonSerializer.Deserialize<List<Cidade>>(jsonString);
                    }
                    else
                    {
                        CarregarCidade();
                    }
                    return cidades;
                }
                else return cidades;
            }             
        }

        private static void CarregarCidade()
        {
            cidades = new List<Cidade>()
            {
                new Cidade(){
                    Id = 100,
                    Nome = "Santos",
                    IdEstado = 11,
                    IdPais = 55,
                    Populacao = 10000
                },
                new Cidade(){
                    Id = 200,
                    Nome = "São Vicente",
                    IdEstado = 11,
                    IdPais = 55,
                    Populacao = 20000
                },
                new Cidade(){
                    Id = 100,
                    Nome = "Belo Horizonte",
                    IdEstado = 31,
                    IdPais = 55,
                    Populacao = 30000
                },
            };
        }
        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize(cidades);
            File.WriteAllText(EnderecoJson, jsonString, Encoding.UTF8);
        }
    }
}