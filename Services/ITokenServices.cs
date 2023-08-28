using webapi.Models;

namespace webapi.Services
{
    public interface ITokenServices
    {
        string createToken(USERS user);
    }
}
