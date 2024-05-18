using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasosController : ControllerBase
    {
        private readonly ICasoRepository _casoRepository;

        public CasosController(ICasoRepository casoRepository)
        {
            _casoRepository = casoRepository;
        }

        // GET: api/Casos
        [HttpGet]
        public async Task<IActionResult> GetCasos()
        {
            var casos = await _casoRepository.GetAllAsync();
            return Ok(casos);
        }

        // GET: api/Casos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaso(int id)
        {
            var caso = await _casoRepository.GetByIdAsync(id);

            if (caso == null)
            {
                return NotFound();
            }

            return Ok(caso);
        }

        // POST: api/Casos
        [HttpPost]
        public async Task<IActionResult> PostCaso([FromBody] Caso caso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCaso = await _casoRepository.CreateAsync(caso);

            return CreatedAtAction(nameof(GetCaso), new { id = createdCaso.ID }, createdCaso);
        }

        // PUT: api/Casos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaso(int id, [FromBody] Caso caso)
        {
            if (id != caso.ID)
            {
                return BadRequest();
            }

            await _casoRepository.UpdateAsync(caso);

            return NoContent();
        }

        // DELETE: api/Casos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaso(int id)
        {
            await _casoRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
