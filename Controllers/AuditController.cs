using AutoMapper;
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
        private readonly IMapper mapper;

        public AuditController(IAuditRepository audit, IMapper mapper)
        {
            this.audit = audit;
            this.mapper = mapper;
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

        [HttpPost("addAudit")]
        public async Task<IActionResult> AddAudit(AddAuditsDTO addAuditsDTO)
        {
            if (addAuditsDTO == null)
            {
                return BadRequest("No ingrese valores en blanco");
            }
            var getAudits = mapper.Map<AUDITS>(addAuditsDTO);   
            var addAudit = await audit.AddAudits(getAudits);
            return Ok(addAudit);
        }

        [HttpPut("updateAudit/{id}")]
        public async Task<IActionResult> UpdateAudit(AUDITS addAuditDTO, string id)
        {
            if ( addAuditDTO == null)
            {
                return BadRequest("El elemento esta vacio");
            }
            var update = await audit.UpdateAudits(addAuditDTO, id);
            return Ok(update);
        }

        [HttpDelete("deleteAudit/{id}")]
        public async Task<IActionResult> DeleteAudit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("El id esta vacio");

            var deleteUser = await audit.RemoveAudits(id);

            if (deleteUser == null)
            {
                return BadRequest("No se pudo borrar la auditoria");
            }
            return Ok("La audiencia ha sido borrado.");
        }
    }
}
