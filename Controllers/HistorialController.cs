using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO.Audits;
using webapi.DTO.History;
using webapi.Interfaces.Historial;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly IHistorialRepository historial;
        private readonly IMapper mapper;

        public HistorialController(IHistorialRepository historial, IMapper mapper)
        {
            this.historial = historial;
            this.mapper = mapper;
        }

        [HttpPost("addHistory")]
        public async Task<IActionResult> AddHistory(AddHistoryDTO history)
        {
            if (history == null)
            {
                return BadRequest("No ingrese valores en blanco");
            }
            var getHistory = mapper.Map<HISTORIAL>(history);
            var addHistory = await historial.CreateHistorialAsync(getHistory);
            return Ok(addHistory);
        }

        [HttpGet("getHistorialById/{id}")]
        public async Task<IActionResult> GetHistoryById(string id)
        {
            var getHistoralById = await historial.GetHistorialByIdModule(id);
            return Ok(getHistoralById);
        }
    }
}
