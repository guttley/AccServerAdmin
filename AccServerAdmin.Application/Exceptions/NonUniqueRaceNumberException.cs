using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class NonUniqueRaceNumberException : Exception
    {
        public NonUniqueRaceNumberException(string message)
            : base(message)
        {

        }
    }
}
