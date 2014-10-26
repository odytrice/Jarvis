using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis
{
    public class TypedResult<T> : IResult
    {
        private T __data;
        public TypedResult(T data)
        {
            this.__data = data;
        }



        public object Data
        {
            get { return __data; }
        }

        public T TypedData { get { return __data; } }
    }
}