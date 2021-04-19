using System;

namespace Skrabbl.Model.Dto
{
    [Serializable]
    public class LoginResponseDto
    {
        public JwtToken JwtToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}