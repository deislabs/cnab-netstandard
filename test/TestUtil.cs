using System.Collections.Generic;
using System.Linq;

namespace Cnab.Tests
{
    public static class TestUtil
    {
        public static bool EqualsAll<T>(this IList<T> a, IList<T> b)
        {
            if (a == null || b == null)
                return (a == null && b == null);

            if (a.Count != b.Count)
                return false;

            return a.SequenceEqual(b);
        }
    }
}