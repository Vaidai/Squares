using MongoDB.Bson;
using MongoDB.Driver;
using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public class MongoDbPointsRepository : IPointsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "points";
        private readonly IMongoCollection<MyPoint> _pointsCollection;
        private readonly FilterDefinitionBuilder<MyPoint> filterBuilder = Builders<MyPoint>.Filter;

        public MongoDbPointsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _pointsCollection = database.GetCollection<MyPoint>(collectionName);
        }


        public async Task ImportAListOfPointsAsync(List<MyPoint> points)
        {
            await _pointsCollection.InsertManyAsync(points);
        }

        public async Task AddPointToListAsync(MyPoint point)
        {
            await _pointsCollection.InsertOneAsync(point);
        }

        public async Task DeletePointFromListAsync(Guid id)
        {
            var filter = filterBuilder.Eq(point => point.Id, id);
            await _pointsCollection.DeleteOneAsync(filter);
        }


        public async Task<MyPoint> GetPointAsync(Guid id)
        {
            var filter = filterBuilder.Eq(point => point.Id, id);
            return await _pointsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MyPoint>> GetPointsAsync()
        {
            return await _pointsCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
