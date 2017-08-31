using System;
using System.IO;
using Xunit;

namespace BDSA2017.Lecture01.Tests
{
    public class ProgramTests
    {
        [Fact(DisplayName = "Main when run with null prints Hello, World!")]
        public void Main_when_run_with_null_prints_Hello_World()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader(Environment.NewLine);
            Console.SetIn(reader);

            // Act
            Program.Main(null);
            var output = writer.GetStringBuilder().ToString();
            var outputReader = new StringReader(output);

            // Assert
            Assert.Equal("Hello, World!", outputReader.ReadLine());
        }

        [Fact(DisplayName = "Main when run with no arguments prints Hello, World!")]
        public void Main_when_run_with_no_arguments_prints_Hello_World()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader(Environment.NewLine);
            Console.SetIn(reader);

            // Act
            Program.Main(null);
            var output = writer.GetStringBuilder().ToString();
            var outputReader = new StringReader(output);

            // Assert
            Assert.Equal("Hello, World!", outputReader.ReadLine());
        }

        [Fact(DisplayName = "Main given Brain prints Hello, Brain!")]
        public void Main_given_Brain_prints_Hello_Brain()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader(Environment.NewLine);
            Console.SetIn(reader);

            // Act
            string[] args = { "Brain" };

            Program.Main(args);
            var output = writer.GetStringBuilder().ToString();
            var outputReader = new StringReader(output);

            // Assert
            Assert.Equal("Hello, Brain!", outputReader.ReadLine());
        }

        [Fact(DisplayName = "Main given 42 prints You gave me the right number: 42")]
        public void Main_given_42_prints_You_gave_me_the_right_number_42()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader("42" + Environment.NewLine);
            Console.SetIn(reader);

            // Act
            Program.Main(new string[0]);
            var output = writer.GetStringBuilder().ToString();

            var expected = @"Hello, World!
Give me a number: 
You gave me the right number: 42!
";

            // Assert
            Assert.Equal(expected, output);
        }

        [Fact(DisplayName = "Main given foo prints That is not a number")]
        public void Main_given_foo_prints_That_is_not_a_number()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader("foo" + Environment.NewLine);
            Console.SetIn(reader);

            // Act
            Program.Main(new string[0]);
            var output = writer.GetStringBuilder().ToString();

            var expected = @"Hello, World!
Give me a number: 
That is not a number!
";

            // Assert
            Assert.Equal(expected, output);
        }

        [Fact(DisplayName = "Main given 43 prints You gave me the wrong number")]
        public void Main_given_43_prints_You_gave_me_the_wrong_number()
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);
            var reader = new StringReader("43" + Environment.NewLine);
            Console.SetIn(reader);

            // Act
            Program.Main(new string[0]);
            var output = writer.GetStringBuilder().ToString();

            var expected = @"Hello, World!
Give me a number: 
You gave me the wrong number: 43!
";

            // Assert
            Assert.Equal(expected, output);
        }
    }
}
