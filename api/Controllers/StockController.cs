using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(ApplicationDBContext context) : ControllerBase
    {

        private readonly ApplicationDBContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Stocks.Select(s => s.ToStockDto()).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null) return NotFound();

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null) return NotFound();

            _context.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}