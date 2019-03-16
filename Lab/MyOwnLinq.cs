using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lab.Entities;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Predicate<TSource> pridicate)
        //public static List<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource, bool> pridicate)
        {
            //List<TSource> result = new List<TSource>();

            foreach (var o in sources)
            {
                if (pridicate(o))
                {
                    yield return o;
                    //result.Add(o);
                }
            }

            //return result;
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, int, bool> pridicate)
        {
            //List<TSource> result = new List<TSource>();

            var index = 0;

            foreach (var o in sources)
            {
                if (pridicate(o, index))
                {
                    yield return o;
                    //result.Add(o);
                }

                index++;
            }

            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> mapper)
        {
            //var result = new List<TResult>();
            int index = 0;
            foreach (var url in urls)
            {
                yield return mapper(url, index++);
                //result.Add(mapper(url, index++));
            }

            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> mapper)
        {
            //var result = new List<TResult>();

            foreach (var url in urls)
            {
                yield return mapper(url);
                //result.Add(mapper(url));
            }

            //return result;
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int takeCount)
        {
            var employes = employees.GetEnumerator();

            int index = 0;
            while (employes.MoveNext())
            {
                if (index++ < takeCount)
                {
                    yield return employes.Current;
                }
                else
                {
                    yield break;
                }
            }

            employes.Dispose();
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> employees, int skipCount)
        {
            var employes = employees.GetEnumerator();

            int index = 0;
            while (employes.MoveNext())
            {
                if (index++ >= skipCount)
                {
                    yield return employes.Current;
                }
            }

            employes.Dispose();
        }

        public static bool JoeyAll<TSource>(this IEnumerable<TSource> Sources, Func<TSource, bool> Pridicate)
        {
            bool res = true;
            foreach (var source in Sources)
            {
                if (Pridicate(source))
                {
                    res = false;
                    break;
                }
            }

            return res;
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> Sources)
        {
            var employes = Sources.GetEnumerator();

            return employes.MoveNext() ? employes.Current : default(TSource);

            //foreach (var source in Sources)
            //{
            //    return source;
            //}

            //return default(TSource);
        }
    }
}