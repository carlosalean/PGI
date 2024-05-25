using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoAssetsController : ControllerBase
    {
        private readonly ITipoAssetRepository _tipoAssetRepository;

        public TipoAssetsController(ITipoAssetRepository tipoAssetRepository)
        {
            _tipoAssetRepository = tipoAssetRepository;
        }

        // GET: api/TipoAssets
        [HttpGet]
        public async Task<IActionResult> GetTipoAssets()
        {
            var tipoAssets = await _tipoAssetRepository.GetAllAsync();
            return Ok(tipoAssets);
        }

        // GET: api/TipoAssets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoAsset(int id)
        {
            var tipoAsset = await _tipoAssetRepository.GetByIdAsync(id);

            if (tipoAsset == null)
            {
                return NotFound();
            }

            return Ok(tipoAsset);
        }

        // POST: api/TipoAssets
        [HttpPost]
        public async Task<IActionResult> PostTipoAsset([FromBody] TipoAsset tipoAsset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTipoAsset = await _tipoAssetRepository.CreateAsync(tipoAsset);

            return CreatedAtAction(nameof(GetTipoAsset), new { id = createdTipoAsset.ID }, createdTipoAsset);
        }

        // PUT: api/TipoAssets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAsset(int id, [FromBody] TipoAsset tipoAsset)
        {
            if (id != tipoAsset.ID)
            {
                return BadRequest();
            }

            await _tipoAssetRepository.UpdateAsync(tipoAsset);

            return NoContent();
        }

        // DELETE: api/TipoAssets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAsset(int id)
        {
            await _tipoAssetRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
