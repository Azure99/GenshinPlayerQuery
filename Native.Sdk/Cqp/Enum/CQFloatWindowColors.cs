using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示 酷Q悬浮窗颜色 的枚举
	/// </summary>
	public enum CQFloatWindowColors
	{
		/// <summary>
		/// 绿色
		/// </summary>
		[Description ("绿色")]
		Green = 1,
		/// <summary>
		/// 橙色
		/// </summary>
		[Description ("橙色")]
		Orange = 2,
		/// <summary>
		/// 红色
		/// </summary>
		[Description ("红色")]
		Red = 3,
		/// <summary>
		/// 深红
		/// </summary>
		[Description ("深红")]
		Crimson = 4,
		/// <summary>
		/// 黑色
		/// </summary>
		[Description ("黑色")]
		Black = 5,
		/// <summary>
		/// 灰色
		/// </summary>
		[Description ("灰色")]
		Gray = 6
	}
}
