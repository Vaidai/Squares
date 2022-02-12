﻿using Squares.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Squares.Api.Dtos
{
    public record SquareDto(Point Point1, Point Point2, Point Point3, Point Point4);


    public record PointDto(Guid Id, int X, int Y);
    public record AddPointDto([Required] int X, [Required] int Y);

}
