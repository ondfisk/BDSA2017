using System.Collections.Generic;
using Xunit;

namespace BDSA2017.Lecture02.Tests
{
    public class CollectionUtilitiesTests
    {
        [Fact]
        public void GetEven_given_1_2_3_4_5_returns_2_4()
        {
            int[] list = { 1, 2, 3, 4, 5 };

            var evens = CollectionUtilities.GetEven(list);

            Assert.Equal(new[] { 2, 4 }, evens);
        }

        [Fact]
        public void Find_given_1_2_3_and_2_returns_true()
        {
            var found = CollectionUtilities.Find(2, 1, 2, 3);

            Assert.True(found);
        }

        [Fact]
        public void Find_given_1_2_3_and_4_returns_false()
        {
            var found = CollectionUtilities.Find(4, 1, 2, 3);

            Assert.False(found);
        }

        [Fact]
        public void Unique_given_1_2_3_3_3_3_4_returns_1_2_3_4()
        {
            var unique = CollectionUtilities.Unique(new[] { 1, 2, 3, 3, 3, 3, 4 });

            var expected = new HashSet<int> { 2, 3, 4, 1 };

            Assert.True(expected.SetEquals(unique));
        }

        [Fact]
        public void Reverse_given_1_2_3_returns_3_2_1()
        {
            int[] list = { 1, 2, 3 };

            var reverse = CollectionUtilities.Reverse(list);

            Assert.Equal(new[] { 3, 2, 1 }, reverse);
        }

        [Fact]
        public void Sort_given_ducks_sorts_the_ducks()
        {
            var ducks = new List<Duck> {
                 new Duck { Id = 1, Name = "Donald Duck", Age = 32 },
               new Duck { Id = 2, Name = "Daisy Duck", Age = 30 }
            };

            CollectionUtilities.Sort(ducks, new DuckAgeComparer());

            Assert.Equal(30, ducks[0].Age);
            Assert.Equal(32, ducks[1].Age);
        }
    }
}
