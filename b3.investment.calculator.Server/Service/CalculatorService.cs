using b3.investment.calculator.Server.Domain.InvestmentTax;
using b3.investment.calculator.Server.Service.Interface;

namespace b3.investment.calculator.Server.Service
{
    public class CalculatorService : ICalculatorService
    {
        private const double CDI = 0.009;
        private const double TB = 1.08;
        static double Tax
        {
            get
            {
                return CDI * TB;
            }
        }

        public async Task<decimal> GetGrossProfitabilityAsync(decimal monetary, int months)
        {         
            return await Task.Run(() =>
            {
                return monetary * Convert.ToDecimal(Math.Pow((1 + Tax), months));
            });
        }
        public async Task<decimal> GetNetIncomeAsync(decimal monetary, int months, decimal gross)
        {
            ITaxRate taxRate = TaxRateFactory.CreateTaxRate(months);
            decimal taxableAmount = gross - monetary;

            var tax = await Task.Run(() => taxRate.CalculateTax(taxableAmount));
            return monetary + tax;
        }
    }
}