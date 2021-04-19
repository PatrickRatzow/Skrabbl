using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface ITokenRepository
    {
        Task AddRefreshToken(RefreshToken refreshToken);
        Task<bool> RemoveRefreshToken(string refreshToken);
        Task RemoveAllRefreshTokensForUser(User user);
        Task RemoveAllExpiredRefreshTokens();
    }
}