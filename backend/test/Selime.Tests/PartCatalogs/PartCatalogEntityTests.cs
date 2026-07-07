using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.PartCatalogs
{
    public class PartCatalogEntityTests
    {
        [Fact]
        public void PartCatalog_ShouldBeCreatable()
        {
            // Act
            var entity = new PartCatalog();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void PartCatalog_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new PartCatalog();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void PartCatalog_PartNumber_ShouldAcceptValue()
        {
            var entity = new PartCatalog { PartNumber = "Test Value" };
            entity.PartNumber.Should().Be("Test Value");
        }

    }
}
