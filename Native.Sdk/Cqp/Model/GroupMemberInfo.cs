using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Expand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 表示描述 QQ群成员 的类
	/// </summary>
	public class GroupMemberInfo : BasisStreamModel, IEquatable<GroupMemberInfo>
	{
		#region --属性--
		/// <summary>
		/// 获取一个值, 指示成员所在群的 <see cref="Cqp.Model.Group"/> 实例
		/// </summary>
		public Group Group { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前成员的QQ号的 <see cref="Cqp.Model.QQ"/> 实例
		/// </summary>
		public QQ QQ { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前成员的QQ昵称
		/// </summary>
		public string Nick { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前成员在此群的群名片
		/// </summary>
		public string Card { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员的性别
		/// </summary>
		public QQSex Sex { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员年龄
		/// </summary>
		public int Age { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前成员所在地区
		/// </summary>
		public string Area { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前成员加入群的日期和时间
		/// </summary>
		public DateTime JoinGroupDateTime { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员最后一次发言的日期和时间
		/// </summary>
		public DateTime LastSpeakDateTime { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员的等级
		/// </summary>
		public string Level { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前的群成员类型
		/// </summary>
		public QQGroupMemberType MemberType { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员在此群获得的专属头衔
		/// </summary>
		public string ExclusiveTitle { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员在此群的专属头衔过期时间, 若本属性为 null 则表示无期限
		/// </summary>
		public DateTime? ExclusiveTitleExpirationTime { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员是否为不良记录群成员
		/// </summary>
		public bool IsBadRecord { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前群成员是否允许修改群名片
		/// </summary>
		public bool IsAllowEditorCard { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="GroupMemberInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		public GroupMemberInfo (CQApi api, string cipherText)
			: base (api, cipherText)
		{
		}
		/// <summary>
		/// 使用指定的密文初始化 <see cref="GroupMemberInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherBytes">模型使用的解密密文字节数组</param>
		public GroupMemberInfo (CQApi api, byte[] cipherBytes)
			: base (api, cipherBytes)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupMemberInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Group.Equals (other.Group) && this.QQ.Equals (other.QQ) && this.Nick.Equals (other.Nick) && this.Card.Equals (other.Card) && this.Sex == other.Sex && this.Age == other.Age && this.Area.Equals (other.Area) && this.JoinGroupDateTime == other.JoinGroupDateTime && this.LastSpeakDateTime == other.LastSpeakDateTime && this.Level.Equals (other.Level) && this.MemberType == other.MemberType && this.ExclusiveTitle.Equals (other.ExclusiveTitle) && this.ExclusiveTitleExpirationTime == other.ExclusiveTitleExpirationTime && this.IsAllowEditorCard == other.IsAllowEditorCard && this.IsBadRecord == other.IsBadRecord;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupMemberInfo);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Group.GetHashCode () & this.QQ.GetHashCode () & this.Nick.GetHashCode () & this.Card.GetHashCode () & this.Sex.GetHashCode () & this.Age.GetHashCode () & this.Area.GetHashCode () & this.JoinGroupDateTime.GetHashCode () & this.LastSpeakDateTime.GetHashCode () & this.Level.GetHashCode () & this.MemberType.GetHashCode () & this.ExclusiveTitle.GetHashCode () & this.ExclusiveTitleExpirationTime.GetHashCode () & this.IsAllowEditorCard.GetHashCode () & this.IsBadRecord.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("群: {0}{1}", this.Group.Id, Environment.NewLine);
			builder.AppendFormat ("QQ: {0}{1}", this.QQ.Id, Environment.NewLine);
			builder.AppendFormat ("昵称: {0}{1}", this.Nick, Environment.NewLine);
			builder.AppendFormat ("名片: {0}{1}", this.Card, Environment.NewLine);
			builder.AppendFormat ("性别: {0}{1}", this.Sex.GetDescription (), Environment.NewLine);
			builder.AppendFormat ("地区: {0}{1}", this.Area, Environment.NewLine);
			builder.AppendFormat ("入群时间: {0}{1}", this.JoinGroupDateTime.ToString ("yyyy-MM-dd HH:mm:ss"), Environment.NewLine);
			builder.AppendFormat ("最后发言时间: {0}{1}", this.LastSpeakDateTime.ToString ("yyyy-MM-dd HH:mm:ss"), Environment.NewLine);
			builder.AppendFormat ("成员等级: {0}{1}", this.Level, Environment.NewLine);
			builder.AppendFormat ("成员类型: {0}{1}", this.MemberType.GetDescription (), Environment.NewLine);
			builder.AppendFormat ("专属头衔: {0}{1}", this.ExclusiveTitle, Environment.NewLine);
			builder.AppendFormat ("专属头衔过期时间: {0}{1}", this.ExclusiveTitleExpirationTime != null ? this.ExclusiveTitleExpirationTime.Value.ToString ("yyyy-MM-dd HH:mm:ss") : "永久", Environment.NewLine);
			builder.AppendFormat ("不良记录成员: {0}{1}", this.IsBadRecord ? "是" : "否", Environment.NewLine);
			builder.AppendFormat ("允许修改名片: {0}{1}", this.IsAllowEditorCard ? "是" : "否", Environment.NewLine);
			return builder.ToString ();
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

		#region --私有方法--
		/// <summary>
		/// 当在派生类中重写时, 进行当前模型初始化
		/// </summary>
		/// <param name="reader">解析模型的数据源</param>
		protected override void Initialize (BinaryReader reader)
		{
			if (reader.Length () < 40)
			{
				throw new InvalidDataException ("读取失败, 获取的原始数据长度小于 40");
			}

			this.Group = new Group (this.CQApi, reader.ReadInt64_Ex ());
			this.QQ = new QQ (this.CQApi, reader.ReadInt64_Ex ());
			this.Nick = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.Card = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.Sex = (QQSex)reader.ReadInt32_Ex ();
			this.Age = reader.ReadInt32_Ex ();
			this.Area = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.JoinGroupDateTime = reader.ReadInt32_Ex ().ToDateTime ();
			this.LastSpeakDateTime = reader.ReadInt32_Ex ().ToDateTime ();
			this.Level = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.MemberType = (QQGroupMemberType)reader.ReadInt32_Ex ();
			this.IsBadRecord = reader.ReadInt32_Ex () == 1;
			this.ExclusiveTitle = reader.ReadString_Ex (CQApi.DefaultEncoding);
			int expTime = reader.ReadInt32_Ex ();
			if (expTime == -1)
			{
				this.ExclusiveTitleExpirationTime = null;
			}
			else
			{
				this.ExclusiveTitleExpirationTime = expTime.ToDateTime ();
			}
		}
		#endregion
	}
}
