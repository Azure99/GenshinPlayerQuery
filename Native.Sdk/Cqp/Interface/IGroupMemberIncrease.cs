﻿using Native.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Interface
{
	/// <summary>
	/// 酷Q群成员增加事件接口
	/// <para/>
	/// Type: 103
	/// </summary>
	public interface IGroupMemberIncrease
	{
		/// <summary>
		/// 当在派生类中重写时, 处理 酷Q群成员增加事件 回调
		/// </summary>
		/// <param name="sender">事件来源对象</param>
		/// <param name="e">附加的事件参数</param>
		void GroupMemberIncrease (object sender, CQGroupMemberIncreaseEventArgs e);
	}
}
