using Squares.Api.Dtos;
using Squares.Api.Entities;

namespace Squares.Api
{
    public static class Extensions
    {
        public static SquareDto AsDto(this MySquare square)
        {
            return new SquareDto(square.Point1, square.Point2, square.Point3, square.Point4);
        }
    }
}
