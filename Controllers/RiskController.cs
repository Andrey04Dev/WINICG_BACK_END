using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using webapi.DTO.Files;
using webapi.DTO.Risks;
using webapi.Interfaces.Files;
using webapi.Interfaces.Risks;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        private readonly IRisksRepository risks;
        private readonly IFileRepository file;
        private readonly IMapper mapper;

        public RiskController(IRisksRepository risks, IFileRepository file, IMapper mapper)
        {
            this.risks = risks;
            this.file = file;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRisks()
        {
            try
            {
                var getRisks = await risks.GetAllRisks();
                if (getRisks == null) { return BadRequest("There is not a data!"); }
                return Ok(getRisks);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("getCountRisks")]
        public async Task<IActionResult> GetCountRisks()
        {
            try
            {
                var getRisks = await risks.GetCountRisks();
                if (getRisks == null) { return BadRequest("There is not a data!"); }
                return Ok(getRisks);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRisksById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getRisksById = await risks.GetRisksById(id);
                if (getRisksById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getRisksById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addRisk")]
        public async Task<IActionResult> AddRisks(AddRisksDTO RISKSDTO)
        {
            try
            {
                if (RISKSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var getRisk = mapper.Map<RISKS>(RISKSDTO);
                var addRISKS = await risks.AddRisks(getRisk);
                if (addRISKS == null) return BadRequest("There is a problem with the data!");
                //var dtoFile = new List<FILES>();

                //foreach (var item in files)
                //{
                //    FILES fileSave = new FILES();
                //    byte[] fileBytes = await SaveFile(item);

                //    fileSave.FILENAME = Path.GetFileNameWithoutExtension(item.FileName);
                //    fileSave.BINARY_FILE = fileBytes;
                //    fileSave.EXTENSION = Path.GetExtension(item.FileName);
                //    fileSave.IDMODULE = getRisk.IDRISKS;

                //    dtoFile.Add(fileSave);

                //    fileSave = null;

                //}
                //var fileResult = await file.AddFiles(dtoFile, getRisk.IDRISKS);
                return Ok(addRISKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateRisk/{id}")]
        public async Task<IActionResult> UpdateRisk( RISKS RISKSDTO, string id)
        {
            try
            {
                if (RISKSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateRISKS = await risks.UpdateRisks(RISKSDTO, id);

                if (updateRISKS == null) return BadRequest("There is a problem with the data!");
                return Ok(updateRISKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteRisk/{id}")]
        public async Task<IActionResult> RemoveRisks(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteRISKS = await risks.RemoveRisks(id);

                if (deleteRISKS == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteRISKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private async Task<byte[]> SaveFile(IFormFile formFile)
        {
            if (formFile == null)
            {
                return null;
            }
            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            var fileBytes = stream.ToArray();
            return fileBytes;
            //return await storageRepository.CreateStorage(fileBytes, formFile.ContentType, Path.GetExtension(formFile.FileName), ConstantApp.FileContainer, Guid.NewGuid().ToString());
        }
    }
}
