using System;
using System.Runtime.Serialization;

namespace Eksamen
{
    [Serializable]
    internal class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message) : base(message)
        {
        }

    }
}