using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squares.Api.Dtos;
using Squares.Api.Entities;
using Squares.Api.Repositories;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("points")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsRepository _repository;
        private IMapper _mapper;


        public PointsController(IPointsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //  GET /points
        [HttpGet]
        public async Task<IEnumerable<PointDTO>> GetPointsAsync()
        {
            var points = await _repository.GetPointsAsync();
            //var result = _mapper.Map<List<Point>, List<PointDTO>>((List<Point>)points); //todo delete
            //IEnumerable<PointDTO> ienumerableDest = _mapper.Map<List<Point>, IEnumerable<PointDTO>>((List<Point>)points);//todo delete
            var result= _mapper.Map<List<Point>, IEnumerable<PointDTO>>((List<Point>)points);
            return result;
        }

        //  GET /points/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PointDTO>> GetPointAsync(Guid id)
        {
            var point = await _repository.GetPointAsync(id);
            if (point is null)
            {
                return NotFound();
            }
            var result = _mapper.Map<Point, PointDTO>(point);
            return result;
        }

        //  DELETE /points/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePointAsync(Guid id)
        {
            var existingItem = await _repository.GetPointAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            await _repository.DeletePointFromListAsync(id);
            return NoContent();
        }

        //  POST /points
        [HttpPost]
        public async Task<ActionResult<PointDTO>> AddPointAsync(AddPointDTO newPoint)
        {
            Point pointToAdd = new()
            {
                Id = Guid.NewGuid(),
                X = newPoint.X,
                Y = newPoint.Y
            };

            await _repository.AddPointToListAsync(pointToAdd);
            var result =  CreatedAtAction(nameof(GetPointAsync), new { id = pointToAdd.Id }, _mapper.Map<Point, AddPointDTO>(pointToAdd));
            return result;
        }

        //  POST /points/list
        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<PointDTO>> InsertAListOfPointsAsync(List<PointDTO> pointsDto)
        {
            foreach (var p in pointsDto)
            {
                Point point = new()
                {
                    Id = Guid.NewGuid(),
                    X = p.X,
                    Y = p.Y
                };
                await _repository.AddPointToListAsync(point);
            }
            return Ok();
        }

    }

}

