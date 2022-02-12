using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<Point> GetPointAsync(Guid id);


        //* I as a user can import a list of points
        Task ImportAListOfPointsAsync(List<Point> points);

        //* I as a user can add a point to an existing list
        Task AddPointToListAsync(Point point);

        //* I as a user can delete a point from an existing list
        Task DeletePointFromListAsync(Guid id);
    }
}