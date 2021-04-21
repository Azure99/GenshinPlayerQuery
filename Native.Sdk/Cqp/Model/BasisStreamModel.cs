using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 描述酷Q需要解析数据流模型的基础类, 该类是抽象的
	/// </summary>
	public abstract class BasisStreamModel : BasisModel, IToSendString
	{
		#region --构造函数--
		/// <summary>
		/// 使用指定的密文初始化 <see cref="BasisStreamModel"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherText">模型使用的解密密文字符串</param>
		/// <exception cref="ArgumentNullException">参数: api 或 cipherText 为 null</exception>
		/// <exception cref="FormatException">cipherText 的长度（忽略空格）不是 0 或 4 的倍数。 或 cipherText 的格式无效。 cipherText 包含非 base 64 字符、两个以上的填充字符或者在填充字符中包含非空格字符</exception>
		public BasisStreamModel (CQApi api, string cipherText)
			: this (api, Convert.FromBase64String (cipherText))
		{

		}
		/// <summary>
		/// 使用指定的密文初始化 <see cref="BasisStreamModel"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="cipherBytes">模型使用的解密密文字节数组</param>
		/// <exception cref="ArgumentNullException">参数: api 或 cipherBytes 为 null</exception>
		public BasisStreamModel (CQApi api, byte[] cipherBytes)
			: base (api)
		{
			using (BinaryReader reader = new BinaryReader (new MemoryStream (cipherBytes)))
			{
				this.Initialize (reader);
			}
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 当在派生类中重写时, 进行当前模型初始化
		/// </summary>
		/// <param name="reader">解析模型的数据源</param>
		protected abstract void Initialize (BinaryReader reader);
		#endregion
	}
}
