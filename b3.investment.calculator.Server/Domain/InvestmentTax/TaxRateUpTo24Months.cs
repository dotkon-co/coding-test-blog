namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public class TaxRateUpTo24Months : ITaxRate
{
    public decimal CalculateTax(decimal investedAmount)
    {
        return investedAmount *(1- 0.175m); // 17.5%
    }
}
