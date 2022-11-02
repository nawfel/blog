using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.models.photo
{
    internal class PhotoCreate
    {
        public string ImageUrl { get; set; }

        public string PublicId { get; set; }
        public string Description { get; set; }
    }
}
