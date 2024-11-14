namespace BuildingBlocks.ExceptionHandling
{
    public class DataExistingException : Exception
    {
        public DataExistingException(string? message) : base(message)
        {
        }

        public DataExistingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
