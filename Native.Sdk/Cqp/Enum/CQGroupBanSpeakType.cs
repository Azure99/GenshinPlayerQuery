using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q 群禁言 事件的类型
	/// </summary>
	public enum CQGroupBanSpeakType
	{
		/// <summary>
		/// 解除禁言
		/// </summary>
		RemoveBanSpeak = 1,
		/// <summary>
		/// 设置禁言
		/// </summary>
		SetBanSpeak = 2
	}
}
