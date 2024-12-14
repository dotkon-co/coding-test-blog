namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public static class TaxRateFactory
{
    private static readonly Dictionary<int, ITaxRate> _taxRates = new()
    {
            { 6, new TaxRateUpTo6Months() },
            { 12, new TaxRateUpTo12Months() },
            { 24, new TaxRateUpTo24Months() }
        };

    public static ITaxRate CreateTaxRate(int months)
    {
        // Check if months is within the dictionary
        foreach (var entry in _taxRates)
        {
            if (months <= entry.Key)
            {
                return entry.Value;
            }
        }
        // If not found, return the class for above 24 months
        return new TaxRateAbove24Months();
    }
}
