using Xunit;
using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class ParametrizedTests
    {
        private bool IsOddNumber(int number) => number % 2 != 0;

        private bool IsAboveFourteen(Person person) => person.Age > 14;

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void AllNumber_AreOdd_WithClassData(int a, int b, int c, int d)
        {
            Assert.True(IsOddNumber(a));
            Assert.True(IsOddNumber(b));
            Assert.True(IsOddNumber(c));
            Assert.True(IsOddNumber(d));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetPersonFromDataGenerator),
                    MemberType = typeof(TestDataGenerator))]
        public void AllPersons_AreAboveFourteen_FromDataGenerator(Person a, Person b, Person c, Person d)
        {
            Assert.True(IsAboveFourteen(a));
            Assert.True(IsAboveFourteen(b));
            Assert.True(IsAboveFourteen(c));
            Assert.True(IsAboveFourteen(d));
        }
    }
}
