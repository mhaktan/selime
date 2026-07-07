using System;
using Xunit;
using FluentAssertions;
using Selime.Entities;

namespace Selime.Tests.AtaChapters
{
    public class AtaChapterEntityTests
    {
        [Fact]
        public void AtaChapter_ShouldBeCreatable()
        {
            // Act
            var entity = new AtaChapter();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void AtaChapter_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new AtaChapter();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void AtaChapter_AtaNumber_ShouldAcceptValue()
        {
            var entity = new AtaChapter { AtaNumber = "Test Value" };
            entity.AtaNumber.Should().Be("Test Value");
        }

        [Fact]
        public void AtaChapter_Name_ShouldAcceptValue()
        {
            var entity = new AtaChapter { Name = "Test Value" };
            entity.Name.Should().Be("Test Value");
        }

    }
}
