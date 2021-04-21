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
	/// 描述 QQ群 的类
	/// </summary>
	public sealed class Group : BasisModel, IEquatable<Group>
	{
		#region --常量--
		/// <summary>
		/// 表示 <see cref="Group"/> 的最小值, 此字段为常数.
		/// </summary>
		public const long MinValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一ID (QQ群号)
		/// </summary>
		public long Id { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Group"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="groupId">模型所托管QQ群号的基础值</param>
		/// <exception cref="ArgumentNullException">参数: api 是 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">QQ群号超出范围</exception>
		public Group (CQApi api, long groupId)
			: base (api)
		{
			if (groupId < Group.MinValue)
			{
				throw new ArgumentOutOfRangeException ("groupId");
			}

			this.Id = groupId;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 发送群消息
		/// </summary>
		/// <param name="message">消息内容, 获取内容时将调用<see cref="object.ToString"/>进行消息转换</param>
		/// <returns>发送成功将返回 <see cref="QQMessage"/> 对象</returns>
		public QQMessage SendGroupMessage (params object[] message)
		{
			return this.CQApi.SendGroupMessage (this, message);
		}
		/// <summary>
		/// 获取群信息
		/// </summary>
		/// <param name="notCache">不使用缓存, 通常为 <code>false</code>, 仅在必要时使用</param>
		/// <returns>获取成功返回 <see cref="GroupInfo"/> 对象</returns>
		public GroupInfo GetGroupInfo (bool notCache = false)
		{
			return this.CQApi.GetGroupInfo (this, notCache);
		}
		/// <summary>
		/// 获取群成员列表
		/// </summary>
		/// <returns>获取成功返回 <see cref="GroupMemberInfo"/> 数组</returns>
		public GroupMemberInfoCollection GetGroupMemberList ()
		{
			return this.CQApi.GetGroupMemberList (this);
		}
		/// <summary>
		/// 获取群成员信息
		/// </summary>
		/// <param name="qqId">目标帐号</param>
		/// <param name="notCache">不使用缓存, 默认为 <code>false</code>, 通常忽略本参数, 仅在必要时使用</param>
		/// <returns>获取成功返回 <see cref="GroupMemberInfo"/></returns>
		public GroupMemberInfo GetGroupMemberInfo (long qqId, bool notCache = false)
		{
			return this.CQApi.GetGroupMemberInfo (this, qqId, notCache);
		}
		/// <summary>
		/// 设置群匿名成员禁言
		/// </summary>
		/// <param name="anonymous">目标群成员匿名信息</param>
		/// <param name="time">禁言的时长 (范围: 1秒 ~ 30天)</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupAnonymousMemberBanSpeak (GroupMemberAnonymousInfo anonymous, TimeSpan time)
		{
			return this.CQApi.SetGroupAnonymousMemberBanSpeak (this, anonymous, time);
		}
		/// <summary>
		/// 设置群成员禁言
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <param name="time">禁言时长 (范围: 1秒 ~ 30天)</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupMemberBanSpeak (long qqId, TimeSpan time)
		{
			return this.CQApi.SetGroupMemberBanSpeak (this, qqId, time);
		}
		/// <summary>
		/// 解除群成员禁言
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool RemoveGroupMemberBanSpeak (long qqId)
		{
			return this.CQApi.RemoveGroupMemberBanSpeak (this, qqId);
		}
		/// <summary>
		/// 设置群全体禁言
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupBanSpeak ()
		{
			return this.CQApi.SetGroupBanSpeak (this);
		}
		/// <summary>
		/// 解除群全体禁言
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool RemoveGroupBanSpeak ()
		{
			return this.CQApi.RemoveGroupBanSpeak (this);
		}
		/// <summary>
		/// 设置群成员名片
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <param name="newName">新名称</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupMemberVisitingCard (long qqId, string newName)
		{
			return this.CQApi.SetGroupMemberVisitingCard (this, qqId, newName);
		}
		/// <summary>
		/// 设置群成员专属头衔, 并指定其过期的时间
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <param name="newTitle">新头衔</param>
		/// <param name="time">过期时间</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupMemberExclusiveTitle (long qqId, string newTitle, TimeSpan time)
		{
			return this.CQApi.SetGroupMemberExclusiveTitle (this, qqId, newTitle, time);
		}
		/// <summary>
		/// 设置群成员永久专属头衔
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <param name="newTitle">新头衔</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupMemberForeverExclusiveTitle (long qqId, string newTitle)
		{
			return this.CQApi.SetGroupMemberForeverExclusiveTitle (this, qqId, newTitle);
		}
		/// <summary>
		/// 设置群管理员
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool SetGroupManage (long qqId)
		{
			return this.CQApi.SetGroupManage (this, qqId);
		}
		/// <summary>
		/// 解除群管理员
		/// </summary>
		/// <param name="qqId">目标QQ</param>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool RemoveGroupManage (long qqId)
		{
			return this.CQApi.RemoveGroupManage (this, qqId);
		}
		/// <summary>
		/// 开启群匿名
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool OpenGroupAnonymous ()
		{
			return this.CQApi.OpenGroupAnonymous (this);
		}
		/// <summary>
		/// 关闭群匿名
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool StopGroupAnonymous ()
		{
			return this.CQApi.StopGroupAnonymous (this);
		}
		/// <summary>
		/// 退出群. 慎用, 此接口需要严格授权
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool ExitGroup ()
		{
			return this.CQApi.ExitGroup (this);
		}
		/// <summary>
		/// 解散群. 慎用, 此接口需要严格授权
		/// </summary>
		/// <returns>操作成功返回 <see langword="true"/>, 否则返回 <see langword="false"/></returns>
		public bool DissolutionGroup ()
		{
			return this.CQApi.DissolutionGroup (this);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (Group other)
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
			return this.Equals (obj as Group);
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
		/// 定义将 <see cref="Group"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="Group"/> 对象</param>
		public static implicit operator long (Group value)
		{
			return value.Id;
		}
		/// <summary>
		/// 定义将 <see cref="Group"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="Group"/> 对象</param>
		public static implicit operator string (Group value)
		{
			return value.ToString ();
		}
		#endregion
	}
}
