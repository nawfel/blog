using blog.models.blogComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.repository
{
    public interface IBlogCommentRepository
    {
        public Task<BlogComment> UpsertAsync(BlogCommentCreate blogCommentCreate, int ApplicationUserId);
        public Task<List<BlogComment>> GetAllAsync(int blogId);
        public Task<BlogComment> GetAsync(int blogCommentId);
        public Task<int> DeleteAsync(int blogCommentId);

    }
}
