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
	/// 表示描述 QQ群成员匿名信息 的类
	/// </summary>
	public class GroupMemberAnonymousInfo : BasisStreamModel, IEquatable<GroupMemberAnonymousInfo>
	{
		#region --属性--
		/// <summary>
		/// 获取一个值, 指示该成员的群匿名标识
		/// </summary>
		public long Id { get; private set; }
		/// <summary>
		/// 获取一个值, 作为该成员在群中的匿名称号
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// 获取当前匿名成员的执行令牌, 用于对该成员做出一些操作. 如: 禁言
		/// </summary>
		public byte[] Token { get; private set; }
		/// <summary>
		/// 获取当前匿名成员的原始字符串, 这个字符串是构造函数中传入的
		/// </summary>
		public string OriginalString { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="GroupMemberAnonymousInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		public GroupMemberAnonymousInfo (CQApi api, string cipherText)
			: base (api, cipherText)
		{
			this.OriginalString = cipherText;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupMemberAnonymousInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return this.OriginalString.Equals (other.OriginalString);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupMemberAnonymousInfo);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.OriginalString.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("ID: {0}{1}", this.Id, Environment.NewLine);
			builder.AppendFormat ("称号: {0}{1}", this.Name, Environment.NewLine);
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
			if (reader.Length () < 12)
			{

				throw new InvalidDataException ("读取失败, 获取的原始数据长度小于 12");
			}

			this.Id = reader.ReadInt64_Ex ();
			this.Name = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.Token = reader.ReadToken_Ex ();
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="GroupMemberAnonymousInfo"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="GroupMemberAnonymousInfo"/> 对象</param>
		public static implicit operator string (GroupMemberAnonymousInfo value)
		{
			return value.OriginalString;
		}
		#endregion
	}
}
