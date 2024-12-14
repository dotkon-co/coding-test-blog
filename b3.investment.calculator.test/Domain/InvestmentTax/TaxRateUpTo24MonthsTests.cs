using b3.investment.calculator.Server.Domain.InvestmentTax;

namespace b3.investment.calculator.test.Domain.InvestmentTax
{
    [TestFixture]
    public class TaxRateUpTo24MonthsTests
    {
        private TaxRateUpTo24Months _taxRate;

        [SetUp]
        public void Setup()
        {
            _taxRate = new TaxRateUpTo24Months();
        }

        [Test]
        public void CalculateTax_ShouldApply17Point5PercentTax()
        {
            // Arrange
            decimal investedAmount = 1000m; // valor investido
            decimal expectedAmountAfterTax = investedAmount * (1 - 0.175m); // 825

            // Act
            decimal result = _taxRate.CalculateTax(investedAmount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAmountAfterTax), "The tax calculation should apply a 17.5% deduction.");
        }
    }
}
