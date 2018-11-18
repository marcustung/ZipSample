using System;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    public class GirlCopare : IEqualityComparer<Girl>
    {
        public bool Equals(Girl x, Girl y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Girl obj)
        {
            return Tuple.Create(obj.Name, obj.Age).GetHashCode();
        }
    }

    
    [TestClass]
    public class UnionTests
    {
        [TestMethod]
        public void Union_girls()
        {
            var first = new List<Girl>
            {
                new Girl()
                {
                    Name = "Amanda"
                },
                new Girl()
                {
                    Name = "Lucy"
                }
            };

            var second = new List<Girl>
            {
                new Girl()
                {
                    Name = "Lucy"
                },
                new Girl()
                {
                    Name = "Xinyi"
                }
            };

            var expected = new List<Girl>
            {
                new Girl()
                {
                    Name = "Amanda"
                },
                new Girl()
                {
                    Name = "Lucy"
                },
                new Girl()
                {
                    Name = "Xinyi"
                }
            };

            var actual = first.MyUnion(second, new GirlCopare()).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void Union_integers()
        {
            var first = new List<int> { 1, 3, 5 };
            var second = new List<int> { 5, 3, 7, 9 };

            var expected = new List<int> { 1, 3, 5, 7, 9 };

            var actual = first.MyUnion(second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}