using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Comment;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task<Comment> CreateAsync(Comment commentModel);
        public Task<Comment?> DeleteAsync(int id);
        public Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateDto);
        public Task<bool> CommentExists(int id);
    }
}