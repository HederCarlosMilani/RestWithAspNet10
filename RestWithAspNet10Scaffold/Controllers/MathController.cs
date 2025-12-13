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

        [HttpGet("mult/{firstNumber}/{secondValue}")]
        public IActionResult GetMult(string firstNumber, string secondValue)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondValue))
            {
                decimal multValue = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondValue);
                return Ok(multValue);
            }
            return BadRequest("Invalid Input Values");
        }

        [HttpGet("div/{firstNumber}/{secondValue}")]
        public IActionResult GetDiv(string firstNumber, string secondValue)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondValue))
            {
                decimal divisor = ConvertToDecimal(secondValue);
                if (divisor == 0)
                {
                    return BadRequest("Division by zero is not allowed.");
                }
                decimal divValue = ConvertToDecimal(firstNumber) / divisor;
                return Ok(divValue);
            }
            return BadRequest("Invalid Input Values");
		}

        [HttpGet("mean/{firstNumber}/{secondValue}")]
            public IActionResult GetMean(string firstNumber, string secondValue)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondValue))
            {
                decimal meanValue = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondValue)) / 2;
                return Ok(meanValue);
            }
            return BadRequest("Invalid Input Values");
		}

        [HttpGet("sqrt/{number}")]
        public IActionResult GetSqrt(string number)
        {
            if (IsNumeric(number))
            {
                double sqrtValue = System.Math.Sqrt((double)ConvertToDecimal(number));
                return Ok(sqrtValue);
            }
            return BadRequest("Invalid Input Value");
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
