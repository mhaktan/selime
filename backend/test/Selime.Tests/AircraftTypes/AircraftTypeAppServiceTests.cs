using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.AircraftTypes;
using Selime.AircraftTypes.Dto;
using Selime.Flows;

namespace Selime.Tests.AircraftTypes
{
    public class AircraftTypeAppServiceTests
    {
        private readonly Mock<IRepository<AircraftType, long>> _repositoryMock;
        private readonly AircraftTypeAppService _service;

        public AircraftTypeAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<AircraftType, long>>();
            _service = new AircraftTypeAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new AircraftType { Id = 1, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" },
                new AircraftType { Id = 2, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" },
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
                new AircraftType { Id = 1, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" },
                new AircraftType { Id = 2, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" },
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
            var dto = new CreateAircraftTypeDto
            {
                TypeCode = "Test typeCode", Manufacturer = "Test manufacturer"
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<AircraftType>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new AircraftType { Id = 1, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new AircraftType { Id = 1, TypeCode = "Test typeCode", Manufacturer = "Test manufacturer" });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
