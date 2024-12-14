namespace b3.investment.calculator.Server.Domain.InvestmentTax;
public interface ITaxRate
{
    decimal CalculateTax( decimal investedAmount);
}
