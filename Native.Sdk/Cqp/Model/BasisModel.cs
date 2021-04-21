using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 描述酷Q数据模型的基础类, 该类是抽象的
	/// </summary>
	public abstract class BasisModel : IToSendString
	{
		#region --属性--
		/// <summary>
		/// 获取当前模型持有用于扩展自身的 <see cref="Cqp.CQApi"/> 对象
		/// </summary>
		public CQApi CQApi { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的 <see cref="Cqp.CQApi"/> 初始化当前类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <exception cref="ArgumentNullException">参数: api 为 null</exception>
		public BasisModel (CQApi api)
		{
			if (api == null)
			{
				throw new ArgumentNullException ("api");
			}

			this.CQApi = api;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 当在派生类中重写时, 处理返回用于发送的字符串
		/// </summary>
		/// <returns>用于发送的字符串</returns>
		public abstract string ToSendString (); 
		#endregion
	}
}
