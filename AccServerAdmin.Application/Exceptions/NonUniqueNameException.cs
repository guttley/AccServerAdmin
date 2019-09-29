using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class NonUniqueNameException : Exception
    {
        public NonUniqueNameException(string message)
            : base(message)
        {

        }
    }
}
