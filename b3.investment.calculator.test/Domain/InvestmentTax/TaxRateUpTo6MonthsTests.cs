using b3.investment.calculator.Server.Domain.InvestmentTax;

namespace b3.investment.calculator.test.Domain.InvestmentTax
{
    [TestFixture]
    public class TaxRateUpTo6MonthsTests
    {
        private TaxRateUpTo6Months _taxRate;

        [SetUp]
        public void Setup()
        {
            _taxRate = new TaxRateUpTo6Months();
        }

        [Test]
        public void CalculateTax_ShouldApply22Point5PercentTax()
        {
            // Arrange
            decimal investedAmount = 1000m; // valor investido
            decimal expectedAmountAfterTax = investedAmount * (1 - 0.225m); // 775

            // Act
            decimal result = _taxRate.CalculateTax(investedAmount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAmountAfterTax), "The tax calculation should apply a 22.5% deduction.");
        }
    }
}
