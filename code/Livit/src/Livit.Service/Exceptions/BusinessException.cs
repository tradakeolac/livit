using System;
namespace Livit.Service.Exceptions
{

    public abstract class BusinessException : Exception
    {
        protected BusinessException(string message) : base(message)
        {
        }

        public abstract int Code { get; }
        public string Type
        {
            get { return this.GetType().Name; }
        }
    }
}