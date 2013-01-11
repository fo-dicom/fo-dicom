using System.Linq;

// ReSharper disable CheckNamespace
namespace System.Collections.Generic
// ReSharper restore CheckNamespace
{
    public class SortedList<TKey, TValue> : Dictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.OrderBy(kv => kv.Key).GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public new ValueCollection Values
        {
            get
            {
                return this.OrderBy(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value).Values;
            }
        }
    }
}