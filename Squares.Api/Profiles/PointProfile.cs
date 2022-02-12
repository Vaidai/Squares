using AutoMapper;
using Squares.Api.Dtos;
using Squares.Api.Entities;

namespace Squares.Api.Profiles
{
    public class PointProfile : Profile
    {
        public PointProfile()
        {
            CreateMap<Point, PointDTO>();
            CreateMap<Point, AddPointDTO>();
            CreateMap<Point, UpdatePointDTO>();
        }
    }
}
