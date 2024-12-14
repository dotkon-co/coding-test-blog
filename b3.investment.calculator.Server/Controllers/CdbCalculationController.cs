using b3.investment.calculator.Server.Request;
using b3.investment.calculator.Server.Service.Interface;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace b3.investment.calculator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdbCalculationController(IInterestCalculatorService interestCalculatorService, InvestmentRequestValidator validator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<InvestmentResponse>(StatusCodes.Status200OK)]
        public  async Task<IActionResult> Post([FromBody] InvestmentRequest investmentRequest)
        {
            ValidationResult validationResult = validator.Validate(investmentRequest);
            if (!validationResult.IsValid)
            {
                var errors = (from error in validationResult.Errors
                              select error.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });                
            }            
            return Ok(await interestCalculatorService.InterestCalculatorAsync(investmentRequest.Monetary, investmentRequest.Period));
        }
    }
}
