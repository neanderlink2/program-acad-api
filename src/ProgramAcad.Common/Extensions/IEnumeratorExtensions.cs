using System;
using System.Collections.Generic;

namespace ProgramAcad.Common.Extensions
{
    public static class IEnumeratorExtensions
    {
        public static TResult Match<T, TResult>(this IEnumerator<T> enumerator,
            Func<IEnumerator<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
        {
            var count = 0;
            while (enumerator.MoveNext())
            {
                count++;
            }
            enumerator.Reset();
            return count > 0 ? methodWhenSome(enumerator) : methodWhenNone();
        }
    }
}
