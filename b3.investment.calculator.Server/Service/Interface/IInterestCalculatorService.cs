using b3.investment.calculator.Server.Request;

namespace b3.investment.calculator.Server.Service.Interface
{
    public interface IInterestCalculatorService
    {
        Task<InvestmentResponse> InterestCalculatorAsync(decimal monetary, int months);        
    }
}
