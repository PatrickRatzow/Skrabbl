using System;

namespace Skrabbl.Model.Dto
{
    [Serializable]
    public class LoginResponseDto
    {
        public Jwt Jwt { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}