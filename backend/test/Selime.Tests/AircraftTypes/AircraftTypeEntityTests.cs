using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.AircraftTypes
{
    public class AircraftTypeEntityTests
    {
        [Fact]
        public void AircraftType_ShouldBeCreatable()
        {
            // Act
            var entity = new AircraftType();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void AircraftType_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new AircraftType();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void AircraftType_TypeCode_ShouldAcceptValue()
        {
            var entity = new AircraftType { TypeCode = "Test Value" };
            entity.TypeCode.Should().Be("Test Value");
        }

        [Fact]
        public void AircraftType_Manufacturer_ShouldAcceptValue()
        {
            var entity = new AircraftType { Manufacturer = "Test Value" };
            entity.Manufacturer.Should().Be("Test Value");
        }

    }
}
