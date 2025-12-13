using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNet10Scaffold.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult GetSum(string firstNumber, string secondNumber) 
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);

                return Ok(sum);
            }
            return BadRequest("Invalid input");
        }

		[HttpGet("sub/{firstNumber}/{secondNumber}")]
		public IActionResult GetSub(string firstNumber, string secondNumber)
		{
			if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
			{
				decimal subValue = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
				return Ok(subValue);
			}
			return BadRequest("Invalid Input Values");
		}

		private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;

            if(decimal.TryParse(
                strNumber,
				System.Globalization.NumberStyles.Any,
				System.Globalization.NumberFormatInfo.InvariantInfo,
                out decimalValue
                ))
            {
                return decimalValue;
            }

            return 0;
        }

        private bool IsNumeric(string strNumber)
        {
            decimal decimalValue;
            bool isNumeric = decimal.TryParse(
                strNumber, 
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out decimalValue
                );

            return isNumeric;
        }
    }
}
