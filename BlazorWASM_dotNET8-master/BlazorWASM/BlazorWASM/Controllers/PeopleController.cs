using BlazorWASM.Data;
using Compartilhado;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWASM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private ApplicationDbContext _context;
        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Chamada GET que busca as Pessoas
        /// </summary>
        /// <returns>Retorna List<Person></returns>
        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                List<Person> people = _context.Person.ToList();


                int? NumeroNullable = null;
                int x = NumeroNullable ?? 0;


                return Ok(people);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Chamada POST para incluir uma nova Pessoa
        /// </summary>
        /// <param name="p">Pessoa a ser incluída</param>
        /// <returns>Confirmação de cadastro</returns>
        [HttpPost] 
        public async Task<IActionResult> PostPeople([FromBody] Person p)
        {   

            try
            {
                Person p1 = new Person() { Name = p.Name, Email = p.Email };

                _context.Person.Add(p1);
                _context.SaveChanges();

                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Chamada PUT para mudar elementos de uma Pessoa existente
        /// </summary>
        /// <param name="p">Pessoa a ser atualizada</param>
        /// <returns>Confirmação de mudança</returns>
        [HttpPut]
        public async Task<IActionResult> PutPeople([FromBody] Person p)
        {
            try
            {
                Person? UpdtatedPerson = _context.Person.FirstOrDefault(e => e.Id == p.Id);
                
                if (UpdtatedPerson != null)
                {
                    UpdtatedPerson.Name = p.Name;
                    UpdtatedPerson.Email = p.Email;
                    _context.Person.Update(UpdtatedPerson);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Chamada DELETE para excluir uma Pessoa específica
        /// </summary>
        /// <param name="id">Chave para identificar uma Pessoa específica</param>
        /// <returns>Confirmação de exclusão</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeople(int id)
        {
            try
            {
                Person? RemovedPerson = _context.Person.FirstOrDefault(e => e.Id == id);
                if (RemovedPerson != null) 
                { 
                    _context.Person.Remove(RemovedPerson);  
                    _context.SaveChanges();
                    return Ok();

                }

                return NotFound();
                
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
