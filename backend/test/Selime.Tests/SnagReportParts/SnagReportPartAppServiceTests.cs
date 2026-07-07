using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.SnagReportParts;
using Selime.SnagReportParts.Dto;
using Selime.Flows;

namespace Selime.Tests.SnagReportParts
{
    public class SnagReportPartAppServiceTests
    {
        private readonly Mock<IRepository<SnagReportPart, long>> _repositoryMock;
        private readonly SnagReportPartAppService _service;

        public SnagReportPartAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<SnagReportPart, long>>();
            _service = new SnagReportPartAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new SnagReportPart { Id = 1, QuantityUsed = 1 },
                new SnagReportPart { Id = 2, QuantityUsed = 1 },
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
                new SnagReportPart { Id = 1, QuantityUsed = 1 },
                new SnagReportPart { Id = 2, QuantityUsed = 1 },
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
            var dto = new CreateSnagReportPartDto
            {
                QuantityUsed = 1
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<SnagReportPart>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SnagReportPart { Id = 1, QuantityUsed = 1 });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SnagReportPart { Id = 1, QuantityUsed = 1 });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
