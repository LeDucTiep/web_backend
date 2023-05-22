namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class InternalException : Exception
    {
        public InternalException() { }
        public InternalException(string? message) : base(message)
        {
        }
    }
}
