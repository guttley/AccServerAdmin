using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class DriverNameNotUniqueException : Exception
    {
        public DriverNameNotUniqueException(string message)
            : base(message)
        {

        }
    }
}
