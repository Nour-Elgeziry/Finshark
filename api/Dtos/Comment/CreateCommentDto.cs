using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(280, ErrorMessage = "Title cannot be longer than 280 characters")]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(280, ErrorMessage = "Content cannot be longer than 280 characters")]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
        public string Content { get; set; } = string.Empty;

    }
}