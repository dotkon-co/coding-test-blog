using FluentValidation.TestHelper;
using b3.investment.calculator.Server.Request;

namespace b3.investment.calculator.test.Request
{
    [TestFixture]
    public class InvestmentRequestValidatorTests
    {
        private InvestmentRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new InvestmentRequestValidator();
        }       

        [Test]
        public void Should_Have_Error_When_Monetary_Is_Zero()
        {
            var request = new InvestmentRequest { Monetary = 0, Period = 2 };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Monetary);
        }

        [Test]
        public void Should_Have_Error_When_Monetary_Is_Above_Max()
        {
            var request = new InvestmentRequest { Monetary = 1000001, Period = 2 };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Monetary);
        }
      

        [Test]
        public void Should_Have_Error_When_Period_Is_Zero()
        {
            var request = new InvestmentRequest { Monetary = 1000, Period = 0 };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Period);
        }

        [Test]
        public void Should_Have_Error_When_Period_Is_Above_Max()
        {
            var request = new InvestmentRequest { Monetary = 1000, Period = 301 };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Period);
        }

        [Test]
        public void Should_Not_Have_Error_When_Valid_Request()
        {
            var request = new InvestmentRequest { Monetary = 500000, Period = 12 };
            var result = _validator.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}