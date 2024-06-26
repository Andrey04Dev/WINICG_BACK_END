﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.No_Accordance;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoAccordanceController : ControllerBase
    {
        private readonly INoAccordanceRepository noAccordance;

        public NoAccordanceController(INoAccordanceRepository noAccordance)
        {
            this.noAccordance = noAccordance;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNoAccordance()
        {
            try
            {
                var getAccordance = await noAccordance.GetAllNoAccordance();
                if (getAccordance == null) { return BadRequest("There is not a data!"); }
                return Ok(getAccordance);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("getCountNoAccordance")]
        public async Task<IActionResult> GetCountNoAccordance()
        {
            try
            {
                var getAccordance = await noAccordance.GetCountNoAccordance();
                if (getAccordance == null) { return BadRequest("There is not a data!"); }
                return Ok(getAccordance);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoAccordanceById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getNoAccordanceById = await noAccordance.GetNoAccordanceById(id);
                if (getNoAccordanceById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getNoAccordanceById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addNoAccordance")]
        public async Task<IActionResult> AddNoAccordance( NO_ACCORDANCE NO_ACCORDANCEDTO)
        {
            try
            {
                if (NO_ACCORDANCEDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addNO_ACCORDANCE = await noAccordance.AddNoAccordance(NO_ACCORDANCEDTO);

                if (addNO_ACCORDANCE == null) return BadRequest("There is a problem with the data!");
                return Ok(addNO_ACCORDANCE);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateNoAccordance/{id}")]
        public async Task<IActionResult> UpdateNoAccordance( NO_ACCORDANCE NO_ACCORDANCEDTO, string id)
        {
            try
            {
                if (NO_ACCORDANCEDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateNO_ACCORDANCE = await noAccordance.UpdateNoAccordance(NO_ACCORDANCEDTO, id);

                if (updateNO_ACCORDANCE == null) return BadRequest("There is a problem with the data!");
                return Ok(updateNO_ACCORDANCE);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteNoAccordance/{id}")]
        public async Task<IActionResult> RemoveNoAccordance(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteNO_ACCORDANCE = await noAccordance.RemoveNoAccordance(id);

                if (deleteNO_ACCORDANCE == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteNO_ACCORDANCE);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
