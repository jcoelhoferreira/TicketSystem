using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class NewTicketViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ?Username { get; set; }

    }
}
