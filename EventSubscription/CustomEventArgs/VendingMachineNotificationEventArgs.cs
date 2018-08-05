using System;

namespace EventSubscription.CustomEventArgs
{
    /// <summary>
    /// Custom EventArgs for a vending machine notification.
    /// </summary>
    public class VendingMachineNotificationEventArgs : EventArgs
    {
        /// <summary>Message sent by the vending machine.</summary>
        public string Message { get; set; }
    }
}
