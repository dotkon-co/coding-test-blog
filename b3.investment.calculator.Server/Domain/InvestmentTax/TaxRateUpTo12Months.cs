namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public class TaxRateUpTo12Months : ITaxRate
{
    public decimal CalculateTax(decimal investedAmount)
    {
        return investedAmount *(1- 0.20m); // 20%
    }
}
