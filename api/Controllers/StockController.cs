using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(ApplicationDBContext context) : ControllerBase
    {

        private readonly ApplicationDBContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Stocks.Select(s => s.ToStockDto()).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            _context.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());


        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            var stockModel = _context.Stocks.Find(id);
            if (stockModel == null) return NotFound();

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }



    }
}