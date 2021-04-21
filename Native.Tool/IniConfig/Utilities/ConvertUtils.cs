using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Utilities
{
	internal static class ConvertUtils
	{
		internal static TimeSpan StringToTimeSpan (string input)
		{
			return TimeSpan.Parse (input, CultureInfo.InvariantCulture);
		}

		internal static BigInteger ToBigInteger (object value)
		{
			if (value is BigInteger)
			{
				return (BigInteger)value;
			}

			string value2 = value as string;
			if (value2 != null)
			{
				return BigInteger.Parse (value2, CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				float value3 = (float)value;
				return new BigInteger (value3);
			}
			if (value is double)
			{
				double value4 = (double)value;
				return new BigInteger (value4);
			}
			if (value is decimal)
			{
				decimal value5 = (decimal)value;
				return new BigInteger (value5);
			}
			if (value is int)
			{
				int value6 = (int)value;
				return new BigInteger (value6);
			}
			if (value is long)
			{
				long value7 = (long)value;
				return new BigInteger (value7);
			}
			if (value is uint)
			{
				uint value8 = (uint)value;
				return new BigInteger (value8);
			}
			if (value is ulong)
			{
				ulong value9 = (ulong)value;
				return new BigInteger (value9);
			}
			byte[] value10;
			if ((value10 = (value as byte[])) != null)
			{
				return new BigInteger (value10);
			}

			throw new InvalidCastException (string.Format ("无法将 {0} 转换为 BigInteger.", value.GetType ().Name));
		}
	}
}
