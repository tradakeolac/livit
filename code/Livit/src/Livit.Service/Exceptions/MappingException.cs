namespace Livit.Service.Exceptions
{

    public class MappingException : BusinessException
    {
        public MappingException(string message) : base(message)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Mapping;
            }
        }
    }
}