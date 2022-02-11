using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public class InMemPointsRepository : IPointsRepository
    {
        private readonly List<MyPoint> points = new()
        {
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 },
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 },
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 },
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 },
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 },
            new MyPoint { Id = Guid.NewGuid(), X = 1, Y = 1 }
        };

        public async Task<IEnumerable<MyPoint>> GetPointsAsync()
        {
            return await Task.FromResult(points);
        }

        public async Task ImportAListOfPointsAsync(List<MyPoint> points)
        {
            foreach (var point in points)
            {
                await AddPointToListAsync(point);
            }
            await Task.CompletedTask;
        }

        public async Task AddPointToListAsync(MyPoint point)
        {
            points.Add(point);
            await Task.CompletedTask;
        }

        public async Task DeletePointFromListAsync(Guid id)
        {
            var index = points.FindIndex(existingItem => existingItem.Id == id);
            points.RemoveAt(index);
            await Task.CompletedTask;
        }

        public async Task<MyPoint> GetPointAsync(Guid id)
        {
            var point = points.Where(point => point.Id == id).SingleOrDefault();
            return await Task.FromResult(point);
        }
    }
}
