using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using webapi.DTO.Audits;
using webapi.Interfaces.Audits;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository audit;

        public AuditController(IAuditRepository audit)
        {
            this.audit = audit;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAudit()
        {
            var getAudit =  await audit.GetAllAudits();
            return Ok(getAudit);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditById(string id)
        {
            var getAuditById = await audit.GetAuditsById(id);
            return Ok(getAuditById);
        }

        [HttpPost]
        public async Task<IActionResult> AddAudit([FromForm] AUDITS addAuditsDTO)
        {
            if (addAuditsDTO == null)
            {
                return BadRequest("No ingrese valores en blanco");
            }
            var addAudit = await audit.AddAudits(addAuditsDTO);
            return Ok(addAudit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAudit([FromForm] AUDITS addAuditDTO, string id)
        {
            if ( addAuditDTO == null)
            {
                return BadRequest("El elemento esta vacio");
            }
            var update = await audit.UpdateAudits(addAuditDTO, id);
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudit(string id)
        {
            if (!string.IsNullOrEmpty(id)) return BadRequest("El id esta vacio");

            var deleteUser = await audit.RemoveAudits(id);

            if (deleteUser == null)
            {
                return BadRequest("No se pudo borrar la auditoria");
            }
            return Ok("La audiencia ha sido borrado.");
        }
    }
}
