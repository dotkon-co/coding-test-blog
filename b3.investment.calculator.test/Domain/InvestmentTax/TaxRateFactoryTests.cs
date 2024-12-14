using b3.investment.calculator.Server.Domain.InvestmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b3.investment.calculator.test.Domain.InvestmentTax
{
    [TestFixture]
    public class TaxRateFactoryTests
    {
        [Test]
        public void CreateTaxRate_ShouldReturnTaxRateUpTo6Months_WhenMonthsIs6OrLess()
        {
            // Arrange
            int months = 6;

            // Act
            ITaxRate taxRate = TaxRateFactory.CreateTaxRate(months);

            // Assert
            Assert.That(taxRate, Is.InstanceOf<TaxRateUpTo6Months>(), "Expected a TaxRateUpTo6Months instance.");
        }

        [Test]
        public void CreateTaxRate_ShouldReturnTaxRateUpTo12Months_WhenMonthsIsBetween7And12()
        {
            // Arrange
            int months = 10;

            // Act
            ITaxRate taxRate = TaxRateFactory.CreateTaxRate(months);

            // Assert
            Assert.That(taxRate, Is.InstanceOf<TaxRateUpTo12Months>(), "Expected a TaxRateUpTo12Months instance.");
        }

        [Test]
        public void CreateTaxRate_ShouldReturnTaxRateUpTo24Months_WhenMonthsIsBetween13And24()
        {
            // Arrange
            int months = 18;

            // Act
            ITaxRate taxRate = TaxRateFactory.CreateTaxRate(months);

            // Assert
            Assert.That(taxRate, Is.InstanceOf<TaxRateUpTo24Months>(), "Expected a TaxRateUpTo24Months instance.");
        }

        [Test]
        public void CreateTaxRate_ShouldReturnTaxRateAbove24Months_WhenMonthsIsGreaterThan24()
        {
            // Arrange
            int months = 30;

            // Act
            ITaxRate taxRate = TaxRateFactory.CreateTaxRate(months);

            // Assert
            Assert.That(taxRate, Is.InstanceOf<TaxRateAbove24Months>(), "Expected a TaxRateAbove24Months instance.");
        }
    }
}
