using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Squares.Api.Controllers;
using Squares.Api.Entities;
using Squares.Api.Repositories;
using System;
using Xunit;

namespace Squares.UnitTests.PointsTests
{
    public class PointsControllerTests
    {
        private readonly Mock<IPointsRepository> _repositoryStub = new();

        [Fact]
        public async void DeletePintAsync_WithExistingPoint_ReturnNoContent()
        {
            // Arange
            MyPoint existingPoint = new MyPoint() { Id = Guid.NewGuid(), X = 1, Y = 2 };
            _repositoryStub.Setup(repo => repo.GetPointAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPoint);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.DeletePointAsync(existingPoint.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void DeletePintAsync_WithNotExistingPoint_ReturnNotFound()
        {
            // Arange
            _repositoryStub.Setup(repo => repo.GetPointAsync(It.IsAny<Guid>()))
                .ReturnsAsync((MyPoint)null);

            var controller = new PointsController(_repositoryStub.Object);

            // Act
            var result = await controller.DeletePointAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.ExecuteResultAsync);
        }
    }
}
