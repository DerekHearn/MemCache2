using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemCache
{
    public class Cache
    {
        private static Cache _cache = new Cache();

        public static Cache instance
        {
            get
            {
                return _cache;
            }
        }

        private const int _SIZE = 64;

        protected CacheItem[] _items = new CacheItem[_SIZE];

        protected object myLock = new object();

        protected int _index = 0;

        public CacheItem[] items
        {
            get
            {
                return _items;
            }
        }

        public virtual void push(CacheItem item)
        {
            lock(myLock)
            {
                _items[_index++] = item;
                if (_index >= _SIZE)
                {
                    _index = 0;
                }
            }
        }

        public virtual void accept(ICacheVisitor visitor)
        {
            visitor.visit(_cache);
        }

        public bool tryGetUserID(Guid sessionGuid, out int userID) 
        {
            var visitor = new GetUserItemVisitor(sessionGuid);
            accept(visitor);
            var item = visitor.getItem();
            if(item != null)
            {
                userID = item.userID;
                return true;
            }
            else
            {
                userID = -1;
                return false;
            }
        }

        public bool tryGetUserID(string sessionGuid, out int userID)
        {
            Guid outGuid;
            var success = Guid.TryParse(sessionGuid, out outGuid);
            if (success)
            {
                return tryGetUserID(outGuid, out userID);
            }
            else
            {
                userID = -1;
                return false;
            }
        }
    }
}
