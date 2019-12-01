using System;

namespace AccServerAdmin.Application.Exceptions
{
    public class SteamIdNotUniqueException : Exception
    {
        public SteamIdNotUniqueException(string message)
            : base(message)
        {

        }
    }
}
