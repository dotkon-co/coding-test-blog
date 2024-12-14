using b3.investment.calculator.Server.Domain.InvestmentTax;

namespace b3.investment.calculator.test.Domain.InvestmentTax
{
    [TestFixture]
    public class TaxRateUpTo12MonthsTests
    {
        private TaxRateUpTo12Months _taxRate;

        [SetUp]
        public void Setup()
        {
            _taxRate = new TaxRateUpTo12Months();
        }

        [Test]
        public void CalculateTax_ShouldApply20PercentTax()
        {
            // Arrange
            decimal investedAmount = 1000m; // valor investido
            decimal expectedAmountAfterTax = investedAmount * (1 - 0.20m); // 800

            // Act
            decimal result = _taxRate.CalculateTax(investedAmount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAmountAfterTax), "The tax calculation should apply a 20% deduction.");
        }
    }
}
