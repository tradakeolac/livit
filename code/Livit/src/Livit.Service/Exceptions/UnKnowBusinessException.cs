namespace Livit.Service.Exceptions
{
    public class UnKnowBusinessException : BusinessException
    {
        public UnKnowBusinessException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Unknow; }
        }
    }
}