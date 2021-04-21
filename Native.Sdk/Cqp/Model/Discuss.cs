using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 描述 讨论组 的类
	/// </summary>
	public class Discuss : BasisModel, IEquatable<Discuss>
	{
		#region --常量--
		/// <summary>
		/// 表示 <see cref="Discuss"/> 的最小值, 此字段为常数.
		/// </summary>
		public const long MinValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一ID (讨论组号)
		/// </summary>
		public long Id { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Discuss"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="discussId">模型所托管讨论组号的基础值</param>
		/// <exception cref="ArgumentNullException">参数: api 是 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">讨论组号超出范围</exception>
		public Discuss (CQApi api, long discussId)
			: base (api)
		{
			if (discussId < Discuss.MinValue)
			{
				throw new ArgumentOutOfRangeException ("discussId");
			}

			this.Id = discussId;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 发送讨论组消息
		/// </summary>
		/// <param name="message">消息内容, 获取内容时将调用<see cref="object.ToString"/>进行消息转换</param>
		/// <returns>发送成功将返回消息 Id, 发送失败将返回负值</returns>
		/// <returns>发送成功将返回 <see cref="QQMessage"/> 对象</returns>
		public QQMessage SendDiscussMessage (params object[] message)
		{
			return this.CQApi.SendDiscussMessage (this, message);
		}
		/// <summary>
		/// 退出讨论组.
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool ExitDiscuss ()
		{
			return this.CQApi.ExitDiscuss (this);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (Discuss other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Id == other.Id;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as Discuss);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			return this.Id.ToString ();
		}
		/// <summary>
		/// 当在派生类中重写时, 处理返回用于发送的字符串
		/// </summary>
		/// <returns>用于发送的字符串</returns>
		public override string ToSendString ()
		{
			return this.ToString ();
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="Discuss"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="Discuss"/> 对象</param>
		public static implicit operator long (Discuss value)
		{
			return value.Id;
		}
		/// <summary>
		/// 定义将 <see cref="Discuss"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="Discuss"/> 对象</param>
		public static implicit operator string (Discuss value)
		{
			return value.ToString ();
		}
		#endregion
	}
}
