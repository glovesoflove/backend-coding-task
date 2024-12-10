namespace Claims
{
    public static class Premium
    {

        /// <summary>
        /// Premium depends on the covered object type and insurance period
        /// length. Base rate: 1250/day. Yacht +10%, Passenger ship +20%,
        /// Tanker +50%, other types +30%. First 30 days use base rate,
        /// next 150 days discounted by 5% for Yacht/2% for others, remaining
        /// days discounted extra 3% for Yacht/1% for others.
        /// </summary>
        public static decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
        {
            decimal basePremiumPerDay = GetBasePremiumPerDay(coverType);
            int insuranceLengthInDays = (endDate - startDate).Days;

            decimal totalPremium = 0;
            int daysProcessed = 0;

            while (daysProcessed < insuranceLengthInDays)
            {
                int daysToProcess = Math.Min(insuranceLengthInDays - daysProcessed, 30);
                totalPremium += CalculatePremiumForDays(basePremiumPerDay, daysToProcess, coverType, 0);
                daysProcessed += daysToProcess;

                daysToProcess = Math.Min(insuranceLengthInDays - daysProcessed, 180 - 30);
                decimal discount = coverType == CoverType.Yacht ? 0.05m : 0.02m;
                totalPremium += CalculatePremiumForDays(basePremiumPerDay, daysToProcess, coverType, discount);
                daysProcessed += daysToProcess;

                daysToProcess = insuranceLengthInDays - daysProcessed;
                discount = coverType == CoverType.Yacht ? 0.08m : 0.03m;
                totalPremium += CalculatePremiumForDays(basePremiumPerDay, daysToProcess, coverType, discount);
                daysProcessed += daysToProcess;
            }

            return totalPremium;
        }

        private static decimal GetBasePremiumPerDay(CoverType coverType)
        {
            decimal multiplier = coverType switch
            {
                CoverType.Yacht => 1.1m,
                CoverType.PassengerShip => 1.2m,
                CoverType.Tanker => 1.5m,
                _ => 1.3m
            };

            return 1250 * multiplier;
        }

        private static decimal CalculatePremiumForDays(decimal basePremiumPerDay, int days, CoverType coverType, decimal discount)
        {
            decimal premiumForDays = basePremiumPerDay * days;
            return coverType == CoverType.Yacht ? premiumForDays * (1 - discount) : premiumForDays * (1 - discount);
        }

      //We can't use the original method to compare against the new one, as I've been told that it has bugs
      public static decimal OldComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
        {
            var multiplier = 1.3m;
            if (coverType == CoverType.Yacht)
            {
                multiplier = 1.1m;
            }

            if (coverType == CoverType.PassengerShip)
            {
                multiplier = 1.2m;
            }

            if (coverType == CoverType.Tanker)
            {
                multiplier = 1.5m;
            }

            var premiumPerDay = 1250 * multiplier;
            var insuranceLength = (endDate - startDate).TotalDays;
            var totalPremium = 0m;

            for (var i = 0; i < insuranceLength; i++)
            {
                if (i < 30) totalPremium += premiumPerDay;
                if (i < 180 && coverType == CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.05m;
                else if (i < 180) totalPremium += premiumPerDay - premiumPerDay * 0.02m;
                if (i < 365 && coverType != CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.03m;
                else if (i < 365) totalPremium += premiumPerDay - premiumPerDay * 0.08m;
            }

            return totalPremium;
        }
    }
}

