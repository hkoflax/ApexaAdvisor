using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvisorManager.Application.Extensions
{
    public static class HealthStatusHelper
    {
        private static Random _random = new Random();

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
