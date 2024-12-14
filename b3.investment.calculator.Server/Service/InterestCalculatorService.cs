using b3.investment.calculator.Server.Domain.InvestmentTax;
using b3.investment.calculator.Server.Request;
using b3.investment.calculator.Server.Service.Interface;

namespace b3.investment.calculator.Server.Service
{
    public class InterestCalculatorService(ICalculatorService calculatorService) :  IInterestCalculatorService
    {
        private readonly ICalculatorService _calculatorService = calculatorService;
        public async Task<InvestmentResponse> InterestCalculatorAsync(decimal monetary, int months)
        {
            var gross = await _calculatorService.GetGrossProfitabilityAsync(monetary, months);
            var net = await _calculatorService.GetNetIncomeAsync(monetary, months, gross);
            return new InvestmentResponse
            {
                Gross = gross,
                Net = net
            };
        }
    }
}
