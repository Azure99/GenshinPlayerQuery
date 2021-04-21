using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 表示酷Q日志中的类型等级
	/// </summary>
	public enum CQLogLevel
	{
		/// <summary>
		/// 调试.		颜色: 灰色
		/// </summary>
		Debug = 0,
		/// <summary>
		/// 信息.		颜色: 黑色
		/// </summary>
		Info = 10,
		/// <summary>
		/// 信息 (成功)	颜色: 紫色
		/// </summary>
		InfoSuccess = 11,
		/// <summary>
		/// 信息 (接收)	颜色: 蓝色
		/// </summary>
		InfoReceive = 12,
		/// <summary>
		/// 信息 (发送)	颜色: 绿色
		/// </summary>
		InfoSend = 13,
		/// <summary>
		/// 警告			颜色: 橙色
		/// </summary>
		Warning = 20,
		/// <summary>
		/// 错误			颜色: 红色
		/// </summary>
		Error = 30,
		/// <summary>
		/// 致命错误		颜色: 深红色
		/// </summary>
		Fatal = 40
	}
}
