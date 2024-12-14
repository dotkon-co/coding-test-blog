using b3.investment.calculator.Server.Domain.InvestmentTax;

namespace b3.investment.calculator.test.Domain.InvestmentTax
{
    [TestFixture]
    public class TaxRateAbove24MonthsTests
    {
        private TaxRateAbove24Months _taxRate;

        [SetUp]
        public void Setup()
        {
            _taxRate = new TaxRateAbove24Months();
        }
        /// <summary>
        /// The tax calculation should apply a 15% deduction."
        /// </summary>
        [Test]
        public void CalculateTax_ShouldApply15PercentTax()
        {
            // Arrange
            decimal investedAmount = 1000m; // valor investido
            decimal expectedAmountAfterTax = investedAmount * (1 - 0.15m); // 850

            // Act
            decimal result = _taxRate.CalculateTax(investedAmount);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAmountAfterTax));
        }
    }
}
