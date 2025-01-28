using System;
using System.Runtime.Serialization;

namespace Eksamen
{
    [Serializable]
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {

        }
    }
}