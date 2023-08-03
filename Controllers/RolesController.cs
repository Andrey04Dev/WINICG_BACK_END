using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO.Audits;
using webapi.DTO.Roles;
using webapi.Interfaces.Roles;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _role;
        private readonly IMapper mapper;

        public RolesController(IRoleRepository role, IMapper _mapper)
        {
            _role = role;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var getRoles = await _role.GetAllRoles();
            //var result = _mapper.Map<List<ListAuditsDTO>>(getRoles);
            return Ok(getRoles);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesById(string id)
        {
            var getRolesByIDd = await _role.GetRolesById(id);
            return Ok(getRolesByIDd);

        }
        [HttpPost]
        public async Task<IActionResult> AddRoles([FromForm] AddRolesDTO roles)
        {
            var getRole = mapper.Map<ROLES>(roles);
            var addRole = await _role.AddRoles(getRole);
            return Ok(addRole);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromForm] AddRolesDTO roles, string id)
        {
            var getRole = mapper.Map<ROLES>(roles);
            var updateRole = await _role.UpdateRoles(getRole,id);
            return Ok(updateRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole( string id)
        {
            var deleteRole = await _role.RemoveRoles(id);
            return Ok(deleteRole);
        }
    }
}
