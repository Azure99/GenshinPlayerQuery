using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q私聊消息类型
	/// </summary>
	public enum CQPrviateMessageType
	{
		/// <summary>
		/// 好友
		/// </summary>
		Friend = 11,
		/// <summary>
		/// 在线状态
		/// </summary>
		OnlineStatus = 1,
		/// <summary>
		/// 群
		/// </summary>
		Group = 2,
		/// <summary>
		/// 讨论组
		/// </summary>
		Discuss = 3
	}
}
