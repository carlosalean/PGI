using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IOCsController : ControllerBase
    {
        private readonly IIOCRepository _iocRepository;

        public IOCsController(IIOCRepository iocRepository)
        {
            _iocRepository = iocRepository;
        }

        // GET: api/IOCs
        [HttpGet]
        public async Task<IActionResult> GetIOCs()
        {
            var iocs = await _iocRepository.GetAllAsync();
            return Ok(iocs);
        }

        // GET: api/IOCs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIOC(int id)
        {
            var ioc = await _iocRepository.GetByIdAsync(id);

            if (ioc == null)
            {
                return NotFound();
            }

            return Ok(ioc);
        }

        // POST: api/IOCs
        [HttpPost]
        public async Task<IActionResult> PostIOC([FromBody] IOC ioc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdIOC = await _iocRepository.CreateAsync(ioc);

            return CreatedAtAction(nameof(GetIOC), new { id = createdIOC.ID }, createdIOC);
        }

        // PUT: api/IOCs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIOC(int id, [FromBody] IOC ioc)
        {
            if (id != ioc.ID)
            {
                return BadRequest();
            }

            await _iocRepository.UpdateAsync(ioc);

            return NoContent();
        }

        // DELETE: api/IOCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIOC(int id)
        {
            await _iocRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
