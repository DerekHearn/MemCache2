using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemCache
{
    public class UserCacheItem : CacheItem<Guid, int>
    {
        public Guid sessionGuid
        {
            get
            {
                return _key;
            }
        }

        public int userID
        {
            get
            {
                return _value;
            }
        }

        public UserCacheItem(int userID, Guid sessionGuid)
            : base(sessionGuid, userID) { }

        public static UserCacheItem create(int userID, string sessionGuid)
        {
            Guid outGuid;
            var success = Guid.TryParse(sessionGuid, out outGuid);
            if (success)
                return new UserCacheItem(userID, outGuid);
            else
                return null;
        }
    }

    class GetUserItemVisitor : ICacheVisitor
    {
        private Guid _sessionGuid;

        private UserCacheItem _item = null;

        private bool _visited = false;

        public GetUserItemVisitor(Guid sessionGuid)
        {
            _sessionGuid = sessionGuid;
        }

        public void visit(Cache cache)
        {
            _visited = true;
            var items = cache.items;

            foreach (UserCacheItem item in items)
            {
                if (item.sessionGuid.Equals(_sessionGuid))
                {
                    _item = item;
                    break;
                }
            }
        }

        public UserCacheItem getItem()
        {
            if (!_visited)
                throw new Exception("Must visit the cache before calling getItem");

            return _item;
        }
    }
}
