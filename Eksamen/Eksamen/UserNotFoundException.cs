using System;
using System.Runtime.Serialization;

namespace Eksamen
{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}