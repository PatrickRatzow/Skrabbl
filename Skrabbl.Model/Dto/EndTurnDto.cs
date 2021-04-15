using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skrabbl.Model.Dto
{
    public class EndTurnDto
    {
        [Required]
        public DateTime EndAt { get; set; }
    }
}
