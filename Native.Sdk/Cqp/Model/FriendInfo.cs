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
	/// 描述 QQ好友信息 的类
	/// </summary>
	public class FriendInfo : BasisStreamModel, IEquatable<FriendInfo>
	{
		#region --属性--
		/// <summary>
		/// 获取一个值, 指示当前账号的 <see cref="Cqp.Model.QQ"/> 实例
		/// </summary>
		public QQ QQ { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前的QQ昵称
		/// </summary>
		public string Nick { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前的备注信息
		/// </summary>
		public string Postscript { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="FriendInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		public FriendInfo (CQApi api, string cipherText)
			: base (api, cipherText)
		{
		}
		/// <summary>
		/// 使用指定的密文初始化 <see cref="FriendInfo"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherBytes">模型使用的解密密文字节数组</param>
		public FriendInfo (CQApi api, byte[] cipherBytes) 
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
		public bool Equals (FriendInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return this.QQ.Equals (other) && this.Nick.Equals (other.Nick) && this.Postscript.Equals (other.Postscript);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as FriendInfo);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.QQ.GetHashCode () & this.Nick.GetHashCode () & this.Postscript.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("QQ: {0}{1}", this.QQ.Id, Environment.NewLine);
			builder.AppendFormat ("昵称: {0}{1}", this.Nick, Environment.NewLine);
			builder.AppendFormat ("备注: {0}{1}", this.Postscript, Environment.NewLine);
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

			this.QQ = new QQ (this.CQApi, reader.ReadInt64_Ex ());
			this.Nick = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.Postscript = reader.ReadString_Ex (CQApi.DefaultEncoding);
		}
		#endregion
	}
}
