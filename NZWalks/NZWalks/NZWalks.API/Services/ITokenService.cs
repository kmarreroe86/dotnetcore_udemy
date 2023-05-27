using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}