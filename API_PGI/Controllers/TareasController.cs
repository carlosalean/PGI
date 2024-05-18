using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : Controller
    {
        private readonly ITareaRepository _tareaRepository;

        public TareasController(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            var Tareas = await _tareaRepository.GetAllAsync();
            return Ok(Tareas);
        }

        // GET: api/Tareas/Caso/5
        [HttpGet("Caso/{idCaso}")]
        public async Task<IActionResult> GetTareaCaso(int idCaso)
        {
            var Tarea = await _tareaRepository.GetByIdCasoAsync(idCaso);
            return Ok(Tarea);
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarea(int id)
        {
            var Tarea = await _tareaRepository.GetByIdAsync(id);
            return Ok(Tarea);
        }


        // POST: api/Tareas
        [HttpPost]
        public async Task<IActionResult> PostTarea([FromBody] Tarea Tarea)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var createdTarea = await _tareaRepository.CreateAsync(Tarea);

            return CreatedAtAction(nameof(GetTarea), new { id = createdTarea.ID }, createdTarea);
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, [FromBody] Tarea tarea)
        {
            if (id != tarea.ID)
            {
                return BadRequest();
            }

            await _tareaRepository.UpdateAsync(tarea);

            return NoContent();
        }

        // DELETE: api/Tareas/5/6
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var success = await _tareaRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"No se encontró la tarea con ID {id} ");
            }

            return NoContent();
        }
    }
}
