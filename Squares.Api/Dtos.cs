using Squares.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Squares.Api.Dtos
{
    public record SquareDto(MyPoint Point1, MyPoint Point2, MyPoint Point3, MyPoint Point4);


    public record PointDto(Guid Id, int X, int Y);
    public record AddPointDto([Required] int X, [Required] int Y);

}
