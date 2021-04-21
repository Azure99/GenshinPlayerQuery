using Native.Tool.IniConfig.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 用于描述 Ini 配置项的类
	/// </summary>
	[Serializable]
	[Obsolete ("请改用 IObject 类型")]
	public class IniObject : List<IniSection>
	{
		#region --字段--
		private string _filePath = string.Empty;
		private Encoding _encoding = Encoding.Default;
		private static readonly Lazy<Regex[]> regices = new Lazy<Regex[]> (() => new Regex[]
		{
			new Regex(@"^\[(.+)\]", RegexOptions.Compiled),						//匹配 节
			new Regex(@"^([^\r\n=]+)=((?:[^\r\n]+)?)",RegexOptions.Compiled),    //匹配 键值对
 			new Regex(@"^;(?:[\s\S]*)", RegexOptions.Compiled)				//匹配 注释
		});
		#endregion

		#region --属性--
		/// <summary>
		/// 通过名字获取或设置指定索引处的元素
		/// </summary>
		/// <param name="name">节名称</param>
		/// <exception cref="SectionNotFoundException">无法通过名字定位 <see cref="IniSection"/> 的位置</exception>
		/// <returns>指定索引处的元素</returns>
		public IniSection this[string name]
		{
			get
			{
				try
				{
					return this.Where (temp => temp.Name.CompareTo (name) == 0).First ();
				}
				catch (ArgumentNullException ex)
				{
					throw new SectionNotFoundException (string.Format ("无法通过指定的名称 {0} 找到对应的 IniSection", name), ex);
				}
				catch (InvalidOperationException ex)
				{
					throw new SectionNotFoundException (string.Format ("无法通过指定的名称 {0} 找到对应的 IniSection", name), ex);
				}
			}
			set
			{
				try
				{
					IniSection section = this.Where (temp => temp.Name.CompareTo (name) == 0).First ();
					this[this.IndexOf (section)] = value;
				}
				catch (ArgumentNullException ex)
				{
					throw new SectionNotFoundException (string.Format ("无法通过指定的名称 {0} 找到对应的 IniSection", name), ex);
				}
				catch (InvalidOperationException ex)
				{
					throw new SectionNotFoundException (string.Format ("无法通过指定的名称 {0} 找到对应的 IniSection", name), ex);
				}
			}
		}

		/// <summary>
		/// 获取或设置用于读取或保存 Ini 配置项的 <see cref="System.Text.Encoding"/> 实例, 默认: ANSI
		/// </summary>
		public Encoding Encoding { get { return this._encoding; } set { this._encoding = value; } }

		/// <summary>
		/// 获取或设置用于保存 Ini 配置项的 <see cref="Uri"/> 实例
		/// </summary>
		public Uri Path { get; set; }

		/// <summary>
		/// 获取用于解析 Ini 配置项的 Regex 对象数组
		/// </summary>
		private static Regex[] Regices { get { return regices.Value; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="IniObject"/> 类的新实例，该实例为空并且具有默认初始容量
		/// </summary>
		public IniObject ()
		{ }

		/// <summary>
		/// 初始化 <see cref="IniObject"/> 类的新实例，该实例为空并且具有指定的初始容量
		/// </summary>
		/// <param name="capacity">新列表最初可以存储的元素数</param>
		/// <exception cref="ArgumentOutOfRangeException">capacity 小于 0</exception>
		public IniObject (int capacity)
			: base (capacity)
		{ }

		/// <summary>
		/// 初始化 <see cref="IniObject"/> 类的新实例，该实例包含从指定集合复制的元素并且具有足够的容量来容纳所复制的元素
		/// </summary>
		/// <param name="collection">一个集合，其元素被复制到新列表中</param>
		/// <exception cref="ArgumentNullException">collection 为 null</exception>
		public IniObject (IEnumerable<IniSection> collection)
			: base (collection)
		{ }
		#endregion

		#region --公开方法--
		/// <summary>
		/// 将 Ini 配置项保存到指定的文件。如果存在指定文件，则此方法会覆盖它
		/// </summary>
		public void Save ()
		{
			if (this.Path == null)
			{
				throw new UriFormatException (string.Format ("Uri: {0}, 是无效的 Uri 对象", "IniObject.Path"));
			}
			this.Save (this.Path);
		}

		/// <summary>
		/// 将 Ini 配置项保存到指定的文件。 如果存在指定文件，则此方法会覆盖它。
		/// </summary>
		/// <param name="filePath">要将文档保存到其中的文件的位置。</param>
		public void Save (string filePath)
		{
			this.Save (new Uri (filePath));
		}

		/// <summary>
		/// 将 Ini 配置项保存到指定的文件。 如果存在指定文件，则此方法会覆盖它。
		/// </summary>
		/// <param name="fileUri">要将文档保存到其中的文件的位置。</param>
		public virtual void Save (Uri fileUri)
		{
			fileUri = ConvertAbsoluteUri (fileUri);

			if (!fileUri.IsFile)
			{
				throw new ArgumentException ("所指定的必须是文件 URI", "fileUri");
			}

			using (TextWriter textWriter = new StreamWriter (fileUri.GetComponents (UriComponents.Path, UriFormat.Unescaped), false, this.Encoding))
			{
				foreach (IniSection section in this)
				{
					textWriter.WriteLine ("[{0}]", section.Name);
					foreach (KeyValuePair<string, IniValue> pair in section)
					{
						textWriter.WriteLine ("{0}={1}", pair.Key, pair.Value);
					}
					textWriter.WriteLine ();
				}
			}
		}

		/// <summary>
		/// 从文件以 ANSI 编码创建一个新的 IniObject 实例对象
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>转换成功返回 IniObject 实例对象</returns>
		public static IniObject Load (string filePath)
		{
			return Load (new Uri (filePath));
		}

		/// <summary>
		/// 从文件以 ANSI 编码创建一个新的 IniObject 实例对象
		/// </summary>
		/// <param name="fileUri">文件路径的 Uri 对象</param>
		/// <returns>转换成功返回 IniObject 实例对象</returns>
		public static IniObject Load (Uri fileUri)
		{
			return Load (fileUri, Encoding.Default);
		}

		/// <summary>
		/// 从文件以指定编码创建一个新的 IniObject 实例对象
		/// </summary>
		/// <param name="filePath">文件路径字符串</param>
		/// <param name="encoding">文件编码</param>
		/// <returns></returns>
		public static IniObject Load (string filePath, Encoding encoding)
		{
			return Load (new Uri (filePath), encoding);
		}

		/// <summary>
		/// 从文件以指定编码创建一个新的 IniObject 实例对象
		/// </summary>
		/// <param name="fileUri">文件路径的 Uri 对象</param>
		/// <param name="encoding">文件编码</param>
		/// <returns>转换成功返回 IniObject 实例对象</returns>
		public static IniObject Load (Uri fileUri, Encoding encoding)
		{
			fileUri = ConvertAbsoluteUri (fileUri);

			if (!fileUri.IsFile)
			{
				throw new ArgumentException ("所指定的必须是文件 URI", "fileUri");
			}

			//解释 Ini 文件
			using (TextReader textReader = new StreamReader (fileUri.GetComponents (UriComponents.Path, UriFormat.Unescaped), encoding))
			{
				IniObject iObj = ParseIni (textReader);
				iObj.Path = fileUri;
				return iObj;
			}
		}

		/// <summary>
		/// 从字符串中创建一个新的 IniObject 实例对象
		/// </summary>
		/// <param name="iniStr">源字符串</param>
		/// <returns>转换成功返回 IniObject 实例对象</returns>
		public static IniObject Parse (string iniStr)
		{
			using (TextReader textReader = new StringReader (iniStr))
			{
				return ParseIni (textReader);
			}
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 处理 <see cref="Uri"/> 实例, 将其变为可直接使用的对象
		/// </summary>
		/// <param name="fileUri">文件路径的 <see cref="Uri"/> 对象</param>
		/// <returns>返回处理过的 Uri</returns>
		private static Uri ConvertAbsoluteUri (Uri fileUri)
		{
			if (!fileUri.IsAbsoluteUri)
			{
				// 处理原始字符串
				StringBuilder urlBuilder = new StringBuilder (fileUri.OriginalString);
				urlBuilder.Replace ("/", "\\");
				while (urlBuilder[0] == '\\')
				{
					urlBuilder.Remove (0, 1);
				}

				// 将相对路径转为绝对路径
				urlBuilder.Insert (0, AppDomain.CurrentDomain.BaseDirectory);
				fileUri = new Uri (urlBuilder.ToString (), UriKind.Absolute);
			}

			return fileUri;
		}

		/// <summary>
		/// 逐行解析 Ini 配置文件字符串
		/// </summary>
		/// <param name="textReader"></param>
		/// <returns></returns>
		private static IniObject ParseIni (TextReader textReader)
		{
			IniObject iniObj = new IniObject ();
			IniSection iniSect = null;
			while (textReader.Peek () != -1)
			{
				string line = textReader.ReadLine ();
				if (string.IsNullOrEmpty (line) == false && Regices[2].IsMatch (line) == false)     //跳过空行和注释
				{
					Match match = Regices[0].Match (line);
					if (match.Success)
					{
						iniSect = new IniSection (match.Groups[1].Value);
						iniObj.Add (iniSect);
						continue;
					}

					match = Regices[1].Match (line);
					if (match.Success)
					{
						iniSect.Add (match.Groups[1].Value.Trim (), match.Groups[2].Value);
					}
				}
			}
			return iniObj;
		}
		#endregion

		#region --重写方法--
		/// <summary>
		/// 将当前实例转换为等效的字符串
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			StringBuilder iniString = new StringBuilder ();
			using (TextWriter textWriter = new StringWriter (iniString))
			{
				foreach (IniSection section in this)
				{
					textWriter.WriteLine ("[{0}]", section.Name.Trim ());
					foreach (KeyValuePair<string, IniValue> pair in section)
					{
						textWriter.WriteLine ("{0}={1}", pair.Key.Trim (), pair.Value.Value.Trim ());
					}
					textWriter.WriteLine ();
				}
			}
			return iniString.ToString ();
		}
		#endregion
	}
}
