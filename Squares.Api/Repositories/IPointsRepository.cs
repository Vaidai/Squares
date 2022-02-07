using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public interface IPointsRepository
    {
        //* I as a user can import a list of points
        Task<IEnumerable<MyPoint>> AddListOfPointsAsync(List<MyPoint> points);

        //* I as a user can add a point to an existing list
        Task<MyPoint> AddPointToListAsync(MyPoint point);

        //* I as a user can delete a point from an existing list
        Task DeletePointFromListAsync(MyPoint point);
    }
}