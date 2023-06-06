using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Shared.Wrappers
{
    public class CustomResult : ICustomResult
    {
        public CustomResult()
        {

        }
        public string? Message { get; set; }
        public bool Succeeded { get; set; }

        public static ICustomResult Fail(string message)
        {
            return new CustomResult { Succeeded = false, Message = message  };
        }

        public static ICustomResult Success(string message)
        {
            return new CustomResult { Succeeded = true, Message = message  };
        }
    }

    public class CustomResult<T> : CustomResult, ICustomResult<T>
    {
        public CustomResult()
        {

        }

        public T? Data { get; set; }


        public new static CustomResult<T> Fail(string message)
        {
            return new CustomResult<T> { Succeeded = false, Message = message };
        }
        public static Task<CustomResult<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }
        public static CustomResult<T> Fail(T tData, string message)
        {
            return new CustomResult<T> { Succeeded = false, Data = tData, Message =  message };
        }
        public static Task<CustomResult<T>> FailAsync(T tData, string message)
        {
            return Task.FromResult(Fail(tData, message));
        }

        public static CustomResult<T> Success(T data)
        {
            return new CustomResult<T> { Succeeded = true, Data = data };
        }
        public static CustomResult<T> Success(T data, string message)
        {
            return new CustomResult<T> { Succeeded = true, Data = data, Message = message };
        }

        public static Task<CustomResult<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }
    }
}
