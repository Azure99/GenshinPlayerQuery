using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Expand;
using Native.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.EventArgs
{
	/// <summary>
	/// 提供用于描述酷Q群禁言事件参数的类
	/// <para/>
	/// Type: 104
	/// </summary>
	public sealed class CQGroupBanSpeakEventArgs : CQEventEventArgs
	{
		#region --属性--
		/// <summary>
		/// 获取当前事件的子类型
		/// </summary>
		public CQGroupBanSpeakType SubType { get; private set; }

		/// <summary>
		/// 获取当前事件的发送时间
		/// </summary>
		public DateTime SendTime { get; private set; }

		/// <summary>
		/// 获取当前事件的来源群
		/// </summary>
		public Group FromGroup { get; private set; }

		/// <summary>
		/// 获取当前事件的来源QQ
		/// </summary>
		public QQ FromQQ { get; private set; }

		/// <summary>
		/// 获取一个值, 指示当前事件是否为全体禁言
		/// </summary>
		public bool IsAllBanSpeak { get; private set; }

		/// <summary>
		/// 获取当前事件的被操作QQ, 若 <see cref="IsAllBanSpeak"/> 是 <code>false</code> 则为 null
		/// </summary>
		public QQ BeingOperateQQ { get; private set; }

		/// <summary>
		/// 获取当前事件的禁言时长, 此值仅在  <see cref="SubType"/> 是 <see cref="CQGroupBanSpeakType.SetBanSpeak"/> 时可用
		/// </summary>
		public TimeSpan? BanSpeakTimeSpan { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQGroupBanSpeakEventArgs"/> 类的新实例
		/// </summary>
		/// <param name="api">酷Q的接口实例</param>
		/// <param name="log">酷Q的日志实例</param>
		/// <param name="id">事件Id</param>
		/// <param name="type">事件类型</param>
		/// <param name="name">事件名称</param>
		/// <param name="function">函数名称</param>
		/// <param name="priority">默认优先级</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="beingOperateQQ">被操作QQ</param>
		/// <param name="duration">禁言时长, 单位: 秒</param>
		public CQGroupBanSpeakEventArgs (CQApi api, CQLog log, int id, int type, string name, string function, uint priority, int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ, long duration)
			: base (api, log, id, type, name, function, priority)
		{
			this.SubType = (CQGroupBanSpeakType)subType;
			this.SendTime = sendTime.ToDateTime ();
			this.FromGroup = new Group (api, fromGroup);
			this.FromQQ = new QQ (api, fromQQ);
			this.IsAllBanSpeak = beingOperateQQ == 0;
			if (!this.IsAllBanSpeak)
			{
				this.BeingOperateQQ = new QQ (api, beingOperateQQ);
			}

			if (subType == 2)   // 当子类型为 2 时才有
			{
				this.BanSpeakTimeSpan = TimeSpan.FromSeconds (duration);
			}
		}
		#endregion

		#region --公开函数--
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
			builder.AppendLine (string.Format ("优先级: {0}", this.Priority));
			builder.AppendLine (string.Format ("子类型: {0}({1})", this.SubType, (int)this.SubType));
			builder.AppendLine (string.Format ("发送时间: {0}", this.SendTime));
			builder.AppendLine (string.Format ("群号: {0}", this.FromGroup != null ? this.FromGroup.Id.ToString () : string.Empty));
			builder.AppendLine (string.Format ("操作者账号: {0}", this.FromQQ != null ? this.FromQQ.Id.ToString () : string.Empty));
			builder.AppendFormat ("操作全体: {0}", this.IsAllBanSpeak);
			if (!this.IsAllBanSpeak)
			{
				builder.AppendLine ();
				builder.AppendLine (string.Format ("被操作账号: {0}", this.BeingOperateQQ != null ? this.BeingOperateQQ.Id.ToString () : string.Empty));
				if (SubType == CQGroupBanSpeakType.SetBanSpeak)
				{
					builder.AppendFormat ("时常: {0}", this.BanSpeakTimeSpan);
				}
			}
			return builder.ToString ();
		}
		#endregion
	}
}
