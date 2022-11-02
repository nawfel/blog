using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.blogComment
{
    internal class blogCommentCreate
    {
        public int BlogCommentId { get; set; }
        public int? ParentBlogCommentId { get; set; }
        public int BlogId { get; set; }


        [Required(ErrorMessage = "content is required")]
        [MinLength(10, ErrorMessage = "leat 5 char")]
        [MaxLength(3000, ErrorMessage = "most 3000 char")]
        public string Content { get; set; }
    }
}
