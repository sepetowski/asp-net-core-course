using Microsoft.AspNetCore.Identity;

namespace UdemyCourse.API.Repositories
{
    public interface ITokenRepository
    {
       string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
