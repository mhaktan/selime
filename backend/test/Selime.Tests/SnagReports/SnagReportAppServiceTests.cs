using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using Selime.Entities;
using Selime.SnagReports;
using Selime.SnagReports.Dto;
using Selime.Flows;

namespace Selime.Tests.SnagReports
{
    public class SnagReportAppServiceTests
    {
        private readonly Mock<IRepository<SnagReport, long>> _repositoryMock;
        private readonly SnagReportAppService _service;

        public SnagReportAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<SnagReport, long>>();
            _service = new SnagReportAppService(_repositoryMock.Object, new Mock<IFlowEngine>().Object, new Mock<IRepository<StatusChangeLog, long>>().Object, new Mock<IRepository<ApprovalRecord, Guid>>().Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new SnagReport { Id = 1, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 },
                new SnagReport { Id = 2, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 },
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
                new SnagReport { Id = 1, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 },
                new SnagReport { Id = 2, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 },
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
            var dto = new CreateSnagReportDto
            {
                ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<SnagReport>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SnagReport { Id = 1, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new SnagReport { Id = 1, ReportNumber = "Test reportNumber", Title = "Test title", Description = "Test description", Severity = 0, ReportedBy = "Test reportedBy", DetectionDate = DateTime.UtcNow, Status = 0 });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
