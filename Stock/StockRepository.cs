
using MongoDB.Driver;

using RepositoryPattern.Stock.Models;

namespace RepositoryPattern.Stock
{
    public class StockRepository : IStockRepository
    {

        private readonly IMongoCollection<StockModel> _stockCollection;

        public StockRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = mongoClient.GetDatabase("stock");
            _stockCollection = mongoDatabase.GetCollection<StockModel>("Stocks");
        }

        public async Task<List<StockModel>> GetStocks()
        {
            return await _stockCollection.Find(_ => true).ToListAsync();
        }

        public async Task<StockModel> GetStockById(string id)
        {
            return await _stockCollection.Find(stock => stock.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateStock(StockModel stockModel)
        {
            await _stockCollection.InsertOneAsync(stockModel);
        }

        public async Task UpdateStock(string id, StockModel stockModel)
        {

            var update = Builders<StockModel>.Update
                .Set(f => f.Name, stockModel.Name)
                .Set(f => f.Price, stockModel.Price)
                .Set(f => f.Index, stockModel.Index);

           await _stockCollection.UpdateOneAsync(stock => stock.Id == id, update);
        }

        public async Task DeleteStock(string id)
        {
            await _stockCollection.DeleteOneAsync(stock => stock.Id == id);
        }
    }
}

