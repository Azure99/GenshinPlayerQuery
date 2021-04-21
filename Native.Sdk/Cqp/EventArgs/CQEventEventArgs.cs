using Native.Sdk.Cqp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q事件类事件参数的基础类, 该类是抽象的
	/// </summary>
	public abstract class CQEventEventArgs : CQEventArgs
	{
		#region --属性--
		/// <summary>
		/// 获取来源事件的事件ID. 是 id 字段
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// 获取来源事件的事件类型. 是 type 字段
		/// </summary>
		public CQMessageEventType Type { get; private set; }

		/// <summary>
		/// 获取来源事件的事件名称. 是 name 字段
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 获取来源事件的默认优先级. 是 priority 字段, 该值不会随着酷Q的调整而变动
		/// </summary>
		public uint Priority { get; private set; }

		/// <summary>
		/// 获取或设置一个值, 指示该事件是否已经处理
		/// </summary>
		public bool Handler { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQEventEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="id">事件id</param>
		/// <param name="type">类型</param>
		/// <param name="name">名称</param>
		/// <param name="function">函数名</param>
		/// <param name="priority">默认优先级</param>
		public CQEventEventArgs (CQApi api, CQLog log, int id, int type, string name, string function, uint priority)
			: base (api, log, function)
		{
			this.Id = id;
			this.Type = (CQMessageEventType)type;
			this.Name = name;
			this.Priority = priority;
			this.Handler = false;
		}
		#endregion
	}
}
