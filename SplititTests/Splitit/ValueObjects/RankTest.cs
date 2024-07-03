using System;
using Splitit.Splitit.ValueObjects;

namespace SplititTests.Splitit.ValueObjects
{
	public class RankTest
	{
        [Theory]
        [InlineData(1)]
        [InlineData(150)]
        [InlineData(300)]
        public void Constructor_ValidRank(int value)
        {
            var rank = new Rank(value);

            Assert.Equal(value, rank.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(301)]
        [InlineData(-1)]
        public void Constructor_InvalidRank_ThrowsArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rank(value));
        }
    }
}

