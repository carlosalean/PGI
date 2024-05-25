using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _assetRepository.GetAllAsync();
            return Ok(assets);
        }

        // GET: api/Tareas/Caso/5
        [HttpGet("IOCs/{id}")]
        public async Task<IActionResult> GetAssetWithIOC(int id)
        {
            var Tarea = await _assetRepository.GetAssetWithIOCsAsync(id);
            return Ok(Tarea);
        }

        // GET: api/Tareas/Caso/5
        [HttpGet("Caso/{idCaso}")]
        public async Task<IActionResult> GetTareaCaso(int idCaso)
        {
            var Tarea = await _assetRepository.GetByIdCasoAsync(idCaso);
            return Ok(Tarea);
        }

        // GET: api/Tareas/Maquina/5
        [HttpGet("Maquina/{idMaquina}")]
        public async Task<IActionResult> GetTareaMaquina(int idMaquina)
        {
            var Tarea = await _assetRepository.GetByIdMaquinaAsync(idMaquina);
            return Ok(Tarea);
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset(int id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        // POST: api/Assets
        [HttpPost]
        public async Task<IActionResult> PostAsset([FromBody] Asset asset)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var createdAsset = await _assetRepository.CreateAsync(asset);

            return CreatedAtAction(nameof(GetAsset), new { id = createdAsset.ID }, createdAsset);
        }

        // PUT: api/Assets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, [FromBody] Asset asset)
        {
            if (id != asset.ID)
            {
                return BadRequest();
            }

            await _assetRepository.UpdateAsync(asset);

            return NoContent();
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            await _assetRepository.DeleteAsync(id);
            return NoContent();
        }

    }

}
