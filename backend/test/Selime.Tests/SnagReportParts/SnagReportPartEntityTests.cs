using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.SnagReportParts
{
    public class SnagReportPartEntityTests
    {
        [Fact]
        public void SnagReportPart_ShouldBeCreatable()
        {
            // Act
            var entity = new SnagReportPart();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void SnagReportPart_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new SnagReportPart();

            // Assert
            entity.Id.Should().Be(default(long));

        }


    }
}
