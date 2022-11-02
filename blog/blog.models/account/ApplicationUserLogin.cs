using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.account
{
    public  class ApplicationUserLogin
    {
        [Required(ErrorMessage ="Username is required")]
        [MinLength(5,ErrorMessage ="leat 5 char")]
        [MaxLength(20,ErrorMessage ="most 20 char")]
        public string UserName { get; set; }
       
        [Required(ErrorMessage = "password is required")]
        [MinLength(5, ErrorMessage = "leat 8 char")]
        [MaxLength(20, ErrorMessage = "most 50 char")]
        public string Password { get; set; }
    }
}
