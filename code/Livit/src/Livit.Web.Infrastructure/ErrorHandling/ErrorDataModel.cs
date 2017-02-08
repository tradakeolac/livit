namespace Livit.Web.Infrastructure.ErrorHandling
{
    using System;

    /// <summary>
    /// Define the error model to return to the client
    /// </summary>
    public class ErrorDataModel
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public int Code { get; set; }
        public string Type { get; set; }
        public string HelpLink { get; set; }
        public DateTime DateTime { get; set; }
    }
}