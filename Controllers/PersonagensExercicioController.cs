using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {            
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };
        
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }


        [HttpGet("GetByNome/{nome}")]
        public IActionResult AcharPorNome(string nome){
            
            personagens = personagens.FindAll(pers => pers.Nome == nome);
            int pers = personagens.Count();
            if(pers > 0){
            return Ok (personagens);
            }
            else
            {
                return NotFound ("Personagem Não Encontrado(NotFound)");
            }
        }

        [HttpPost("PostValidacao")]
        public IActionResult ValidacaoAddPersonagem(Personagem novoPersonagem){

            if(novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia >30){
                return BadRequest ("Não é possível adicionar o personagem (BadRequest)");
            }
            else{
                personagens.Add(novoPersonagem);
            return Ok(personagens);
            }
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult ValidacaoMago(Personagem novoPersonagem){
            
            var mago = ClasseEnum.Mago;
            if(novoPersonagem.Classe == mago && novoPersonagem.Inteligencia < 35 ){
                return BadRequest ();
            }
            else{
                personagens.Add(novoPersonagem);
                return Ok(personagens);
            }
        }
        [HttpGet("GetClerigoMago")]
        public IActionResult ClerigoMago(){
            personagens = personagens.FindAll(x => x.Classe != ClasseEnum.Cavaleiro);
            personagens = personagens.OrderByDescending(x => x.PontosVida).ToList();
            return Ok(personagens);
        }
        [HttpGet("GetEstatisticas")]
        public IActionResult QtdSomaEstatisticas(){
           int qtd = personagens.Count();
           decimal somatoria = personagens.Sum(x=>x.Inteligencia);
           string msg = $"A quantidade de personagens é {qtd} e a somátoria da inteligência {somatoria}";
           return Ok (msg);
        }
        [HttpGet("GetByClasse/{enumId}")]
        public IActionResult EscolhaDaClasse(int enumId){
            ClasseEnum enumDigitado = (ClasseEnum)enumId;
            List<Personagem> listaBusca = personagens.FindAll(x =>x.Classe == enumDigitado);

            return Ok (listaBusca);
        }
    }
}