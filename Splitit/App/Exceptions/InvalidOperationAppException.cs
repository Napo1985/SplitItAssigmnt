namespace Splitit.App.Exceptions
{
    public class InvalidOperationAppException : InvalidOperationException
    {
        public InvalidOperationAppException() { }

        public InvalidOperationAppException(string message) : base(message) { }

        public InvalidOperationAppException(string message, Exception innerException) : base(message, innerException) { }
    }
}

