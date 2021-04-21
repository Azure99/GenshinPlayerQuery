using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q 群添加请求 的事件类型
	/// </summary>
	public enum CQGroupAddRequestType
	{
		/// <summary>
		/// 申请入群
		/// </summary>
		ApplyAddGroup = 1,
		/// <summary>
		/// 机器人被邀请
		/// </summary>
		RobotBeInviteAddGroup = 2
	}
}
