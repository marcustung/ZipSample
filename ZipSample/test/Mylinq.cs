using System;
using System.Collections;
using System.Collections.Generic;
using ExpectedObjects;

namespace ZipSample.test
{
    public static class Mylinq
    {
        public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            var enumerator1 = second.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                yield return enumerator1.Current;
            }
        }

        public static IEnumerable<T> MyReverse<T>(this IEnumerable<T> source)
        {
            return new Stack<T>(source);
        }

        public static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is TResult result)
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TResult> MyCast<TResult>(this IEnumerable arrayList)
        {
            var enumerator = arrayList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is TResult cast)
                {
                    yield return cast;
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
        }

        public static IEnumerable<TResult> MyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TSecond, TFirst, TResult> myZip)
        {
            var firstEnumerator = second.GetEnumerator();
            var secondEnumerator = first.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return myZip(firstEnumerator.Current, secondEnumerator.Current);
            }
        }


        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return MyUnion(first, second, EqualityComparer<T>.Default);
        }
        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
           
            var hashSet = new HashSet<T>(comparer);
            var enumerator = first.GetEnumerator();
            var enumerator1 = second.GetEnumerator();

            while (enumerator.MoveNext())
            {
                hashSet.Add(enumerator.Current);
            }

            while (enumerator1.MoveNext())
            {
                hashSet.Add(enumerator1.Current);
            }

            return hashSet;
        }
    }
}