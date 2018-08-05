using System;
using EventSubscription.CustomEventArgs;
using EventSubscription.Model;

namespace EventSubscription
{
    /// <summary>
    /// Entry point.<para />
    /// Based on https://docs.microsoft.com/en-gb/dotnet/standard/events/how-to-raise-and-consume-events. <para />
    /// Further reading:
    /// https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/;
    /// https://blogs.msdn.microsoft.com/ericlippert/2009/04/29/events-and-races/.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var machine = new VendingMachine("Machine in the hall");

            // The program subscribes to the event VendingMachineNotification. That way,
            // the program will be notified whenever the machine wants to "speak" to it.
            machine.VendingMachineNotification += HandleVendingMachineNotification;

            // Fill the machine with coffee!
            var coffee1 = new Coffee("Decaffeinato", "Arabica", "Colombia", Coffee.CoffeeStrength.Decaf);
            var coffee2 = new Coffee("Classico", "Arabica", "Ethiopia", Coffee.CoffeeStrength.Standard);
            var coffee3 = new Coffee("Forte", "Robusta", "Réunion", Coffee.CoffeeStrength.Strong);

            var slot1 = new CoffeeSlot(coffee1);
            var slot2 = new CoffeeSlot(coffee2);
            var slot3 = new CoffeeSlot(coffee3);

            machine.AddCoffeeSlot(slot1);
            machine.AddCoffeeSlot(slot2);
            machine.AddCoffeeSlot(slot3);

            // Display the content of the machine.
            Console.WriteLine(machine.ToString());

            // Make a few coffees.
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"=============== iteration {i} ===============");

                // At the 10th coffee (the limit is 5 by default), the event OutOfBeans will be raised
                // and the method machine.HandleOutOfBeans will be invoked.
                machine.MakeCoffee("Decaffeinato");

                if (i < 10)
                {
                    machine.MakeCoffee("Forte");
                }

                if (i == 5)
                {
                    // This coffee doesn't exist: a message should appear.
                    machine.MakeCoffee("Speciale");
                }
            }
        }

        /// <summary>
        /// Event handling for the event VendingMachineNotification.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="message">The message that the vending machine sent.</param>
        private static void HandleVendingMachineNotification(object sender, VendingMachineNotificationEventArgs e)
        {
            // In our case, we know that the sender is a VendingMachine, but we don't need information
            // from this particular vending machine in our handler.

            Console.WriteLine(e.Message);
        }
    }
}
