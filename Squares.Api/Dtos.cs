using Squares.Api.Entities;

namespace Squares.Api.Dtos
{
    public record SquareDto(MyPoint point1, MyPoint point2, MyPoint point3, MyPoint point4);
    public record PointDto(Guid id, int x, int y);

}
