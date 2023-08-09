using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Company_position;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyPositionController : ControllerBase
    {
        private readonly ICompanyPositionRepository company;

        public CompanyPositionController(ICompanyPositionRepository company)
        {
            this.company = company;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCompanyPosition()
        {
            try
            {
                var getCompany = await company.GetAllCompanyPosition();
                if (getCompany == null) { return BadRequest("There is not a data!"); }
                return Ok(getCompany);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyPositionById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getCompanynById = await company.GetCompanyPositionByID(id);
                if (getCompanynById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getCompanynById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addCompanyPosition")]
        public async Task<IActionResult> AddCompanyPosition([FromForm] COMPANY_POSITION position)
        {
            try
            {
                if (position == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addCompany = await company.AddCompanyPosition(position);

                if (addCompany == null) return BadRequest("There is a problem with the data!");
                return Ok(addCompany);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateCompanyPosition/{id}")]
        public async Task<IActionResult> UpdateCompanyPosition([FromForm] COMPANY_POSITION companyDTO, string id)
        {
            try
            {
                if (companyDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateCompanyPosition = await company.UpdateCompanyPosition(companyDTO, id);

                if (updateCompanyPosition == null) return BadRequest("There is a problem with the data!");
                return Ok(updateCompanyPosition);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteCompanyPosition/{id}")]
        public async Task<IActionResult> RemoveCompanyPosition(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteCompanyPosition = await company.RemoveCompanyPosition(id);

                if (deleteCompanyPosition == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteCompanyPosition);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
