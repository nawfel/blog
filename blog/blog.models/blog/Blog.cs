using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.blog
{
    public class Blog

    {

        public string Username { get; set; }
        public int ApplicationUserId  { get; set; }
        public int PhotoId { get; set; }
        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
