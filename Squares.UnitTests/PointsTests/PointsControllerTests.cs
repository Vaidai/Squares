using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Squares.Api.Controllers;
using Squares.Api.Dtos;
using Squares.Api.Entities;
using Squares.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Squares.UnitTests.PointsTests
{
    public class PointsControllerTests
    {
        private readonly Mock<IPointsRepository> _repositoryStub = new();

        [Fact]
        public async Task DeletePointAsync_WithExistingPoint_ReturnNoContent()
        {
            // Arange
            MyPoint existingPoint = new() { Id = Guid.NewGuid(), X = 1, Y = 2 };

            _repositoryStub.Setup(repo => repo.GetPointAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPoint);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.DeletePointAsync(existingPoint.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeletePointAsync_WithUnexistingPoint_ReturnNotFound()
        {
            // Arange
            MyPoint unexistingPoint = new() { Id = Guid.NewGuid(), X = 1, Y = 2 };

            _repositoryStub.Setup(repo => repo.GetPointAsync(It.IsAny<Guid>()))
                .ReturnsAsync(unexistingPoint);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.DeletePointAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.ExecuteResult);
        }

        [Fact]
        public async Task AddPointAsync_WithPoint_ReturnCreatedPoint()
        {
            // Arrange
            var pointForInsering = new AddPointDto(2, 2);
            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.AddPointAsync(pointForInsering);

            // Assert
            var insertedPoint = (result.Result as CreatedAtActionResult).Value as PointDto;

            pointForInsering.Should().BeEquivalentTo(insertedPoint);
            insertedPoint.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetPointsAsync_WithEmptyList_ReturnEmptyList()
        {
            // Arrange
            MyPoint[] expected = new MyPoint[] { };

            _repositoryStub.Setup(repo => repo.GetPointsAsync()).ReturnsAsync(expected);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var actualPoints = await controller.GetPointsAsync();

            // Assert
            Assert.Empty(actualPoints);
        }

        [Fact]
        public async Task GetPointsAsync_WithExistingPoints_ReturnAllPoints()
        {
            // Arrange
            var expected = new[] {
                new MyPoint() { Id = Guid.NewGuid(), X = 1, Y = 2 },
                new MyPoint() { Id = Guid.NewGuid(), X = 13, Y = 32 } };

            _repositoryStub.Setup(repo => repo.GetPointsAsync()).ReturnsAsync(expected);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var actualPoints = await controller.GetPointsAsync();

            // Assert
            actualPoints.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPointAsync_WithExistingPoint_ReturnExpectedPoint()
        {
            // Arrange
            var expected = new MyPoint() { Id = Guid.NewGuid(), X = 1, Y = 2 };
            _repositoryStub.Setup(repo => repo.GetPointAsync(Guid.NewGuid())).ReturnsAsync(expected);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.GetPointAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPointAsync_WithUnExistingPoint_ReturnNotFound()
        {
            // Arrange
            var point = new MyPoint() { Id = Guid.NewGuid(), X = 1, Y = 2 };
            _repositoryStub.Setup(repo => repo.GetPointAsync(Guid.NewGuid())).ReturnsAsync(point);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.GetPointAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task InsertAListOfPointsAsync_WithExistingPoints_ReturnOk()
        {
            // Arrange
            List<PointDto> pointsToCreate = new()
            {
                new PointDto(Guid.NewGuid(), 1, 2),
                new PointDto(Guid.NewGuid(), 3, 5)
            };

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.InsertAListOfPointsAsync(pointsToCreate);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

    }
}
