using System;
using System.Text.Json.Serialization;

namespace Skrabbl.Model
{
    [Serializable]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        [JsonIgnore] public User User { get; set; }
    }
}