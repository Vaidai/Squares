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

        public static PointDto PointAsDto(this Point point)
        {
            return new PointDto(point.Id, point.X, point.Y);
        }
    }
}
