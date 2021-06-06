namespace CleanCli.Todo.Domain.Exceptions
{
    using System;

    public class UnsupportedColourException : Exception
    {
        public UnsupportedColourException(string code)
            : base($"Colour \"{code}\" is unsupported.")
        {
        }
    }
}
