using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q事件参数的基础类, 该类是抽象的
	/// </summary>
	public abstract class CQEventArgs
	{
		#region --属性--
		/// <summary>
		/// 获取当前事件的 <see cref="Cqp.CQApi"/> 实例.
		/// </summary>
		public CQApi CQApi { get; private set; }

		/// <summary>
		/// 获取当前事件的 <see cref="Cqp.CQLog"/> 实例.
		/// </summary>
		public CQLog CQLog { get; private set; }

		/// <summary>
		/// 获取当前事件的回调函数名称. 是 function 字段
		/// </summary>
		public string Function { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="function">触发此事件的函数名称</param>
		public CQEventArgs (CQApi api, CQLog log, string function)
		{
			this.CQApi = api;
			this.CQLog = log;
			this.Function = function;
		}
		#endregion
	}
}
