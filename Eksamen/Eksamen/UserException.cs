using System;
using System.Runtime.Serialization;

namespace Eksamen
{
    [Serializable]
    internal class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }
}