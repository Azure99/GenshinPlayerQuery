using Native.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Interface
{
	/// <summary>
	/// 酷Q应用启用接口
	/// <para/>
	/// Type: 1003
	/// </summary>
	public interface IAppEnable
	{
		/// <summary>
		/// 当在派生类中重写时, 处理 酷Q应用启用 回调
		/// </summary>
		/// <param name="sender">事件来源对象</param>
		/// <param name="e">附加的事件参数</param>
		void AppEnable (object sender, CQAppEnableEventArgs e);
	}
}
