using System;
using EventSubscription.CustomExceptions;

namespace EventSubscription.Model
{
    /// <summary>
    /// Represents a coffee slot in a vending machine. A coffee slot contains
    /// beans of a specific coffee.
    /// </summary>
    public class CoffeeSlot
    {
        #region statics
        /// <summary>Default stock level of a coffee to put in a slot.</summary>
        private static readonly int defautRefillStockLevel = 15;

        /// <summary>Default minimum stock level.</summary>
        private static readonly int defaultMinimumStockLevel = 5;
        #endregion statics

        #region event and delegate
        /// <summary>Handler for the OutOfBeansHandler event.</summary>
        public delegate void OutOfBeansHandler(CoffeeSlot coffeeSlot);

        /// <summary>Event raised when the slot is going low on beans.</summary>
        public event OutOfBeansHandler OutOfBeans;
        #endregion event and delegate

        #region properties
        /// <summary>Coffee slot's number in the vending machine.</summary>
        public int Number { get; set; }

        /// <summary>Name of the coffee contained in the slot.</summary>
        public string CoffeeName { get; private set; }

        private Coffee _Coffee;
        /// <summary>The coffee contained in the slot.</summary>
        public Coffee Coffee
        {
            get => _Coffee;
            private set
            {
                this._Coffee = value;

                // The coffee slot takes the name of the coffee it contains.
                CoffeeName = value.Name;
            }
        }

        /// <summary>Current stock level of beans in the slot.</summary>
        public int CurrentStockLevel { get; private set; }

        /// <summary>Minimum stock level of beans in the slot the make a coffee.</summary>
        private int MinimumStockLevel { get; set; }
        #endregion properties

        #region constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CoffeeSlot()
        {
            Number = 0;
            Coffee = null;
            CurrentStockLevel = 0;
            MinimumStockLevel = 0;
        }

        /// <summary>
        /// Default constructor for a single coffee.
        /// </summary>
        /// <param name="coffee">The coffee to store in the slot.</param>
        public CoffeeSlot(Coffee coffee)
        {
            Number = 0;
            Coffee = coffee;
            CurrentStockLevel = defautRefillStockLevel;
            MinimumStockLevel = defaultMinimumStockLevel;
        }

        // Other constructors for other properties could be created.
        #endregion constructors

        /// <summary>
        /// Beans are used to make a coffee.
        /// </summary>
        /// <exception cref="OutOfBeansException" />
        public void UseBeans()
        {
            // A coffee can be made only if its stock level is OK. Otherwise,
            // an exception indicating that the stock level is too low is raised.
            if (CurrentStockLevel > MinimumStockLevel)
            {
                // Decrement the stock level.
                CurrentStockLevel--;

                // If the stock level reach the minimum, raise the event.
                if (CurrentStockLevel == MinimumStockLevel)
                {
                    // Check whether the event is null.
                    // Raise the event.
                    OutOfBeans?.Invoke(this);
                }
            }
            else
            {
                throw new OutOfBeansException($"Slot {Number}: I can't make coffee anymore, I'm empty. Please refill!");
            }
        }

        /// <summary>
        /// Display the coffee slot's information.
        /// </summary>
        /// <returns>The coffee slot's information.</returns>
        public override string ToString()
        {
            return $"Slot {Number}: " + Coffee.ToString();
        }
    }
}
