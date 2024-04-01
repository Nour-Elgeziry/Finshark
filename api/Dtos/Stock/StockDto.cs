using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be longer than 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company name cannot be longer than 10 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000, ErrorMessage = "Purchase must be between 1 and 1000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "Last Div must be between 0.001 and 100")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be longer than 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 1000000000, ErrorMessage = "Market Cap must be between 1 and 1000000000")]
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; } = [];
    }
}