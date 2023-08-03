using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Users;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository user;

        public UserController(IUserRepository user)
        {
            this.user = user;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var getUsers = await user.GetAllUsers();
                if (getUsers == null) { return BadRequest("There is not a data!"); }
                return Ok(getUsers);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getUserById = await user.GetUsersById(id);
                if (getUserById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getUserById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers([FromForm] USERS USERSDTO)
        {
            try
            {
                if (USERSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addUSERS = await user.AddUsers(USERSDTO);

                if (addUSERS == null) return BadRequest("There is a problem with the data!");
                return Ok(addUSERS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers([FromForm] USERS USERSDTO, string id)
        {
            try
            {
                if (USERSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateUSERS = await user.UpdateUsers(USERSDTO, id);

                if (updateUSERS == null) return BadRequest("There is a problem with the data!");
                return Ok(updateUSERS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteUSERS = await user.RemoveUsers(id);

                if (deleteUSERS == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteUSERS);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
