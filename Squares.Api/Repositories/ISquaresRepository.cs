using Squares.Api.Entities;

namespace Squares.Api.Repositories
{
    public interface ISquaresRepository
    {
        //* I as a user can retrieve the squares identified
        Task<IEnumerable<MySquare>> GetSquaresAsync();
    }
}

