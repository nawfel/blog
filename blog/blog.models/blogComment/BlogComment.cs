using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.blogComment
{
    internal class BlogComment :blogCommentCreate
    {
        public int Username { get; set; }

        public int ApplicationUserId { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
