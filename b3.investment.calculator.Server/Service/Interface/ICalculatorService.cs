namespace b3.investment.calculator.Server.Service.Interface
{
    public interface ICalculatorService
    {        
        Task<decimal> GetGrossProfitabilityAsync(decimal monetary, int months);
        Task<decimal> GetNetIncomeAsync(decimal monetary, int months, decimal gross);
    }
}
