using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q启动事件参数的类
	/// <para/>
	/// Type: 1001
	/// </summary>
	public sealed class CQStartupEventArgs : CQEventEventArgs
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQStartupEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="id">事件ID</param>
		/// <param name="type">类型</param>
		/// <param name="name">名称</param>
		/// <param name="function">函数名称</param>
		/// <param name="priority">默认优先级</param>
		public CQStartupEventArgs (CQApi api, CQLog log, int id, int type, string name, string function, uint priority)
			: base (api, log, id, type, name, function, priority)
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
			builder.AppendLine (string.Format ("类型: {0}({1})", this.Type, (int)this.Type));
			builder.AppendLine (string.Format ("名称: {0}", this.Name));
			builder.AppendLine (string.Format ("函数: {0}", this.Function));
			builder.AppendFormat ("优先级: {0}", this.Priority);
			return builder.ToString ();
		}
		#endregion
	}
}
