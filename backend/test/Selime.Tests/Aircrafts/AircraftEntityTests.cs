using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.Aircrafts
{
    public class AircraftEntityTests
    {
        [Fact]
        public void Aircraft_ShouldBeCreatable()
        {
            // Act
            var entity = new Aircraft();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Aircraft_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Aircraft();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void Aircraft_Registration_ShouldAcceptValue()
        {
            var entity = new Aircraft { Registration = "Test Value" };
            entity.Registration.Should().Be("Test Value");
        }

    }
}
