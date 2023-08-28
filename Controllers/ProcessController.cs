using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Process;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessRepository process;

        public ProcessController(IProcessRepository process)
        {
            this.process = process;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProcess()
        {
            try
            {
                var getProcess = await process.GetAllProcess();
                if (getProcess == null) { return BadRequest("There is not a data!"); }
                return Ok(getProcess);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getProcessById = await process.GetProcessById(id);
                if (getProcessById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getProcessById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addProcess")]
        public async Task<IActionResult> AddProcess(PROCESS PROCESSDTO)
        {
            try
            {
                if (PROCESSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addPROCESS = await process.AddProcess(PROCESSDTO);

                if (addPROCESS == null) return BadRequest("There is a problem with the data!");
                return Ok(addPROCESS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateProcess/{id}")]
        public async Task<IActionResult> UpdateProcess(PROCESS PROCESSDTO, string id)
        {
            try
            {
                if (PROCESSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updatePROCESS = await process.UpdateProcess(PROCESSDTO, id);

                if (updatePROCESS == null) return BadRequest("There is a problem with the data!");
                return Ok(updatePROCESS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteProcess/{id}")]
        public async Task<IActionResult> RemoveProcess(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deletePROCESS = await process.RemoveProcess(id);

                if (deletePROCESS == null) return BadRequest("There is a problem with the data!");
                return Ok(deletePROCESS);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
