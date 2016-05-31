using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zed.Core.Resolvers
{
    public interface IServiceResolver
    {
        T GetService<T>();
    }
}
 