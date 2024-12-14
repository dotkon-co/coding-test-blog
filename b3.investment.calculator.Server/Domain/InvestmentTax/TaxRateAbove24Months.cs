namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public class TaxRateAbove24Months : ITaxRate
{
    public decimal CalculateTax(decimal investedAmount)
    {
        return investedAmount *(1- 0.15M); // 15%
    }
}
