using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketModule.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
