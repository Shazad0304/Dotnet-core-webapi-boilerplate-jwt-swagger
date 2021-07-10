using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cronicle.Models
{
    public class ResetPassword
    {
        [Required]
        public string code { get; set; }

        [Required]
        public string newPassword { get; set; }
    }
}
