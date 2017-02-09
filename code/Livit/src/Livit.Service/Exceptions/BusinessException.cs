using System;

namespace Livit.Service.Exceptions
{
    public abstract class BusinessException : Exception
    {
        protected BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        public abstract int Code { get; }

        public string Type
        {
            get { return this.GetType().Name; }
        }
        public override string HelpLink
        {
            get
            {
                return $"http://api.liv.it/references/errors/{Type.ToLower()}.html";
            }
        }
    }
}