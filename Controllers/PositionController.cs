using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Position;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository position;

        public PositionController(IPositionRepository position)
        {
            this.position = position;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        {
            try
            {
                var getAccordance = await position.GetAllPosition();
                if (getAccordance == null) { return BadRequest("There is not a data!"); }
                return Ok(getAccordance);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("getCountPosition")]
        public async Task<IActionResult> GetCountPosition()
        {
            try
            {
                var getAccordance = await position.GetCountPosition();
                if (getAccordance == null) { return BadRequest("There is not a data!"); }
                return Ok(getAccordance);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getPositionById = await position.GetPositionById(id);
                if (getPositionById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getPositionById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addPosition")]
        public async Task<IActionResult> AddPosition(POSITION PositionDTO)
        {
            try
            {
                if (PositionDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addPosition = await position.AddPosition(PositionDTO);

                if (addPosition == null) return BadRequest("There is a problem with the data!");
                return Ok(addPosition);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updatePosition/{id}")]
        public async Task<IActionResult> UpdatePosition(POSITION PositionDTO, string id)
        {
            try
            {
                if (PositionDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updatePosition = await position.UpdatePosition(PositionDTO, id);

                if (updatePosition == null) return BadRequest("There is a problem with the data!");
                return Ok(updatePosition);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deletePosition/{id}")]
        public async Task<IActionResult> RemovePosition(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deletePosition = await position.RemovePosition(id);

                if (deletePosition == null) return BadRequest("There is a problem with the data!");
                return Ok(deletePosition);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
