using Native.Sdk.Cqp.Expand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 表示描述 QQ群信息 的类
	/// </summary>
	public class GroupInfo : BasisStreamModel, IEquatable<GroupInfo>
	{
		#region --字段--
		private readonly bool _isGroupList = false;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取一个值, 指示当前QQ群 <see cref="Cqp.Model.Group"/> 对象
		/// </summary>
		public Group Group { get; private set; }
		/// <summary>
		/// 获取当前QQ群的名称
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// 获取一个值, 指示QQ群的当前人数; 当前属性仅在 <see cref="CQApi.GetGroupInfo(long, bool)"/> 中可用
		/// </summary>
		public int CurrentMemberCount { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前QQ群最大可容纳的人数; 当前属性仅在 <see cref="CQApi.GetGroupInfo(long, bool)"/> 中可用
		/// </summary>
		public int MaxMemberCount { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="BasisStreamModel"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		/// <param name="isGroup">是否解析群列表</param>
		public GroupInfo (CQApi api, string cipherText, bool isGroup)
			: base (api, cipherText)
		{
			this._isGroupList = isGroup;
		}
		/// <summary>
		/// 使用指定的密文初始化 <see cref="GroupInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherBytes">模型使用的解密密文字符串</param>
		/// <param name="isGroup">是否解析群列表</param>
		public GroupInfo (CQApi api, byte[] cipherBytes, bool isGroup)
			: base (api, cipherBytes)
		{
			this._isGroupList = isGroup;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Group.Equals (other.Group) && this.Name.Equals (other.Name) && this.CurrentMemberCount == other.CurrentMemberCount && this.MaxMemberCount == other.MaxMemberCount;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupInfo);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Group.GetHashCode () & this.Name.GetHashCode () & this.CurrentMemberCount.GetHashCode () & this.MaxMemberCount.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("群: {0}{1}", this.Group.Id, Environment.NewLine);
			builder.AppendFormat ("群名: {0}{1}", this.Name, Environment.NewLine);
			if (!this._isGroupList)
			{
				builder.AppendFormat ("人数: {0}/{1}{2}", this.CurrentMemberCount, this.MaxMemberCount, Environment.NewLine);
			}
			return builder.ToString ();
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

		#region --私有方法--
		/// <summary>
		/// 当在派生类中重写时, 进行当前模型初始化
		/// </summary>
		/// <param name="reader">解析模型的数据源</param>
		protected override void Initialize (BinaryReader reader)
		{
			if (reader.Length () < 10)
			{
				throw new InvalidDataException ("读取失败, 获取的原始数据长度小于 10");
			}

			this.Group = new Group (this.CQApi, reader.ReadInt64_Ex ());
			this.Name = reader.ReadString_Ex (CQApi.DefaultEncoding);
			if (!this._isGroupList)
			{
				this.CurrentMemberCount = reader.ReadInt32_Ex ();
				this.MaxMemberCount = reader.ReadInt32_Ex ();
			}
		}
		#endregion
	}
}
