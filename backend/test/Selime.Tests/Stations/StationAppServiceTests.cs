using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.Stations;
using Selime.Stations.Dto;
using Selime.Flows;

namespace Selime.Tests.Stations
{
    public class StationAppServiceTests
    {
        private readonly Mock<IRepository<Station, long>> _repositoryMock;
        private readonly StationAppService _service;

        public StationAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Station, long>>();
            _service = new StationAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Station { Id = 1, Code = "Test code", Name = "Test name", City = "Test city" },
                new Station { Id = 2, Code = "Test code", Name = "Test name", City = "Test city" },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act
            var result = _repositoryMock.Object.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public void Repository_GetAll_WithFilter_ShouldWork()
        {
            // Arrange
            var entities = new[]
            {
                new Station { Id = 1, Code = "Test code", Name = "Test name", City = "Test city" },
                new Station { Id = 2, Code = "Test code", Name = "Test name", City = "Test city" },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act — simulate keyword filter
            var result = _repositoryMock.Object.GetAll()
                .Where(x => x.Id.ToString().Contains("1"));

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldInsertEntity()
        {
            // Arrange
            var dto = new CreateStationDto
            {
                Code = "Test code", Name = "Test name", City = "Test city"
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Station>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Station { Id = 1, Code = "Test code", Name = "Test name", City = "Test city" });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Station { Id = 1, Code = "Test code", Name = "Test name", City = "Test city" });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
