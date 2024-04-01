using api.Mappers;
using api.Interfaces;
using api.Dtos.Comment;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {

        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IStockRepository _stockRepository = stockRepository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments.Select(c => c.ToCommentDto()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            // check if stock exists
            var stockExists = await _stockRepository.StockExists(stockId);
            if (!stockExists) return BadRequest("Stock does not exist");
            var comment = commentDto.ToCommentFromCreate(stockId);
            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null) return NotFound();
            return NoContent();
        }

    }
}