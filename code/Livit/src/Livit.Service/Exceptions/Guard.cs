namespace Livit.Service.Exceptions
{
    public static class Guard
    {
        public static void EnsureRequestNotNull(object argument, string argumentName, string service = null)
        {
            if (argument == null)
                throw new RequestArgumentNullException(string.Format("The argument '{0}' is null " + service, argumentName));
        }
    }
}