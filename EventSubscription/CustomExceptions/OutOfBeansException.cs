using System;

namespace EventSubscription.CustomExceptions
{
    /// <summary>
    /// Custom exception raised when a coffee slot is out of coffee beans
    /// </summary>
    class OutOfBeansException : Exception
    {
        public OutOfBeansException(string message) : base(message)
        {

        }
    }
}
