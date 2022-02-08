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

        public PointsController(IPointsRepository repository)
        {
            this._repository = repository;
        }


        //  GET /points
        [HttpGet]
        public async Task<IEnumerable<PointDto>> GetPointsAsync()
        {
            var points = (await _repository.GetPointsAsync())
                .Select(point => point.PointAsDto());
            return points;
        }

        //  GET /points/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PointDto>> GetPointAsync(Guid id)
        {
            var point = await _repository.GetPointAsync(id);
            if (point is null)
            {
                return NotFound();
            }
            return point.PointAsDto();
        }

        //  DELETE /points/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePointAsync(Guid id)
        {
            var existingItem = _repository.GetPointAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            await _repository.DeletePointFromListAsync(id);
            return NoContent();
        }

        //  POST /points
        [HttpPost]
        public async Task<ActionResult<PointDto>> AddPointAsync(AddPointDto pointDto)
        {
            MyPoint point = new()
            {
                Id = Guid.NewGuid(),
                X = pointDto.X,
                Y = pointDto.Y
            };

            await _repository.AddPointToListAsync(point);
            return CreatedAtAction(nameof(GetPointAsync), new { id = point.Id }, point.PointAsDto());
        }

        //  POST /points/list
        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<PointDto>> InsertAListOfPointsAsync(List<PointDto> pointsDto)
        {
            foreach (var p in pointsDto)
            {
                MyPoint point = new()
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

