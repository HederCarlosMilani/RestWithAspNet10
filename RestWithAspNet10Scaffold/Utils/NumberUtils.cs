namespace RestWithAspNet10Scaffold.Utils
{
    public class NumberUtils
    {
		public static decimal ConvertToDecimal(string strNumber)
		{
			decimal decimalValue;

			if (decimal.TryParse(
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

		public static bool IsNumeric(string strNumber)
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
