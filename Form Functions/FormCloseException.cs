using System;

namespace Form_Functions
{
    public class FormCloseException : Exception
    {
        public FormCloseException(string message) : base(message) { }
        public FormCloseException() { }
        public FormCloseException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class LogInFormCloseException : FormCloseException
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Closer Closer { get; set; }
        public LogInFormCloseException(string message) : base(message) { }
        public LogInFormCloseException() { }
        public LogInFormCloseException(string message, Exception innerException) : base(message, innerException) { }
    }

    public enum Closer
    {
        CancelButton, ConfirmButton, User
    }
}