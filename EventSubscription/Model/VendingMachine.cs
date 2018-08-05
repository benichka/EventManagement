using System;
using System.Collections.Generic;
using System.Text;
using EventSubscription.CustomEventArgs;
using EventSubscription.CustomExceptions;

namespace EventSubscription.Model
{
    /// <summary>
    /// Represents a vending machine containing multiple coffee slots.
    /// </summary>
    public class VendingMachine
    {
        #region event and delegate
        /// <summary>Handler for the VendingMachineNotification event.</summary>
        public delegate void VendingMachineNotificationHandler(object sender, VendingMachineNotificationEventArgs e);

        /// <summary>Event raised whenener the machine wants to notify the user of something.</summary>
        public event VendingMachineNotificationHandler VendingMachineNotification;
        #endregion event and delegate

        #region properties
        /// <summary>Name of the machine.</summary>
        public string Name { get; set; }

        /// <summary>Number of slots in use in the machine.</summary>
        private int slotCounter;

        private List<CoffeeSlot> _CoffeeSlots;
        /// <summary>List of coffees that the machine contains.</summary>
        public List<CoffeeSlot> CoffeeSlots
        {
            get => this._CoffeeSlots;
            private set
            {
                // Setting the slots with the coffees.
                this._CoffeeSlots = value;

                // For each coffee slot, the vending machine subscribes to the
                // event OutOfBeans. That way, the vending machine will be notified
                // whenever a slot reaches its minimum capacity.
                foreach (var coffeeSlot in _CoffeeSlots)
                {
                    coffeeSlot.OutOfBeans += HandleOutOfBeans;
                }
            }
        }
        #endregion properties

        /// <summary>
        /// Constructor.
        /// </summary>
        public VendingMachine(string name)
        {
            this.slotCounter = 0;
            this.Name = name;
            CoffeeSlots = new List<CoffeeSlot>();
        }

        /// <summary>
        /// Add a coffee slot.
        /// </summary>
        /// <param name="coffeeSlot">The coffeeSlot to add</param>
        public void AddCoffeeSlot(CoffeeSlot coffeeSlot)
        {
            // Add a slot number and give it to the new slot.
            this.slotCounter++;

            coffeeSlot.Number = this.slotCounter;

            // The coffee slot is added to the machine.
            CoffeeSlots.Add(coffeeSlot);

            // The vending machine then subscribes to the new coffee slot.
            coffeeSlot.OutOfBeans += HandleOutOfBeans;
        }

        /// <summary>
        /// Coffee making.
        /// </summary>
        /// <param name="coffeeName">The name of the coffee to make.</param>
        public void MakeCoffee(string coffeeName)
        {
            var selectedCoffee = CoffeeSlots.Find(coffeeSlots => coffeeSlots.CoffeeName.Equals(coffeeName));
            if (selectedCoffee != null)
            {
                try
                {
                    selectedCoffee.UseBeans();

                    var eventArgs = new VendingMachineNotificationEventArgs() { Message = $"Your coffee {coffeeName} has been made. Enjoy!" };
                    VendingMachineNotification?.Invoke(this, eventArgs);
                }
                catch (OutOfBeansException oobEx)
                {
                    var eventArgs = new VendingMachineNotificationEventArgs() { Message = oobEx.Message };
                    VendingMachineNotification?.Invoke(this, eventArgs);
                }
            }
            else
            {
                var eventArgs = new VendingMachineNotificationEventArgs() { Message = $"the coffee {coffeeName} is not available in that machine" };
                VendingMachineNotification?.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// Event handling for the event OutOfBeans.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="e">Event arguments that was sent in the event.</param>
        private void HandleOutOfBeans(object sender, EventArgs e)
        {
            // In our case, we know that the sender is a CoffeeSlot with no arguments.
            var coffeeSlot = (CoffeeSlot)sender;

            var eventArgs = new VendingMachineNotificationEventArgs() { Message = $"Vending machine {Name}: coffee {coffeeSlot.CoffeeName} reached its minimum level." };
            VendingMachineNotification?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Display the vending machine's information.
        /// </summary>
        /// <returns>The vending machine's information.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Vending machine {Name}:");
            sb.Append("\r\n");
            foreach (var coffeeSlot in CoffeeSlots)
            {
                sb.Append(coffeeSlot.ToString());
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }
}
