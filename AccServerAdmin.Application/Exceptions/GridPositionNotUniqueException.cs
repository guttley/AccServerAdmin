using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class GridPositionNotUniqueException : Exception
    {
        public GridPositionNotUniqueException(string message)
            : base(message)
        {

        }
    }
}
