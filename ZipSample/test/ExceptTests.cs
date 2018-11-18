using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    public interface IMapper<TSource, TResult>
    {
        TResult Project(TSource source);
    }

    [TestClass]
    public class ExceptTests
    {
        [TestMethod]
        public void Except_integers()
        {
            var first = new List<int> { 1, 3, 5, 4 };
            var second = new List<int> { 5, 3, 7, 9 };

            var expected = new List<int> { 1, 4 };

            var actual = MyExcept(first, second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void Except_integers_2()
        {
            var first = new List<int> { 1, 4, 3, 5, 4 };
            var second = new List<int> { 5, 3, 7, 9 };

            var expected = new List<int> { 1, 4 };

            var actual = MyExcept(first, second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void TransOrderToOrderDTO()
        {
            var titan = new Titan();
            var order = new Order()
            {
                OrderId = 1,
                Amount = 100,
                CreateDate = new DateTime(2018, 1, 3)
            };
            var expect = new OrderDTO()
            {
                Id = 1,
                Date = "01/03/2018",
                TotalAmount = 100m
            };

            var actual = titan.Project(order, order1 => new OrderDTO
            {
                Date = order.CreateDate.ToString("MM/dd/yyyy"),
                Id = order.OrderId,
                TotalAmount = Convert.ToDecimal(order.Amount)
            });

            Assert.AreEqual(expect.Id, actual.Id);
            Assert.AreEqual(expect.Date, actual.Date);
            Assert.AreEqual(expect.TotalAmount, actual.TotalAmount);
        }

        [TestMethod]
        public void TransOrderToOrderDTO1()
        {
            var titan = new Titan();
            var order = new Order()
            {
                OrderId = 1,
                Amount = 100,
                CreateDate = new DateTime(2018, 1, 3)
            };
            var expect = new OrderDTO()
            {
                Id = 1,
                Date = "01/03/2018",
                TotalAmount = 100m
            };

            var actual = titan.Project(order, new OrderMapper());

            Assert.AreEqual(expect.Id, actual.Id);
            Assert.AreEqual(expect.Date, actual.Date);
            Assert.AreEqual(expect.TotalAmount, actual.TotalAmount);
        }

        [TestMethod]
        public void TransOrderToOrderDTO2()
        {
            var titan = new Titan();
            var order = new Order()
            {
                OrderId = 1,
                Amount = 100,
                CreateDate = new DateTime(2018, 1, 3),
                Key = 5,
                Name = "Joey"
            };
            var expect = new OrderDTO()
            {
                Key = 5,
                Name = "Joey"

            };

            var actual = titan.Project(order);

            Assert.AreEqual(expect.Name, actual.Name);
            //Assert.AreEqual("", actual.Date);
            Assert.AreEqual(expect.Key, actual.Key);

        }

        private IEnumerable<int> MyExcept(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSet = new HashSet<int>(second);
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (hashSet.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }
    }

    public class OrderMapper : IMapper<Order, OrderDTO>
    {
        public OrderDTO Project(Order source)
        {
            return new OrderDTO
            {
                Date = source.CreateDate.ToString("MM/dd/yyyy"),
                Id = source.OrderId,
                TotalAmount = Convert.ToDecimal(source.Amount)
            };
        }
    }

    public class Titan
    {
        public TResult Project<TSource, TResult>(TSource source, Func<TSource, TResult> orderMapping)
        {
            return orderMapping(source);
        }

        public TResult Project<TSource, TResult>(TSource source, IMapper<TSource, TResult> mapper)
        {
            return mapper.Project(source);
        }

        public OrderDTO Project(Order order)
        {
            var dtoProps = typeof(OrderDTO).GetProperties();

            var orderDto = new OrderDTO();

            foreach (var propertyInfo in dtoProps)
            {
                if (order.GetType().GetProperties().Select(x=>x.Name).Contains(propertyInfo.Name))
                {
                    propertyInfo.SetValue(orderDto, typeof(Order).GetProperty(propertyInfo.Name)?.GetValue(order), null);
                }
            }

            return orderDto;
        }
    }
}