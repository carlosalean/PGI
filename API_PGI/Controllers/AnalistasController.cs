using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalistasController : Controller
    {
        private readonly IAnalistaRepository _analistaRepository;

        public AnalistasController(IAnalistaRepository analistaRepository)
        {
            _analistaRepository = analistaRepository;
        }

        // GET: api/Analistas
        [HttpGet]
        public async Task<IActionResult> GetAnalistas()
        {
            var Tareas = await _analistaRepository.GetAllAsync();
            return Ok(Tareas);
        }

        // GET: api/Analistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnalista(int id)
        {
            var Analista = await _analistaRepository.GetByIdAsync(id);

            if (Analista == null)
            {
                return NotFound();
            }

            return Ok(Analista);
        }

        // POST: api/Analistas
        [HttpPost]
        public async Task<IActionResult> PostAnalista([FromBody] Analista analista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTarea = await _analistaRepository.CreateAsync(analista);

            return CreatedAtAction(nameof(GetAnalista), new { id = createdTarea.ID }, createdTarea);
        }

        // PUT: api/Analistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalista(int id, [FromBody] Analista analista)
        {
            if (id != analista.ID)
            {
                return BadRequest();
            }

            await _analistaRepository.UpdateAsync(analista);

            return NoContent();
        }

        // DELETE: api/Analistas/5/6
        [HttpDelete("{idcaso}/{idanalista}")]
        public async Task<IActionResult> DeleteAnalista(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Identificadores inválidos.");
            }
            await _analistaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
