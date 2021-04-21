using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;

namespace Native.Sdk.Cqp.Interface
{
	/// <summary>
	/// 酷Q群上传事件接口
	/// <para/>
	/// Type: 11
	/// </summary>
	public interface IGroupUpload
	{
		/// <summary>
		/// 当在派生类中重写时, 处理 酷Q群上传事件 回调
		/// </summary>
		/// <param name="sender">事件来源对象</param>
		/// <param name="e">附加的事件参数</param>
		void GroupUpload (object sender, CQGroupUploadEventArgs e);
	}
}
