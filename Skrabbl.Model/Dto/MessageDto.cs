using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.Model.Dto
{
    public class MessageDto
    {
        [Required]
        [MaxLength(255)]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
