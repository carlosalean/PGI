using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using BackEnd_PGI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MaquinasController : ControllerBase
    {
        private readonly IMaquinaRepository _maquinaRepository;

        public MaquinasController(IMaquinaRepository maquinaRepository)
        {
            _maquinaRepository = maquinaRepository;
        }

        // GET: api/Maquinas
        [HttpGet]
        public async Task<IActionResult> GetMaquinas()
        {
            var maquinas = await _maquinaRepository.GetAllAsync();
            return Ok(maquinas);
        }


        // GET: api/Maquinas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaquina(int id)
        {
            var maquina = await _maquinaRepository.GetByIdAsync(id);

            if (maquina == null)
            {
                return NotFound();
            }

            return Ok(maquina);
        }

        // GET: api/Tareas/Caso/5
        [HttpGet("Caso/{idCaso}")]
        public async Task<IActionResult> GetTareaCaso(int idCaso)
        {
            var Tarea = await _maquinaRepository.GetByIdCasoAsync(idCaso);
            return Ok(Tarea);
        }

        // GET: api/Tareas/Caso/5
        [HttpGet("Assets")]
        public async Task<IActionResult> GetMaquinasWithAssets()
        {
            var Tarea = await _maquinaRepository.GetMaquinasWithAssetsAsync();
            return Ok(Tarea);
        }

        // POST: api/Maquinas
        [HttpPost]
        public async Task<IActionResult> PostMaquina([FromBody] Maquina maquina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMaquina = await _maquinaRepository.CreateAsync(maquina);

            return CreatedAtAction(nameof(GetMaquina), new { id = createdMaquina.ID }, createdMaquina);
        }

        // PUT: api/Maquinas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(int id, [FromBody] Maquina maquina)
        {
            if (id != maquina.ID)
            {
                return BadRequest();
            }

            await _maquinaRepository.UpdateAsync(maquina);

            return NoContent();
        }

        // DELETE: api/Maquinas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            await _maquinaRepository.DeleteAsync(id);
            return NoContent();
        }

    }

}
