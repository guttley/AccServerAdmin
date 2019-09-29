using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class EmptyDirectoryException : Exception
    {
        public EmptyDirectoryException(string message) 
            : base(message)
        {

        }
        
    }
}
