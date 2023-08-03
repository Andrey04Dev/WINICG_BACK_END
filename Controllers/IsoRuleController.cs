using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Isorule;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsoRuleController : ControllerBase
    {
        private readonly IIsoRuleRepository isoRule;

        public IsoRuleController(IIsoRuleRepository isoRule)
        {
            this.isoRule = isoRule;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIsorule()
        {
            try
            {
                var getIsorule = await isoRule.GetAllIsoRule();
                if (getIsorule == null) { return BadRequest("There is not a data!"); }
                return Ok(getIsorule);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getIsoruleById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getIsoruleById = await isoRule.GetIsoRuleById(id);
                if (getIsoruleById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getIsoruleById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIsoule([FromForm] ISORULE ISORULEDTO)
        {
            try
            {
                if (ISORULEDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addISORULE = await isoRule.AddIsoRule(ISORULEDTO);

                if (addISORULE == null) return BadRequest("There is a problem with the data!");
                return Ok(addISORULE);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIsorule([FromForm] ISORULE ISORULEDTO, string id)
        {
            try
            {
                if (ISORULEDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateISORULE = await isoRule.UpdateIsoRule(ISORULEDTO, id);

                if (updateISORULE == null) return BadRequest("There is a problem with the data!");
                return Ok(updateISORULE);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveIsorule(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteISORULE = await isoRule.RemoveIsoRule(id);

                if (deleteISORULE == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteISORULE);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
