using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.blog
{
    internal class BlogCreate
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "leat 5 char")]
        [MaxLength(50, ErrorMessage = "most 20 char")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Username is required")]
        
        public string Content { get; set; }
       
        public int? PhotoId { get; set; }
    }
}
