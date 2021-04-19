using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public interface IJwtService
    {
        JwtToken GenerateSecurityToken(User user);
        Task<RefreshToken> GenerateRefreshToken(User user);
        Task<bool> RemoveToken(string refreshToken);
        Task<RefreshToken> RefreshToken(User user, string refreshToken);
    }
}