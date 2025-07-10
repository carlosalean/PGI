using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using IntegrationsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IOCsController : ControllerBase
    {
        private readonly IIOCRepository _iocRepository;
        private readonly IOCServiceFactory _iocServiceFactory;
        private readonly IAssetRepository _assetsRepository;
        private readonly IMockAnalysisService _mockAnalysisService;
        private readonly ITipoIOCRepository _tipoIOCRepository;
        private readonly IThreatDetectionService _threatDetectionService;

        public IOCsController(IIOCRepository iocRepository, 
                              IOCServiceFactory iocServiceFactory, 
                              IAssetRepository assetsRepository, 
                              IMockAnalysisService mockAnalysisService, 
                              ITipoIOCRepository tipoIOCRepository,
                              IThreatDetectionService threatDetectionService)
        {
            _iocRepository = iocRepository;
            _iocServiceFactory = iocServiceFactory;
            _assetsRepository = assetsRepository;
            _mockAnalysisService = mockAnalysisService;
            _tipoIOCRepository = tipoIOCRepository;
            _threatDetectionService = threatDetectionService;
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

        [HttpPost("analyze/case/{casoId}")]
        public async Task<IActionResult> AnalyzeCaseAssets(int casoId)
        {
            // Obtener todos los Assets relacionados con el caso
            var assets = await _assetsRepository.GetByIdCasoAsync(casoId);
            if (assets == null || !assets.Any())
            {
                return NotFound("No se encontraron Assets para el caso especificado.");
            }

            var iocs = new List<IOC>();

            // Analizar cada archivo de manera independiente
            foreach (var asset in assets)
            {
                // Simular la descarga del archivo desde FTP
                var fileData = await _mockAnalysisService.MockDownloadFileFromFtpAsync(asset.Ubicacion);
                if (fileData == null)
                {
                    return BadRequest($"No se pudo simular la descarga del archivo para el asset con ID {asset.ID}.");
                }

                var fileContent = Encoding.UTF8.GetString(fileData);

                // Detectar el tipo de IOC en función del contenido del archivo y el tipo de asset
                var tipoIOC = await _threatDetectionService.DetectThreatAndGetTipoIOCAsync(asset.TipoAssetID, fileContent);
                if (tipoIOC == null)
                {
                    return BadRequest($"No se pudo determinar el TipoIOC para el asset con ID {asset.ID}.");
                }

                // Generar datos de análisis ficticios usando el MockAnalysisService
                var (resumen, descripcion) = await _mockAnalysisService.AnalyzeAssetAsync(asset);

                // Crear un nuevo registro en la tabla IOC para cada análisis realizado
                var ioc = new IOC
                {
                    AssetID = asset.ID,
                    TipoIocId = tipoIOC.ID,
                    Valor = resumen,
                    Descripcion = descripcion,
                    TipoIOC = tipoIOC
                };

                // Guardar el IOC y añadirlo a la lista de resultados
                await _iocRepository.CreateAsync(ioc);
                iocs.Add(ioc);
            }

            // Retornar la lista de IOCs generados para el caso
            return Ok(iocs);
        }


        //[HttpPost("analyze/case/{casoId}")]
        //public async Task<IActionResult> AnalyzeCaseAssets(int casoId)
        //{
        //    // 1. Obtener todos los Assets relacionados con el caso
        //    var assets = await _assetsRepository.GetByIdCasoAsync(casoId);
        //    if (assets == null || !assets.Any())
        //    {
        //        return NotFound("No se encontraron Assets para el caso especificado.");
        //    }

        //    var iocs = new List<IOC>();

        //    // 2. Analizar cada archivo de manera independiente
        //    foreach (var asset in assets)
        //    {
        //        // Descargar el archivo desde el servidor FTP
        //        var fileData = await DownloadFileFromFtpAsync(asset.Ubicacion);
        //        if (fileData == null)
        //        {
        //            return BadRequest($"No se pudo descargar el archivo para el asset con ID {asset.ID}.");
        //        }

        //        // Determinar el tipo de IOC en función del tipo de Asset
        //        var tipoIOC = GetTipoIOCByAsset(asset);
        //        if (tipoIOC == null)
        //        {
        //            return BadRequest($"No se pudo determinar el TipoIOC para el asset con ID {asset.ID}.");
        //        }

        //        // Obtener el servicio adecuado y analizar el archivo
        //        var service = _iocServiceFactory.GetService(tipoIOC.BuscarEn.Value);
        //        (string resumen, string descripcion) = await service.AnalyzeFileAsync(fileData, asset.Ubicacion);

        //        // Crear un nuevo registro en la tabla IOC para cada análisis realizado
        //        var ioc = new IOC
        //        {
        //            TipoIocId = tipoIOC.ID,
        //            Valor = resumen,
        //            Descripcion = descripcion,
        //            TipoIOC = tipoIOC
        //        };

        //        // Guardar el IOC y añadirlo a la lista de resultados
        //        await _iocRepository.CreateAsync(ioc);
        //        iocs.Add(ioc);
        //    }

        //    // Retornar la lista de IOCs generados para el caso
        //    return Ok(iocs);
        //}

        //private async Task<byte[]> DownloadFileFromFtpAsync(string fileName)
        //{
        //    var ftpUrl = $"ftp://tu-servidor-ftp/{fileName}";
        //    var request = (FtpWebRequest)WebRequest.Create(ftpUrl);
        //    request.Method = WebRequestMethods.Ftp.DownloadFile;
        //    request.Credentials = new NetworkCredential("ftp-username", "ftp-password");

        //    try
        //    {
        //        using var response = await request.GetResponseAsync();
        //        using var stream = response.GetResponseStream();
        //        using var memoryStream = new MemoryStream();
        //        await stream.CopyToAsync(memoryStream);
        //        return memoryStream.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error descargando el archivo desde FTP: {ex.Message}");
        //        return null;
        //    }
        //}


    }

}
