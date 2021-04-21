using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q悬浮窗状态事件参数的类, 该类是抽象的
	/// </summary>
	public abstract class CQStatusEventArgs : CQEventArgs
	{
		#region --属性--
		/// <summary>
		/// 获取来源事件的ID. 是 id 字段
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// 获取来源事件的名称. 是 name 字段
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 获取来源事件的显示标题. 是 title 字段
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 获取来源事件的刷新间隔. 是 period 字段, 目前仅支持 1000ms (1秒)
		/// </summary>
		public TimeSpan Period { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQStatusEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="id">事件Id</param>
		/// <param name="name">名称</param>
		/// <param name="title">标题</param>
		/// <param name="function">函数名</param>
		/// <param name="period">刷新间隔</param>
		public CQStatusEventArgs (CQApi api, CQLog log, int id, string name, string title, string function, long period)
			: base (api, log, function)
		{
			this.Id = id;
			this.Name = name;
			this.Title = title;
			this.Period = new TimeSpan (period * TimeSpan.TicksPerMillisecond);
		}
		#endregion
	}
}
