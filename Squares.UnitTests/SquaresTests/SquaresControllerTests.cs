using FluentAssertions;
using Moq;
using Squares.Api.Controllers;
using Squares.Api.Dtos;
using Squares.Api.Entities;
using Squares.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Squares.UnitTests.SquaresTests
{
    public class SquaresControllerTests
    {
        [Fact]
        public void UnitOfWork_StateUnderTest_ExpectedBehavior()
        {
            // Arange
            int expected = 5;

            // Act
            int actual = 5; //Class.methot();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetSquaresAsync_WithExistingSquares_ReturnAllItems()
        {
            var repositoryStub = new Mock<ISquaresRepository>();
            // Arange
            MySquare[] expectedSquares = new[]
            {
                new MySquare { Point1 = new MyPoint { X = -1, Y = 1 }, Point2 = new MyPoint { X = 1, Y = 1 }, Point3 = new MyPoint { X = 1, Y = -1 }, Point4 = new MyPoint { X = -1, Y = -1 } },
                new MySquare { Point1 = new MyPoint { X = -3, Y = 2 }, Point2 = new MyPoint { X = 2, Y = 2 }, Point3 = new MyPoint { X = 2, Y = -2 }, Point4 = new MyPoint { X = -2, Y = -2 } },
                new MySquare { Point1 = new MyPoint { X = -3, Y = 3 }, Point2 = new MyPoint { X = 3, Y = 3 }, Point3 = new MyPoint { X = 3, Y = -3 }, Point4 = new MyPoint { X = -3, Y = -3 } }
            };

            repositoryStub.Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(expectedSquares);

            var controller = new SquaresController(repositoryStub.Object);

            // Act
            var actualSquares = await controller.GetSquaresAsync();

            // Assert
            actualSquares.Should().BeEquivalentTo(
                expectedSquares,
                options => options.ComparingByMembers<SquareDto>());
        }
    }
}
