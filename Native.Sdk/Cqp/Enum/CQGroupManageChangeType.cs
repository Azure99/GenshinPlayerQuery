using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q 群管理改变 的类型
	/// </summary>
	public enum CQGroupManageChangeType
	{
		/// <summary>
		/// 被取消管理
		/// </summary>
		RemoveManage = 1,
		/// <summary>
		/// 被设置管理
		/// </summary>
		SetManage = 2
	}
}
