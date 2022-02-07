using Microsoft.AspNetCore.Mvc;
using Squares.Api.Dtos;
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

        //  DELETE /{id}
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

    }
}
