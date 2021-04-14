using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skrabbl.Model.Dto
{
    public class WordDto
    {
        [Required]
        public string Word { get; set; }
    }
}
