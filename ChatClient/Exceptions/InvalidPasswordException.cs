using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChatClient.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
        {
        }

        public InvalidPasswordException(string message) : base(message)
        {
        }

        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
