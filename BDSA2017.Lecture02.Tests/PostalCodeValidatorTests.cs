using System;
using Xunit;

namespace BDSA2017.Lecture02.Tests
{
    public class PostalCodeValidatorTests
    {
        [Fact]
        public void IsValid_given_2000_returns_true()
        {
            var valid = PostalCodeValidator.IsValid("2000");

            Assert.True(valid);
        }

        [Fact]
        public void IsValid_given_2000x_returns_false()
        {
            var valid = PostalCodeValidator.IsValid("2000x");

            Assert.False(valid);
        }

        [Fact]
        public void TryParse_given_2000_Frederiksberg_returns_true()
        {
            var valid = PostalCodeValidator.TryParse("2000 Frederiksberg", out var postalCode, out var locality);

            Assert.True(valid);
            Assert.Equal("2000", postalCode);
            Assert.Equal("Frederiksberg", locality); 
        }

        [Fact]
        public void IsValid_given_100_Tórshavn_returns_true()
        {
            var valid = PostalCodeValidator.IsValid("100");

            Assert.True(valid);
        }

        [Fact]
        public void TryParse_given_2720_Vanløse_returns_true()
        {
            var valid = PostalCodeValidator.TryParse("2720 Vanløse", out var postalCode, out var locality);

            Assert.True(valid);
            Assert.Equal("2720", postalCode);
            Assert.Equal("Vanløse", locality);
        }
    }
}
