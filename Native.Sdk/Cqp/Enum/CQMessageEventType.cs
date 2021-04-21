using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q消息类事件的事件类型
	/// </summary>
	public enum CQMessageEventType
	{
		/// <summary>
		/// 私聊消息
		/// </summary>
		PrivateMessage = 21,
		/// <summary>
		/// 群消息
		/// </summary>
		GroupMessage = 2,
		/// <summary>
		/// 讨论组消息
		/// </summary>
		DiscussMessage = 4,
		/// <summary>
		/// 群文件上传
		/// </summary>
		GroupFileUpload = 11,
		/// <summary>
		/// 群管理变动
		/// </summary>
		GroupManageChange = 101,
		/// <summary>
		/// 群成员减少
		/// </summary>
		GroupMemberDecrease = 102,
		/// <summary>
		/// 群成员增加
		/// </summary>
		GroupMemberIncrease = 103,
		/// <summary>
		/// 群成员禁言
		/// </summary>
		GroupMemberBanSpeak = 104,
		/// <summary>
		/// 好友添加
		/// </summary>
		FriendAdd = 201,
		/// <summary>
		/// 好友添加请求
		/// </summary>
		FriendAddRequest = 301,
		/// <summary>
		/// 群添加请求
		/// </summary>
		GroupAddRequest = 302,
		/// <summary>
		/// 酷Q启动
		/// </summary>
		CQStartup = 1001,
		/// <summary>
		/// 酷Q退出
		/// </summary>
		CQExit = 1002,
		/// <summary>
		/// 酷Q应用被启用
		/// </summary>
		CQAppEnable = 1003,
		/// <summary>
		/// 酷Q应用被禁用
		/// </summary>
		CQAppDisable = 1004
	}
}
