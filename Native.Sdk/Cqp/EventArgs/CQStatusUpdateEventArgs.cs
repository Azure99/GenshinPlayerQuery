using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q悬浮窗状态更新事件参数的类
	/// </summary>
	public sealed class CQStatusUpdateEventArgs : CQStatusEventArgs
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQStatusUpdateEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="id">悬浮窗id</param>
		/// <param name="name">名称</param>
		/// <param name="title">英文名称</param>
		/// <param name="function">函数名称</param>
		/// <param name="period">更新间隔</param>
		public CQStatusUpdateEventArgs (CQApi api, CQLog log, int id, string name, string title, string function, long period)
			: base (api, log, id, name, title, function, period)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendLine (string.Format ("ID: {0}", this.Id));
			builder.AppendLine (string.Format ("名称: {0}", this.Name));
			builder.AppendLine (string.Format ("标题: {0}", this.Title));
			builder.AppendLine (string.Format ("函数: {0}", this.Function));
			builder.AppendFormat ("刷新间隔: {0}", this.Period);
			return builder.ToString ();
		}
		#endregion
	}
}
