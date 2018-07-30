namespace EventSubscription.Model
{
    /// <summary>
    /// Represents a coffee type in a vending machine.
    /// </summary>
    public class Coffee
    {
        /// <summary>
        /// Describes a coffee strength.
        /// </summary>
        public enum CoffeeStrength { Decaf, Standard, Strong }

        #region properties
        /// <summary>Coffee's name.</summary>
        public string Name { get; set; }

        /// <summary>Coffee's bean type.</summary>
        public string Bean { get; private set; }

        /// <summary>Coffee's country of origin.</summary>
        public string CountryOfOrigin { get; private set; }

        /// <summary>Coffee's strength.</summary>
        public CoffeeStrength Strength { get; private set; }
        #endregion properties

        /// <summary>
        /// Complete constructor.
        /// </summary>
        /// <param name="name">Name of the coffee to create.</param>
        /// <param name="bean">Bean of the coffee to create. The type of bean can not be changed afterward.</param>
        /// <param name="countryOfOrigin">Country of origin of the coffee to create.The country of origin can not be changed afterward.</param>
        /// <param name="strength">Strength of the coffee to create. The strength of the coffee can not be changed afterward.</param>
        public Coffee(string name, string bean, string countryOfOrigin, CoffeeStrength strength)
        {
            Name = name;
            Bean = bean;
            CountryOfOrigin = countryOfOrigin;
            Strength = strength;
        }

        /// <summary>
        /// Display the coffee's information.
        /// </summary>
        /// <returns>The coffee's information.</returns>
        public override string ToString()
        {
            return $"Coffee – name: {Name}; type of bean: {Bean}; country of origin: {CountryOfOrigin}; strength: {Strength}";
        }
    }
}
