using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO.Users;
using webapi.Interfaces.Users;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository user;
        //private readonly ITokenServices token;
        private readonly IMapper mapper;

        public UserController(IUserRepository user, IMapper mapper)
        {
            this.user = user;
            //this.token = token;
            this.mapper = mapper;
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

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUsers(AddUserDTO USERSDTO)
        {
            try
            {
                USERSDTO.EMAIL = USERSDTO.EMAIL.ToLower();
                if (USERSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }

                var getUser = mapper.Map<USERS>(USERSDTO);
                var addUSERS = await user.AddUsers(getUser, USERSDTO.PASSWORD);

                if (addUSERS == null) return BadRequest("There is a problem with the data!");
                return Ok(addUSERS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUsers(UpdateUserDTO USERSDTO, string id)
        {
            try
            {
                if (USERSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var getUser = mapper.Map<USERS>(USERSDTO);
                var updateUSERS = await user.UpdateUsers(getUser, USERSDTO.PASSWORD, id);

                if (updateUSERS == null) return BadRequest("There is a problem with the data!");
                return Ok(updateUSERS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("deleteUser/{id}")]
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
        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> LoginUser(USERS userLoginDTO)
        //{
        //    try
        //    {
        //        var userLogged = await user.Login(userLoginDTO.EMAIL, userLoginDTO.PASSWORD);
        //        if (userLogged == null) { return Unauthorized($"The credentials for {userLoginDTO.EMAIL} are wrong!"); }
        //        var tokenResult = token.createToken(userLogged);



        //        if (userLogged != null && tokenResult != null)
        //        {
        //            return Ok(new { token = tokenResult, name = userLogged.FULLNAME, role = userLogged.ROLE.ROLE });
        //        }
        //        return BadRequest("The tocken expired");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"The message is: {ex.Message}");
        //    }

        //}
    }
}
