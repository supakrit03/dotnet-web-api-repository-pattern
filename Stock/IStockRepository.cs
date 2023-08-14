
using RepositoryPattern.Stock.Models;

namespace RepositoryPattern.Stock
{
	public interface IStockRepository
	{
		Task<List<StockModel>> GetStocks();
		Task<StockModel> GetStockById(string id);
		Task CreateStock(StockModel stockModel);
		Task UpdateStock(string id, StockModel stockModel);
		Task DeleteStock(string id);
	}
}

