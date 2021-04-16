using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skrabbl.Model.Dto
{
    public class JoinLobbyDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string LobbyCode { get; set; }
    }
}
