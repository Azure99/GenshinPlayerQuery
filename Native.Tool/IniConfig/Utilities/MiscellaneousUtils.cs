using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Utilities
{
	internal class MiscellaneousUtils
	{
		internal static int ByteArrayCompare (byte[] b1, byte[] b2)
		{
			int num = b1.Length.CompareTo (b2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < b1.Length; i++)
			{
				int temp = b1[i].CompareTo (b2[i]);
				if (temp != 0)
				{
					return temp;
				}
			}
			return 0;
		}
	}
}
