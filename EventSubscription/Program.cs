using System;
using EventSubscription.Model;

namespace EventSubscription
{
    /// <summary>
    /// Entry point.
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
            var coffee1 = new Coffee("Coffee 1", "Arusha", "Tanzania", Coffee.CoffeeStrength.Decaf);
            var coffee2 = new Coffee("Coffee 2", "Bergendal", "Indonesia", Coffee.CoffeeStrength.Standard);
            var coffee3 = new Coffee("Coffee 3", "Bourbon", "Réunion", Coffee.CoffeeStrength.Strong);

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
                machine.MakeCoffee("Coffee 1");

                if (i < 10)
                {
                    machine.MakeCoffee("Coffee 3");
                }

                if (i == 5)
                {
                    // This coffee doesn't exist: a message should appear.
                    machine.MakeCoffee("Coffee 5");
                }
            }
        }

        /// <summary>
        /// Event handling for the event VendingMachineNotification.
        /// </summary>
        /// <param name="vendingMachine">Vending machine that raised the event.</param>
        /// <param name="message">The message that the vending machine sent.</param>
        private static void HandleVendingMachineNotification(VendingMachine vendingMachine, string message)
        {
            Console.WriteLine(message);
        }
    }
}
