using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class ZipTests
    {
        [TestMethod]
        public void pair_3_girls_and_5_boys()
        {
            var girls = Repository.Get3Girls();
            var keys = Repository.Get5Keys();

            var girlAndBoyPairs = girls.MyZip(keys, (enumeratorCurrent, enumerator1Current) => new Tuple<string, string>(enumerator1Current.Name, enumeratorCurrent.OwnerBoy.Name)).ToList();
            var expected = new List<Tuple<string, string>>
            {
                Tuple.Create("Jean", "Joey"),
                Tuple.Create("Mary", "Frank"),
                Tuple.Create("Karen", "Bob"),
            };

            expected.ToExpectedObject().ShouldEqual(girlAndBoyPairs);
        }

        [TestMethod]
        public void pair_3_keys_and_5_girls()
        {
            var girls = Repository.Get5Girls();
            var keys = Repository.Get3Keys();

            var girlAndBoyPairs = keys.MyZip(girls, (enumeratorCurrent, enumerator1Current) => new Tuple<string, string>(enumeratorCurrent.Name, enumerator1Current.OwnerBoy.Name)).ToList();
            var expected = new List<Tuple<string, string>>
            {
                Tuple.Create("Jean", "Joey"),
                Tuple.Create("Mary", "Frank"),
                Tuple.Create("Karen", "Bob"),
            };

            expected.ToExpectedObject().ShouldEqual(girlAndBoyPairs);
        }

        //private IEnumerable<Tuple<string, string>> MyZip(IEnumerable<Girl> girls, IEnumerable<Key> keys)
        //{
        //    var enumerator = girls.GetEnumerator();
        //    var enumerator1 = keys.GetEnumerator();

        //    while (enumerator.MoveNext() && enumerator1.MoveNext())
        //    {
        //        yield return new Tuple<string, string>(enumerator.Current.Name, enumerator1.Current.OwnerBoy.Name);
        //    }
        //    //int nowkey = 0;
        //    //foreach (var girl in girls)
        //    //{
        //    //    int i = 0;
        //    //    foreach (var key in keys)
        //    //    {
        //    //        if (i == nowkey)
        //    //        {
        //    //            yield return new Tuple<string, string>(girl.Name, key.OwnerBoy.Name);
        //    //            break;
        //    //        }

        //    //        i++;
        //    //    }

        //    //    nowkey++;
        //    //}
        //}
    }
}