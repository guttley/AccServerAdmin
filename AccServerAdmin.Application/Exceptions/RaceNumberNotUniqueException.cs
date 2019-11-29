using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class RaceNumberNotUniqueException : Exception
    {
        public RaceNumberNotUniqueException(string message)
            : base(message)
        {

        }
    }
}
