using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class BopNotUniqueException : Exception
    {
        public BopNotUniqueException(string message)
            : base(message)
        {

        }
    }
}
