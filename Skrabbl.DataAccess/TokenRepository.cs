using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        private readonly ICommandText _commandText;

        public TokenRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(_commandText.AddRefreshToken, new
                    {
                        refreshToken.Token,
                        refreshToken.ExpiresAt,
                        UserId = refreshToken.User.Id
                    }
                )
            );
        }

        public async Task<bool> RemoveRefreshToken(string refreshToken)
        {
            return await WithConnection(async conn =>
            {
                var deletedRows = await conn.ExecuteAsync(_commandText.RemoveRefreshToken, new
                {
                    Token = refreshToken
                });

                return deletedRows > 0;
            });
        }

        public async Task RemoveAllRefreshTokensForUser(User user)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(_commandText.RemoveAllRefreshTokensForUser, new
                    {
                        UserId = user.Id
                    }
                )
            );
        }

        public async Task RemoveAllExpiredRefreshTokens()
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(_commandText.RemoveAllExpiredRefreshTokens)
            );
        }
    }
}