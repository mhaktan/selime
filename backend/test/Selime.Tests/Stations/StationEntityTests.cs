using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.Stations
{
    public class StationEntityTests
    {
        [Fact]
        public void Station_ShouldBeCreatable()
        {
            // Act
            var entity = new Station();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Station_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Station();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void Station_Code_ShouldAcceptValue()
        {
            var entity = new Station { Code = "Test Value" };
            entity.Code.Should().Be("Test Value");
        }

        [Fact]
        public void Station_Name_ShouldAcceptValue()
        {
            var entity = new Station { Name = "Test Value" };
            entity.Name.Should().Be("Test Value");
        }

        [Fact]
        public void Station_City_ShouldAcceptValue()
        {
            var entity = new Station { City = "Test Value" };
            entity.City.Should().Be("Test Value");
        }

    }
}
