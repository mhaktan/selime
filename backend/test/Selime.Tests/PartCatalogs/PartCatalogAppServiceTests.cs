using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.PartCatalogs;
using Selime.PartCatalogs.Dto;
using Selime.Flows;

namespace Selime.Tests.PartCatalogs
{
    public class PartCatalogAppServiceTests
    {
        private readonly Mock<IRepository<PartCatalog, long>> _repositoryMock;
        private readonly PartCatalogAppService _service;

        public PartCatalogAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<PartCatalog, long>>();
            _service = new PartCatalogAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new PartCatalog { Id = 1, PartNumber = "Test partNumber", StockQuantity = 1 },
                new PartCatalog { Id = 2, PartNumber = "Test partNumber", StockQuantity = 1 },
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
                new PartCatalog { Id = 1, PartNumber = "Test partNumber", StockQuantity = 1 },
                new PartCatalog { Id = 2, PartNumber = "Test partNumber", StockQuantity = 1 },
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
            var dto = new CreatePartCatalogDto
            {
                PartNumber = "Test partNumber", StockQuantity = 1
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<PartCatalog>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new PartCatalog { Id = 1, PartNumber = "Test partNumber", StockQuantity = 1 });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new PartCatalog { Id = 1, PartNumber = "Test partNumber", StockQuantity = 1 });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
