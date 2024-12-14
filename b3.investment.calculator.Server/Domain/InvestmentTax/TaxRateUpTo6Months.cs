namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public class TaxRateUpTo6Months : ITaxRate
{
    public decimal CalculateTax(decimal investedAmount)
    {
        return investedAmount *(1- 0.225m); // 22.5%
    }
}
