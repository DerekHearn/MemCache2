using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemCache
{
    public abstract class CacheItem
    {
        protected DateTime _cachedAt;

        protected bool _dirty;

        protected CacheItem()
        {
            _cachedAt = DateTime.UtcNow;
            _dirty = false;
        }
    }

    public class CacheItem<Key, Value> : CacheItem
    {
        protected Key _key;

        protected Value _value;

        protected CacheItem(Key key, Value value)
            :base()
        {
            _key = key;
            _value = value;
        }
    

    }
}
