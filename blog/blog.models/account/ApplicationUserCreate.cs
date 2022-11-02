using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.account
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
       
        [MinLength(5, ErrorMessage = "leat 5 char")]
        [MaxLength(20, ErrorMessage = "most 20 char")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "email is invalid")]
        [EmailAddress( ErrorMessage = "leat 8 char")]
        [MaxLength(30, ErrorMessage = "most 30 char")]
        public string Email { get; set; }
    }
}
