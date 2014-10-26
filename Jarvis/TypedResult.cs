using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis
{
    public class TypedResult<T> : IResult
    {
        private T __data;
        public TypedResult()
        { }

        public TypedResult(T data)
        {
            this.__data = data;
        }

        public TypedResult(T data, string commandBuffer)
        {
            this.__data = data;
            this.CommandBuffer = commandBuffer;
        }



        public object Data
        {
            get { return __data; }
        }

        public T TypedData { get { return __data; } }

        public string CommandBuffer
        {
            get;
            set;
        }


        public Core.Message.ICommand Command
        {
            get;
            set;
        }
    }
}