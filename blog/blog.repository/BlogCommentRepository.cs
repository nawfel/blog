using blog.models.blogComment;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.repository
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly IConfiguration _config;

        public BlogCommentRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<int> DeleteAsync(int blogCommentId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync("BlogComment_GetAll", new { BlogCommentId = blogCommentId },
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<List<BlogComment>> GetAllAsync(int blogId)
        {
            IEnumerable<BlogComment> blogComment;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                blogComment = await connection.QueryAsync<BlogComment>("Photo_GetByUserId", new { BlogId = blogId },
                    commandType: CommandType.StoredProcedure);
            }
            return blogComment.ToList();
        }

        public async Task<BlogComment> GetAsync(int blogCommentId)
        {
            BlogComment blogComment;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                blogComment = await connection.QueryFirstOrDefaultAsync<BlogComment>("BlogComment_Get", new { BlogCommentId = blogCommentId },
                    commandType: CommandType.StoredProcedure);
            }
            return blogComment;
        }

        public async Task<BlogComment> UpsertAsync(blogCommentCreate blogCommentCreate, int ApplicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("BlogCommentId", typeof(int));
            dataTable.Columns.Add("ParentBlogCommentId", typeof(int));
            dataTable.Columns.Add("BlogId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));

            int? newBlogCommentId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newBlogCommentId = await connection.ExecuteScalarAsync<int?>(
                    "BlogComment_Upsert",
                    new { Photo = dataTable.AsTableValuedParameter("dbo.BlogCommentType") },
                    commandType: CommandType.StoredProcedure);
            }
            newBlogCommentId = newBlogCommentId ?? blogCommentCreate.BlogCommentId;
            BlogComment blogComment = await GetAsync(newBlogCommentId.Value);
            
            return blogComment;
        }
    }
}
