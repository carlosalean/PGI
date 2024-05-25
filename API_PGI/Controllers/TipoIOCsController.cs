using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoIOCsController : ControllerBase
    {
        private readonly ITipoIOCRepository _tipoIOCRepository;

        public TipoIOCsController(ITipoIOCRepository tipoIOCRepository)
        {
            _tipoIOCRepository = tipoIOCRepository;
        }

        // GET: api/TipoIOCs
        [HttpGet]
        public async Task<IActionResult> GetTipoIOCs()
        {
            var tipoIOCs = await _tipoIOCRepository.GetAllAsync();
            return Ok(tipoIOCs);
        }

        // GET: api/TipoIOCs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoIOC(int id)
        {
            var tipoIOC = await _tipoIOCRepository.GetByIdAsync(id);

            if (tipoIOC == null)
            {
                return NotFound();
            }

            return Ok(tipoIOC);
        }

        // POST: api/TipoIOCs
        [HttpPost]
        public async Task<IActionResult> PostTipoIOC([FromBody] TipoIOC tipoIOC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTipoIOC = await _tipoIOCRepository.CreateAsync(tipoIOC);

            return CreatedAtAction(nameof(GetTipoIOC), new { id = createdTipoIOC.ID }, createdTipoIOC);
        }

        // PUT: api/TipoIOCs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIOC(int id, [FromBody] TipoIOC tipoIOC)
        {
            if (id != tipoIOC.ID)
            {
                return BadRequest();
            }

            await _tipoIOCRepository.UpdateAsync(tipoIOC);

            return NoContent();
        }

        // DELETE: api/TipoIOCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoIOC(int id)
        {
            await _tipoIOCRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
