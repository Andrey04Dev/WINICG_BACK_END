using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO.Certification;
using webapi.Interfaces.Certification;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationController : ControllerBase
    {
        private readonly ICertificationRepository certification;

        public CertificationController(ICertificationRepository certification)
        {
            this.certification = certification;
        }
        [HttpGet("getCountCertification")]
        public async Task<IActionResult> GetCountCertification()
        {
            try
            {
                var getCertification = await certification.GetCountCertification();
                if (getCertification == null) { return BadRequest("There is not a data!"); }
                return Ok(getCertification);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCertification()
        {
            try
            {
                var getCertification = await certification.GetAllCertification();
                if (getCertification == null) { return BadRequest("There is not a data!"); }
                return Ok(getCertification);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificationById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getCertificationById = await certification.GetCertificationById(id);
                if (getCertificationById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getCertificationById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("addCertification")]
        public async Task<IActionResult> AddCertification( CERTIFICATION certificationDTO)
        {
            try
            {
                if (certificationDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addCertification = await certification.AddCertification(certificationDTO);

                if (addCertification == null) return BadRequest("There is a problem with the data!");
                return Ok(addCertification);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateCertification/{id}")]
        public async Task<IActionResult> UpdateCertification( CERTIFICATION certificationDTO, string id)
        {
            try
            {
                if (certificationDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateCertification = await certification.UpdateCertification(certificationDTO, id);

                if (updateCertification == null) return BadRequest("There is a problem with the data!");
                return Ok(updateCertification);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteCertification/{id}")]
        public async Task<IActionResult> RemoveCertification(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteCertification = await certification.RemoveCertification(id);

                if (deleteCertification == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteCertification);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
