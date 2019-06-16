using Alura.LeilaoOnline.Core;
using System.Collections;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.Tests
{
    class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[] {5, 1, 3, 9},
            new object[] {7, 1, 5, 3}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<object[]> GetPersonFromDataGenerator()
        {
            yield return new object[]
            {
                new Person {Name = "Tribbiani", Age = 56},
                new Person {Name = "Gotti", Age = 16},
                new Person {Name = "Sopranos", Age = 15},
                new Person {Name = "Corleone", Age = 27}
            };

            yield return new object[]
            {
                new Person {Name = "Mancini", Age = 79},
                new Person {Name = "Vivaldi", Age = 16},
                new Person {Name = "Serpico", Age = 19},
                new Person {Name = "Salieri", Age = 20}
            };
        }
    }
}
