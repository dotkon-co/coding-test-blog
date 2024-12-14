using b3.investment.calculator.Server.Service;

namespace b3.investment.calculator.test.Service
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        private CalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            // Configura uma instância do CalculatorService para cada teste
            _calculatorService = new CalculatorService();
        }

        [Test]
        public async Task GetGrossProfitabilityAsync_ShouldReturnExpectedGrossProfit()
        {
            // Arrange
            decimal monetary = 1000m;
            int months = 12;
            double tax = 0.009 * 1.08; // O valor esperado do Tax baseado nos valores CDI e TB no código

            // Act
            var result = await _calculatorService.GetGrossProfitabilityAsync(monetary, months);
            decimal expectedGross = monetary * Convert.ToDecimal(Math.Pow(1 + tax, months));

            // Assert
            Assert.That(result, Is.EqualTo(expectedGross), "Gross profitability is not correct.");
        }

        [Test]
        public async Task GetNetIncomeAsync_ShouldReturnExpectedNetIncome()
        {
            // Arrange
            decimal monetary = 1000m;
            int months = 12;
            decimal gross = 1200m; // Simule um valor bruto de rendimento
            decimal expectedTax = 160m; // Simule o valor do imposto esperado (baseado no retorno do mock)

            // Act
            var result = await _calculatorService.GetNetIncomeAsync(monetary, months, gross);

            // Assert
            decimal expectedNetIncome = monetary + expectedTax;
            Assert.That(result, Is.EqualTo(expectedNetIncome), "Net income is not correct.");
        }
    }
}
