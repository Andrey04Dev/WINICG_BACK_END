using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Risks;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        private readonly IRisksRepository risks;

        public RiskController(IRisksRepository risks)
        {
            this.risks = risks;
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
        public async Task<IActionResult> AddRisks([FromForm] RISKS RISKSDTO)
        {
            try
            {
                if (RISKSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addRISKS = await risks.AddRisks(RISKSDTO);

                if (addRISKS == null) return BadRequest("There is a problem with the data!");
                return Ok(addRISKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateRisk/{id}")]
        public async Task<IActionResult> UpdateRisk([FromForm] RISKS RISKSDTO, string id)
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
    }
}
