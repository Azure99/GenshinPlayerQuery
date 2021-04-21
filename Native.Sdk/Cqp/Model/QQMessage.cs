using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Expand;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 描述 QQ消息 的类
	/// </summary>
	public class QQMessage : BasisStreamModel, IEquatable<QQMessage>
	{
		#region --属性--
		/// <summary>
		/// 获取当前消息的全局唯一标识
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前消息是否发送成功
		/// </summary>
		public bool IsSuccess { get { return this.Id >= 0; } }
		/// <summary>
		/// 获取当前消息的原文
		/// </summary>
		public string Text { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前消息是否为正则消息
		/// </summary>
		public bool IsRegexMessage { get; private set; }
		/// <summary>
		/// 获取当前实例正则消息的解析结果
		/// </summary>
		public Dictionary<string, string> RegexResult { get; private set; }
		/// <summary>
		/// 获取当前消息中所有 [CQ:...] 的对象集合
		/// </summary>
		public List<CQCode> CQCodes { get; private set; }
		/// <summary>
		/// 获取一个值, 指示当前消息是否属于纯图片消息
		/// </summary>
		public bool IsImageMessage
		{
			get
			{
				if (this.CQCodes.Count == 0)
				{
					return false;
				}
				return this.CQCodes.All (CQCode.EqualIsImageCQCode);
			}
		}
		/// <summary>
		/// 获取一个值, 指示当前消息是否属于纯语音消息
		/// </summary>
		public bool IsRecordMessage
		{
			get
			{
				if (this.CQCodes.Count == 0)
				{
					return false;
				}

				return this.CQCodes.All (CQCode.EqualIsRecordCQCode);
			}
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用 <see cref="CQApi"/> 和相关消息来初始化 <see cref="QQMessage"/> 类的新实例
		/// </summary>
		/// <param name="api">模型使用的 <see cref="Cqp.CQApi"/></param>
		/// <param name="msgId">模型使用的消息Id</param>
		/// <param name="msg">模型使用的消息原文</param>
		/// <param name="isRegexMsg">指示当前实例是否是正则消息</param>
		public QQMessage (CQApi api, int msgId, string msg, bool isRegexMsg = false)
			: base (api, (isRegexMsg == true) ? msg : string.Empty)
		{
			if (msg == null)
			{
				throw new ArgumentNullException ("msg");
			}

			this.Id = msgId;
			this.Text = msg;
			this.IsRegexMessage = isRegexMsg;
			if (!this.IsRegexMessage)
			{
				this.CQCodes = CQCode.Parse (this.Text);
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 撤回消息
		/// </summary>
		/// <returns>撤回成功返回 <see langword="true"/>, 失败返回 <see langword="false"/></returns>
		public bool RemoveMessage ()
		{
			return this.CQApi.RemoveMessage (this.Id);
		}
		/// <summary>
		/// 接收消息中的语音 (消息含有CQ码 "record" 的消息)
		/// </summary>
		/// <param name="format">所需的目标语音的音频格式</param>
		/// <exception cref="ArithmeticException">当前实例属于正则消息时 (仅 Debug 生效)</exception>
		/// <returns>返回语音文件位于本地服务器的绝对路径</returns>
		public string ReceiveRecord (CQAudioFormat format)
		{
			if (this.IsRegexMessage)
			{
#if DEBUG
				throw new ArithmeticException ("无法解析原始消息. 原因: 当前消息属于正则消息");
#endif
			}

			if (this.IsRecordMessage)
			{
				return this.CQApi.ReceiveRecord (this.CQCodes[0].Items["file"], format);
			}

			return null;
		}
		/// <summary>
		/// 接收消息中指定的图片 (消息含有CQ码 "image" 的消息)
		/// </summary>
		/// <param name="index">要接收的图片索引, 该索引从 0 开始</param>
		/// <exception cref="ArgumentOutOfRangeException">index 小于 0。 - 或 - index 等于或大于 <see cref="QQMessage.CQCodes"/> 包含 <see cref="CQFunction.Image"/> 的数量</exception>
		/// <exception cref="ArithmeticException">当前实例属于正则消息时 (仅 Debug 生效)</exception>
		/// <returns>返回图片文件位于本地服务器的绝对路径</returns>
		public string ReceiveImage (int index)
		{
			if (this.IsRegexMessage)
			{
#if DEBUG
				throw new ArithmeticException ("无法解析原始消息. 原因: 当前消息属于正则消息");
#endif
			}

			IEnumerable<CQCode> codes = from code in this.CQCodes where code.Function == CQFunction.Image select code;
			if (codes != null)
			{
				if (codes.Count () > 0)
				{
					return this.CQApi.ReceiveImage (codes.ElementAt (index).Items["file"]);
				}
			}
			return null;
		}
		/// <summary>
		/// 接收消息中的所有图片 (消息含有CQ码 "image" 的消息)
		/// </summary>
		/// <exception cref="ArithmeticException">当前实例属于正则消息时 (仅 Debug 生效)</exception>
		/// <returns>返回图片文件位于本地服务器的绝对路径数组</returns>
		public string[] ReceiveAllImage ()
		{
			if (this.IsRegexMessage)
			{
#if DEBUG
				throw new ArithmeticException ("无法解析原始消息. 原因: 当前消息属于正则消息");
#endif
			}

			IEnumerable<CQCode> codes = from code in this.CQCodes where code.Function == CQFunction.Image select code;
			if (codes != null)
			{
				string[] result = new string[codes.Count ()];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = this.CQApi.ReceiveImage (codes.ElementAt (i).Items["file"]);
				}
				return result;
			}
			return null;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (QQMessage other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Id == other.Id && this.Text.Equals (other.Text);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as QQMessage);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & Text.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("标识: {0}{1}", this.Id, Environment.NewLine);
			builder.AppendFormat ("内容: {0}{1}", this.Text, Environment.NewLine);
			builder.AppendFormat ("是否正则: {0}{1}", this.IsRegexMessage, Environment.NewLine);
			builder.AppendLine ("解析结果:");
			foreach (KeyValuePair<string, string> item in this.RegexResult)
			{
				builder.Append ("\t");
				builder.AppendFormat ("{0}: {1}", item.Key, item.Value);
				builder.AppendLine ();
			}
			return builder.ToString ();
		}
		/// <summary>
		/// 当在派生类中重写时, 处理返回用于发送的字符串
		/// </summary>
		/// <returns>用于发送的字符串</returns>
		public override string ToSendString ()
		{
			return this;
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 进行当前模型初始化
		/// </summary>
		/// <param name="reader">解析模型的数据源</param>
		protected override void Initialize (BinaryReader reader)
		{
			if (this.IsRegexMessage)
			{
				if (reader.Length () < 4)
				{
					throw new InvalidDataException ("读取失败, 获取的原始数据长度小于 4");
				}

				int count = reader.ReadInt32_Ex (); // 获取解析到的正则结果个数
				if (count > 0)
				{
					if (this.IsRegexMessage)
					{
						this.RegexResult = new Dictionary<string, string> (count);
					}

					for (int i = 0; i < count; i++)
					{
						using (BinaryReader temeReader = new BinaryReader (new MemoryStream (reader.ReadToken_Ex ())))
						{
							if (reader.Length () < 4)
							{
								throw new InvalidDataException (string.Format ("读取失败, 获取的原始数据出现异常. Index: {0} 的数据长度小于 4", i + 1));
							}

							string key = temeReader.ReadString_Ex (CQApi.DefaultEncoding);
							string value = temeReader.ReadString_Ex (CQApi.DefaultEncoding);
							this.RegexResult.Add (key, value);
						}
					}
				}
			}
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="QQ"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="QQ"/> 对象</param>
		/// <exception cref="ArithmeticException">当 <see cref="IsRegexMessage"/> 为 <see langword="true"/> 时会发生异常 (仅 Debug 模式)</exception>
		public static implicit operator string (QQMessage value)
		{
			if (value.IsRegexMessage)
			{
#if DEBUG
				throw new ArithmeticException ("无法将 QQMessage 隐式转换为 String 类型, 因为当前实例的 IsRegexMessage 为 True");
#else
				return string.Empty;
#endif
			}
			return value.Text;
		}
		#endregion
	}
}
