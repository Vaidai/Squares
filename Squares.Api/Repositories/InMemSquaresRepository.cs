using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public class InMemSquaresRepository : ISquaresRepository
    {
        private readonly List<MySquare> squares = new()
        {
            new MySquare { Point1 = new Point { X = -1, Y = 1 }, Point2 = new Point { X = 1, Y = 1 }, Point3 = new Point { X = 1, Y = -1 }, Point4 = new Point { X = -1, Y = -1 } },
            new MySquare { Point1 = new Point { X = -3, Y = 2 }, Point2 = new Point { X = 2, Y = 2 }, Point3 = new Point { X = 2, Y = -2 }, Point4 = new Point { X = -2, Y = -2 } },
            new MySquare { Point1 = new Point { X = -3, Y = 3 }, Point2 = new Point { X = 3, Y = 3 }, Point3 = new Point { X = 3, Y = -3 }, Point4 = new Point { X = -3, Y = -3 } }
        };

        public async Task<IEnumerable<MySquare>> GetSquaresAsync()
        {
            return await Task.FromResult(squares);
        }
    }
}
