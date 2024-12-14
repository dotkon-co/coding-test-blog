using Moq;
using b3.investment.calculator.Server.Service;
using b3.investment.calculator.Server.Service.Interface;

namespace b3.investment.calculator.test.Service
{
    [TestFixture]
    public class InterestCalculatorServiceTests
    {
        private Mock<ICalculatorService> _mockCalculatorService;
        private InterestCalculatorService _interestCalculatorService;

        [SetUp]
        public void Setup()
        {
            _mockCalculatorService = new Mock<ICalculatorService>();
            _interestCalculatorService = new InterestCalculatorService(_mockCalculatorService.Object);
        }

        [Test]
        public async Task InterestCalculatorAsync_ShouldReturnExpectedInvestmentResponse()
        {
            // Arrange
            decimal monetary = 1000m;
            int months = 12;
            decimal expectedGross = 1100m;
            decimal expectedNet = 1050m;

            // Configura os mocks para retornar valores simulados
            _mockCalculatorService.Setup(cs => cs.GetGrossProfitabilityAsync(monetary, months))
                                  .ReturnsAsync(expectedGross);
            _mockCalculatorService.Setup(cs => cs.GetNetIncomeAsync(monetary, months, expectedGross))
                                  .ReturnsAsync(expectedNet);

            // Act
            var result = await _interestCalculatorService.InterestCalculatorAsync(monetary, months);

            // Assert
            Assert.That(result, Is.Not.Null, "The result must not be null");
            Assert.Multiple(() =>
            {
                Assert.That(result.Gross, Is.EqualTo(expectedGross), "Gross profitability is not correct.");
                Assert.That(result.Net, Is.EqualTo(expectedNet), "Net profitability is not correct.");
            });
        }
    }
}
