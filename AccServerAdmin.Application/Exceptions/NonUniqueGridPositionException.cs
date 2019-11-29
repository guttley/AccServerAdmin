using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class NonUniqueGridPositionException : Exception
    {
        public NonUniqueGridPositionException(string message)
            : base(message)
        {

        }
    }
}
