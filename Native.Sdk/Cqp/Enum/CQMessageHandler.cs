using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q处理消息的方式
	/// </summary>
	public enum CQMessageHandler
	{
		/// <summary>
		/// 忽略消息, 允许后续应用继续处理此消息
		/// </summary>
		Ignore = 0,
		/// <summary>
		/// 拦截消息, 以阻止后续应用继续处理此消息
		/// </summary>
		Intercept = 1
	}
}
