using Microsoft.AspNetCore.Mvc;
using Squares.Api.Dtos;
using Squares.Api.Repositories;

namespace Squares.Api.Controllers
{
    [ApiController]
    [Route("squares")]
    public class SquaresController : ControllerBase
    {
        private readonly ISquaresRepository _repository;
        public SquaresController(ISquaresRepository repository)
        {
            this._repository = repository;
        }


        //  GET /items
        [HttpGet]
        public async Task<IEnumerable<SquareDto>> GetSquaresAsync()
        {
            var squares = (await _repository.GetSquaresAsync())
                .Select(square => square.AsDto());
            return squares;
        }

    }
}
