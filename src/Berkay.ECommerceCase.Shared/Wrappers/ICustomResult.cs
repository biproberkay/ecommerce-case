using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Shared.Wrappers
{
    public interface ICustomResult
    {
        string? Message { get; set; }

        bool Succeeded { get; set; }
    }

    public interface ICustomResult<out T> : ICustomResult
    {
#if false //Invalid varience: 'T' must be invariantly valid on 'ICustomResult<T>' 'T' is covariant
        T Data { get; set; }
#else
        T? Data { get; }
#endif
    }
}
