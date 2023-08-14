
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Stock.Dtos;
using RepositoryPattern.Stock.Models;


namespace RepositoryPattern.Stock;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetStocks()
    {
        try
        {

            var stocks = await _stockRepository.GetStocks();

            return Ok(stocks);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStock(string id)
    {
        try
        {

            var stock = await _stockRepository.GetStockById(id);

            return Ok(stock);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockDto createStockDto)
    {
        try
        {

            var newStock = new StockModel
            {
                Name = createStockDto.Name,
                Price = createStockDto.Price,
                Index = createStockDto.Index
            };

            await _stockRepository.CreateStock(newStock);

            return CreatedAtAction(nameof(GetStock), new { id = newStock.Id }, newStock);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock([FromRoute] string id, [FromBody] UpdateStockDto updateStockDto)
    {
        try
        {
            var stock = await _stockRepository.GetStockById(id);

            if (stock is null) return NotFound();

            await _stockRepository.UpdateStock(id, new StockModel
            {
                Id = stock.Id,
                Name = updateStockDto.Name,
                Price = updateStockDto.Price,
                Index = updateStockDto.Index
            });

            return Ok("Stock updated");

        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteStock(string id)
    {
        var stock = await _stockRepository.GetStockById(id);

        if (stock is null)
        {
            return NotFound();
        }

        await _stockRepository.DeleteStock(id);

        return NoContent();
    }
}


