using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class OfTypeTests
    {
        [TestMethod]
        public void pick_integer_from_ArrayList()
        {
            var arrayList = new ArrayList { 2, "q", 6 };
            var actual = arrayList.MyOfType<int>().ToList();

            var expected = new List<int> { 2, 6 };
            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}