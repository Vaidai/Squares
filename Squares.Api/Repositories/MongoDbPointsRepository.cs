using MongoDB.Bson;
using MongoDB.Driver;
using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public class MongoDbPointsRepository : IPointsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "points";
        private readonly IMongoCollection<Point> _pointsCollection;
        private readonly FilterDefinitionBuilder<Point> filterBuilder = Builders<Point>.Filter;

        public MongoDbPointsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _pointsCollection = database.GetCollection<Point>(collectionName);
        }

        public async Task<IEnumerable<Point>> GetPointsAsync()
        {
            return await _pointsCollection.Find(new BsonDocument()).ToListAsync();
        }


        public async Task ImportAListOfPointsAsync(List<Point> points)
        {
            await _pointsCollection.InsertManyAsync(points);
        }

        public async Task AddPointToListAsync(Point point)
        {
            await _pointsCollection.InsertOneAsync(point);
        }

        public async Task DeletePointFromListAsync(Guid id)
        {
            var filter = filterBuilder.Eq(point => point.Id, id);
            await _pointsCollection.DeleteOneAsync(filter);
        }


        public async Task<Point> GetPointAsync(Guid id)
        {
            var filter = filterBuilder.Eq(point => point.Id, id);
            return await _pointsCollection.Find(filter).SingleOrDefaultAsync();
        }


    }
}
