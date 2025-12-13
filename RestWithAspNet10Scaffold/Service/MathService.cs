using RestWithAspNet10Scaffold.Utils;

namespace RestWithAspNet10Scaffold.Service
{
	
    public class MathService
    {

		public decimal Sum(string firstNumber, string secondNumber)
        {
			if (NumberUtils.IsNumeric(firstNumber) && NumberUtils.IsNumeric(secondNumber))
			{
				var sum = NumberUtils.ConvertToDecimal(firstNumber) + NumberUtils.ConvertToDecimal(secondNumber);

				return sum;
			}
			
			throw new Exception("Invalid Input Values");
		}

        public decimal Sub(string firstNumber, string secondNumber)
        {
			if (NumberUtils.IsNumeric(firstNumber) && NumberUtils.IsNumeric(secondNumber))
			{
				decimal subValue = NumberUtils.ConvertToDecimal(firstNumber) - NumberUtils.ConvertToDecimal(secondNumber);
				return subValue;
			}
			throw new Exception("Invalid Input Values");
		}

		public decimal Mult(string firstNumber, string secondNumber)
		{
			if (NumberUtils.IsNumeric(firstNumber) && NumberUtils.IsNumeric(secondNumber))
			{
				decimal multValue = NumberUtils.ConvertToDecimal(firstNumber) * NumberUtils.ConvertToDecimal(secondNumber);
				return multValue;
			}
			throw new Exception("Invalid Input Values");
		}

		public decimal Div(string firstNumber, string secondNumber)
		{
			if (NumberUtils.IsNumeric(firstNumber) && NumberUtils.IsNumeric(secondNumber))
			{
				decimal divisor = NumberUtils.ConvertToDecimal(secondNumber);
				if (divisor == 0)
				{
					throw new Exception("Division by zero is not allowed.");
				}
				decimal divValue = NumberUtils.ConvertToDecimal(firstNumber) / divisor;
				return divValue;
			}
			throw new Exception("Invalid Input Values");
		}

		public decimal Mean(string firstNumber, string secondNumber)
		{
			if (NumberUtils.IsNumeric(firstNumber) && NumberUtils.IsNumeric(secondNumber))
			{
				decimal meanValue = (NumberUtils.ConvertToDecimal(firstNumber) + NumberUtils.ConvertToDecimal(secondNumber)) / 2;
				return meanValue;
			}
			throw new Exception("Invalid Input Values");
		}

		public decimal Sqrt(string number)
		{
			if (NumberUtils.IsNumeric(number))
			{
				decimal value = NumberUtils.ConvertToDecimal(number);
				if (value < 0)
				{
					throw new Exception("Square root of negative number is not allowed.");
				}
				double sqrtValue = Math.Sqrt((double)value);
				return (decimal)sqrtValue;
			}
			throw new Exception("Invalid Input Value");
		}
	}
}
