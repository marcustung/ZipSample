using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class ConcatTests
    {
        [TestMethod]
        public void concat_empolyee()
        {
            var firstEmployee = new List<Employee>
            {
                new Employee() {Id = 91, Name = "Joey"},
            };
            var secondEmployee = new List<Employee>

            {
                new Employee(){ Id = 1,Name = "David"},
                new Employee(){ Id = 2,Name = "Tom"}
            }; ;

            var actual = firstEmployee.MyConcat(secondEmployee).ToList();

            var expected = new List<Employee>()
            {
                new Employee() {Id = 91, Name = "Joey"},
                new Employee(){ Id = 1,Name = "David"},
                new Employee(){ Id = 2,Name = "Tom"}
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void concat_integers()
        {
            var first = new int[] { 1, 3, 5 };
            var second = new int[] { 2, 4, 6 };

            var actual = first.MyConcat(second).ToArray();

            var expected = new int[] { 1, 3, 5, 2, 4, 6 };
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}