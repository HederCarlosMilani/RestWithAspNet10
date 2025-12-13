using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Service;

namespace RestWithAspNet10Scaffold.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {

        private readonly MathService _mathService;

        public MathController(MathService mathService)
        {
            this._mathService = mathService;
		}

		[HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult GetSum(string firstNumber, string secondNumber) 
        {
            try
            {
                var sum = _mathService.Sum(firstNumber, secondNumber);
                return Ok(sum);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpGet("sub/{firstNumber}/{secondNumber}")]
		public IActionResult GetSub(string firstNumber, string secondNumber)
		{
            try
            {
                var sub = _mathService.Sub(firstNumber, secondNumber);
                return Ok(sub);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}

        [HttpGet("mult/{firstNumber}/{secondNumber}")]
        public IActionResult GetMult(string firstNumber, string secondNumber)
        {
            try
            {
                var mult = _mathService.Mult(firstNumber, secondNumber);
                return Ok(mult);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("div/{firstNumber}/{secondValue}")]
        public IActionResult GetDiv(string firstNumber, string secondValue)
        {
            try
            {
                decimal div = _mathService.Div(firstNumber, secondValue);
                return Ok(div);
			}
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
			}
		}

        [HttpGet("mean/{firstNumber}/{secondValue}")]
            public IActionResult GetMean(string firstNumber, string secondValue)
        {
            try
            {
                decimal mean = _mathService.Mean(firstNumber, secondValue);
                return Ok(mean);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}

        [HttpGet("sqrt/{number}")]
        public IActionResult GetSqrt(string number)
        {
            try
            {
                decimal sqrt = _mathService.Sqrt(number);
                return Ok(sqrt);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}

    }
}
