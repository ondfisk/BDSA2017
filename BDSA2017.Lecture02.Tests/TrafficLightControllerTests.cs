using System;
using Xunit;

namespace BDSA2017.Lecture02.Tests
{
    public class TrafficLightControllerTests
    {
        [Fact]
        public void CanIGo_given_Green_returns_True()
        {
            var controller = new TrafficLightController();

            var go = controller.MayIGo(TrafficLightColor.Green);

            Assert.True(go);
        }

        [Fact]
        public void CanIGo_given_Yellow_returns_False()
        {
            var controller = new TrafficLightController();

            var go = controller.MayIGo(TrafficLightColor.Yellow);

            Assert.False(go);
        }

        [Fact]
        public void CanIGo_given_Red_returns_False()
        {
            var controller = new TrafficLightController();

            var go = controller.MayIGo(TrafficLightColor.Red);

            Assert.False(go);
        }

        [Fact]
        public void CanIGo_given_InvalidColor_throws_ArgumentException()
        {
            var controller = new TrafficLightController();

            Assert.Throws<ArgumentException>(() => controller.MayIGo((TrafficLightColor) 42));
        }

        [Theory]
        [InlineData(TrafficLightColor.Green, true)]
        [InlineData(TrafficLightColor.Yellow, false)]
        [InlineData(TrafficLightColor.Red, false)]
        public void CanIGo_given_color_returns_expected(TrafficLightColor color, bool expected)
        {
            var controller = new TrafficLightController();

            var go = controller.MayIGo(color);

            Assert.Equal(expected, go);
        }
    }
}
