﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Attribute
{
	/// <summary>
	/// 表示对象不参与 IniConfig 的序列化的特性
	/// </summary>
	[Obsolete ("该类型跟随 IniConvert 类型过期")]
	[AttributeUsage (AttributeTargets.Property)]
	public sealed class IniNonSerializeAttribute : System.Attribute
	{

	}
}
