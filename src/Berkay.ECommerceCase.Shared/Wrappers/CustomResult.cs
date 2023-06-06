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
        //Solved issue: Implement interface includes throw new NotImplementedException... why?
        // https://stackoverflow.com/a/51550759/12878692
#if false
        public List<string> Messages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Succeeded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } 
#endif
        public List<string>? Messages { get; set; }
        public bool Succeeded { get; set; }

        public static ICustomResult Fail(string message)
        {
            return new CustomResult { Succeeded = false, Messages = new List<string> { message } };
        }

        public static ICustomResult Fail(List<string> messages)
        {
            return new CustomResult { Succeeded = false, Messages = messages };
        }

        public static ICustomResult Success(string message)
        {
            return new CustomResult { Succeeded = true, Messages = new List<string> { message } };
        }
    }

    public class CustomResult<T> : CustomResult, ICustomResult<T>
    {
        public CustomResult()
        {

        }

        public T? Data { get; set; }


        public static CustomResult<T> Fail(T tData, string message)
        {
            return new CustomResult<T> { Succeeded = false, Data = tData, Messages = new List<string> { message } };
        }
        public new static ICustomResult<T> Fail(string message)
        {
            return new CustomResult<T> { Succeeded = false, Messages = new List<string> { message } };
        }
        public new static ICustomResult<T> Fail(List<string> messages)
        {
            return new CustomResult<T> { Succeeded = false, Messages = messages };
        }

        /// <summary>
        /// No need to send a message situation
        /// </summary>
        /// <param name="data"></param>
        /// <returns>CustomResult<T></returns>
        public static ICustomResult<T> Success(T data)
        {
            return new CustomResult<T> { Succeeded = true, Data = data };
        }
        public static ICustomResult<T> Success(T data, string message)
        {
            return new CustomResult<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }
        public static ICustomResult<T> Success(T data, List<string> messages)
        {
            return new CustomResult<T> { Succeeded = true, Data = data, Messages = messages };
        }
    }

}
