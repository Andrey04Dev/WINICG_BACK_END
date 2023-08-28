using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Flags;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {
        private readonly IFlagsRepository flags;

        public FlagController(IFlagsRepository flags)
        {
            this.flags = flags;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFlags()
        {
            try
            {
                var getFlags = await flags.GetAllFlags();
                if (getFlags == null) { return BadRequest("There is not a data!"); }
                return Ok(getFlags);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlagById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getFlagById = await flags.GetFlagsById(id);
                if (getFlagById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getFlagById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addFlag")]
        public async Task<IActionResult> AddFlag( FLAGS flagDTO)
        {
            try
            {
                if (flagDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addFlag = await flags.AddFlags(flagDTO);

                if (addFlag == null) return BadRequest("There is a problem with the data!");
                return Ok(addFlag);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateFlag/{id}")]
        public async Task<IActionResult> UpdateFlag( FLAGS flagDTO, string id)
        {
            try
            {
                if (flagDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateFlag = await flags.UpdateFlags(flagDTO, id);

                if (updateFlag == null) return BadRequest("There is a problem with the data!");
                return Ok(updateFlag);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteFlag/{id}")]
        public async Task<IActionResult> RemoveFlag(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteFlag = await flags.RemoveFlags(id);

                if (deleteFlag == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteFlag);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
