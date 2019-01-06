using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DZRichPresenceClient.Misc
{
    public static class ArrayExtensions
    {
        private static Random Rand = new Random();

        public static T RandomElement<T>(this T[] items)
        {
            return items[Rand.Next(0, items.Length)];
        }

        public static T RandomElement<T>(this List<T> items)
        {
            return items[Rand.Next(0, items.Count)];
        }

        public static string RandomElement(this StringCollection items)
        {
            return items[Rand.Next(0, items.Count)];
        }
    }
}
