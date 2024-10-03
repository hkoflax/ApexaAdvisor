namespace AdvisorManager.Application.Extensions
{
    /// <summary>
    /// A helper class that generates random health status values.
    /// </summary>
    public static class HealthStatusHelper
    {
        private static Random _random = new Random();

        /// <summary>
        /// Generates a random health status based on predefined probabilities.
        /// </summary>
        /// <returns>
        /// A string representing the health status. 
        /// The result is "Green" with 60% probability, "Yellow" with 20% probability, and "Red" with 20% probability.
        /// </returns>
        public static string GenerateHealthStatus()
        {
            int num = _random.Next(1, 101);
            if (num <= 60)
                return "Green";
            if (num <= 80)
                return "Yellow";
            return "Red";
        }
    }
}
