using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemCache
{
    public interface ICacheVisitor
    {
        void visit(Cache cache);
    }
}
