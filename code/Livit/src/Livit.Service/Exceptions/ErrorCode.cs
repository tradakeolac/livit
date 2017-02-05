namespace Livit.Service.Exceptions
{
    public enum ErrorCode : int
    {
        Add = 100,
        Delete = 101,
        Update = 102,
        Get = 103,
        ArgumentNull = 202,
        Mapping = 203,
        NotFoundService = 404,
        Common = 500,
        Unknow = 1000,
    }
}
