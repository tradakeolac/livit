namespace Livit.Service.Exceptions
{
    public static class Guard
    {
        public static void EnsureRequestNotNull(object argument, string argumentName, string service = null)
        {
            if (argument == null)
                throw new System.ArgumentNullException(argumentName)
                    .ToBusinessException<RequestArgumentNullException>(string.Format("The argument '{0}' is null " + service, argumentName));
        }

        public static void EnsureStringNotNullOrEmpty(string argument, string argumentName, string service = null)
        {
            if (string.IsNullOrEmpty(argument))
                EnsureRequestNotNull(null, argumentName, service);
        }
    }
}