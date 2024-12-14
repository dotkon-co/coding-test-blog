using FluentValidation;

namespace b3.investment.calculator.Server.Request
{
    public class InvestmentRequestValidator : AbstractValidator<InvestmentRequest>
    {
        public InvestmentRequestValidator()
        {
            RuleFor(request => request.Monetary)
             .NotNull().WithMessage(Messages.MSG_01)
             .GreaterThan(0).WithMessage(Messages.MSG_01)
             .LessThanOrEqualTo(1000000).WithMessage(Messages.MSG_03); // Mensagem para limite superior

            RuleFor(request => request.Period)
                .NotNull().WithMessage(Messages.MSG_02) // Mensagem para valores nulos                
                .GreaterThan(1).WithMessage(Messages.MSG_02)
                .LessThanOrEqualTo(300).WithMessage(Messages.MSG_04);
        }
    }
}
