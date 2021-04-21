using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Utilities
{
	internal class MathUtils
	{
		internal static bool ApproxEquals(double a, double b)
		{
			if (a == b)
			{
				return true;
			}
			double num = (Math.Abs (a) + Math.Abs (b) + 10.0) * 2.2204460492503131E-16;
			double num2 = a - b;
			return -num < num2 && num > num2;
		}
	}
}
