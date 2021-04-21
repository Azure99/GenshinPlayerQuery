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
	/// 描述 QQ 的类
	/// </summary>
	public sealed class QQ : BasisModel, IEquatable<QQ>
	{
		#region --常量--
		/// <summary>
		/// 表示 <see cref="QQ"/> 的最小值, 此字段为常数
		/// </summary>
		public const long MinValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一ID (QQ号)
		/// </summary>
		public long Id { get; private set; }
		/// <summary>
		/// 判断是否是登录QQ (机器人QQ)
		/// </summary>
		public bool IsLoginQQ
		{
			get { return this.CQApi.GetLoginQQ () == this.Id; }
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="QQ"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="qqId">模型所托管QQ号的基础值</param>
		/// <exception cref="ArgumentNullException">参数: api 是 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">QQ号超出范围</exception>
		public QQ (CQApi api, long qqId)
			: base (api)
		{
			if (qqId < QQ.MinValue)
			{
				throw new ArgumentOutOfRangeException ("qqId");
			}

			this.Id = qqId;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 发送私聊消息
		/// </summary>
		/// <param name="message">消息内容, 获取内容时将调用<see cref="object.ToString"/>进行消息转换</param>
		/// <returns>发送成功将返回 <see cref="QQMessage"/> 对象</returns>
		public QQMessage SendPrivateMessage (params object[] message)
		{
			return this.CQApi.SendPrivateMessage (this, message);
		}
		/// <summary>
		/// 发送赞
		/// </summary>
		/// <param name="count">发送赞的次数, 范围: 1~10 (留空为1次)</param>
		/// <returns>执行成功返回 <code>true</code>, 失败返回 <code>false</code></returns>
		public bool SendPraise (int count = 1)
		{
			return this.CQApi.SendPraise (this, count);
		}
		/// <summary>
		/// 获取陌生人信息
		/// </summary>
		/// <param name="notCache">不使用缓存, 默认为 <code>false</code>, 通常忽略本参数, 仅在必要时使用</param>
		/// <returns>获取成功返回 <see cref="StrangerInfo"/></returns>
		public StrangerInfo GetStrangerInfo (bool notCache = false)
		{
			return this.CQApi.GetStrangerInfo (this, notCache);
		}
		/// <summary>
		/// 获取酷Q "At某人" 代码
		/// </summary>
		/// <returns>返回 <see cref="CQCode"/> 对象</returns>
		public CQCode CQCode_At ()
		{
			return CQApi.CQCode_At ((long)this);
		}
		/// <summary>
		/// 获取酷Q "好友名片分享" 代码
		/// </summary>
		/// <returns>返回 <see cref="CQCode"/> 对象</returns>
		public CQCode CQCode_ShareFriendCard ()
		{
			return CQApi.CQCode_ShareFriendCard ((long)this);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (QQ other)
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
			return this.Equals (obj as QQ);
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
		/// 返回用于发送的字符串
		/// </summary>
		/// <returns>用于发送的字符串</returns>
		public override string ToSendString ()
		{
			return this.ToString ();
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="QQ"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="QQ"/> 对象</param>
		public static implicit operator long (QQ value)
		{
			return value.Id;
		}
		/// <summary>
		/// 定义将 <see cref="QQ"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="QQ"/> 对象</param>
		public static implicit operator string (QQ value)
		{
			return value.ToString ();
		}
		#endregion
	}
}
