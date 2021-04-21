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
	/// 表示描述 群文件信息 的类
	/// </summary>
	public class GroupFileInfo : BasisStreamModel, IEquatable<GroupFileInfo>
	{
		#region --属性--
		/// <summary>
		/// 获取一个值, 指示当前文件的 Busid (唯一标识符)
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前文件的名称
		/// </summary>
		public string FileName { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前文件的Id
		/// </summary>
		public string FileId { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前文件的大小
		/// </summary>
		public long FileSize { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="BasisStreamModel"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		public GroupFileInfo (CQApi api, string cipherText)
			: base (api, cipherText)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupFileInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Id == other.Id && this.FileName.Equals (other.FileName) && this.FileId.Equals (other.FileId) && this.FileSize == other.FileSize;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupFileInfo);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & this.FileName.GetHashCode () & this.FileId.GetHashCode () & this.FileSize.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("BusId: {0}{1}", this.Id, Environment.NewLine);
			builder.AppendFormat ("文件名: {0}{1}", this.FileName, Environment.NewLine);
			builder.AppendFormat ("文件ID: {0}{1}", this.FileId, Environment.NewLine);
			builder.AppendFormat ("文件大小: {0}{1}", this.FileSize, Environment.NewLine);
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
			if (reader.Length () < 20)
			{
				throw new InvalidDataException ("读取失败, 获取的原始数据长度小于 20");
			}

			this.FileId = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.FileName = reader.ReadString_Ex (CQApi.DefaultEncoding);
			this.FileSize = reader.ReadInt64_Ex ();
			this.Id = (int)reader.ReadInt64_Ex ();
		}
		#endregion
	}
}
