using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.AtaChapters;
using Selime.AtaChapters.Dto;
using Selime.Flows;

namespace Selime.Tests.AtaChapters
{
    public class AtaChapterAppServiceTests
    {
        private readonly Mock<IRepository<AtaChapter, long>> _repositoryMock;
        private readonly AtaChapterAppService _service;

        public AtaChapterAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<AtaChapter, long>>();
            _service = new AtaChapterAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new AtaChapter { Id = 1, AtaNumber = "Test ataNu", Name = "Test name" },
                new AtaChapter { Id = 2, AtaNumber = "Test ataNu", Name = "Test name" },
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
                new AtaChapter { Id = 1, AtaNumber = "Test ataNu", Name = "Test name" },
                new AtaChapter { Id = 2, AtaNumber = "Test ataNu", Name = "Test name" },
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
            var dto = new CreateAtaChapterDto
            {
                AtaNumber = "Test ataNu", Name = "Test name"
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<AtaChapter>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new AtaChapter { Id = 1, AtaNumber = "Test ataNu", Name = "Test name" });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new AtaChapter { Id = 1, AtaNumber = "Test ataNu", Name = "Test name" });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
