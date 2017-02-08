namespace Livit.Model.Entities
{
    using System;

    public class TokenResponseEntity : EntityBase<string>
    {
        public string AccessToken { get; set; }
        public long? ExpiresInSeconds { get; set; }
        public DateTime Issued { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
    }
}