using Native.Tool.IniConfig.Attribute;
using Native.Tool.IniConfig.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Native.Tool.IniConfig
{
	/// <summary>
	/// 配置项 (Ini) 文件的操作类
	/// </summary>
	public class IniConfig
	{
		#region --字段--
		private IObject _object;
		private Encoding _encoding;
		private static readonly Regex[] Regices = new Regex[]
		{
			new Regex(@"^\[(.+)\]", RegexOptions.Compiled),						//匹配 节
			new Regex(@"^([^\r\n=]+)=((?:[^\r\n]+)?)",RegexOptions.Compiled),   //匹配 键值对
 			new Regex(@"^;(?:[\s\S]*)", RegexOptions.Compiled)					//匹配 注释
		};
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例持有的 <see cref="IObject"/>
		/// </summary>
		public IObject Object
		{
			get { return this._object; }
			set
			{
				this._object = value;
			}
		}
		/// <summary>
		/// 获取或设置当前实例读取或写入文件时使用的编码格式
		/// </summary>
		public Encoding Encoding
		{
			get { return this._encoding; }
			set { this._encoding = value; }
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="IniConfig"/> 类的新实例, 并设置文件位置和默认的文件编码
		/// </summary>
		/// <param name="filePath"><see cref="IniConfig"/> 使用的文件位置</param>
		public IniConfig (string filePath)
			: this (filePath, null)
		{ }
		/// <summary>
		/// 初始化 <see cref="IniConfig"/> 类的新实例, 并设置文件位置和文件编码
		/// </summary>
		/// <param name="filePath"><see cref="IniConfig"/> 使用的文件位置</param>
		/// <param name="encoding"><see cref="IniConfig"/> 使用的文件编码</param>
		public IniConfig (string filePath, Encoding encoding)
		{
			this.InitializeIni (filePath, encoding);
		}
		/// <summary>
		/// 初始化 <see cref="IniConfig"/> 类的新实例, 该实例持有一个 <see cref="IObject"/>
		/// </summary>
		/// <param name="obj"><see cref="IniConfig"/> 使用的 Ini 对象</param>
		public IniConfig (IObject obj)
			: this (obj, null)
		{ }
		/// <summary>
		/// 初始化 <see cref="IniConfig"/> 类的新实例, 该实例持有一个 <see cref="IObject"/>
		/// </summary>
		/// <param name="obj"><see cref="IniConfig"/> 使用的 Ini 对象</param>
		/// <param name="encoding"><see cref="IniConfig"/> 使用的文件编码</param>
		public IniConfig (IObject obj, Encoding encoding)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			this.InitializeIni (obj.FileName, encoding);
			foreach (ISection item in obj)
			{
				this._object.Add (item);
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 从文件中读取配置项 (Ini) 实例, 重新加载则会清空当前实例
		/// </summary>
		/// <returns>返回读取的结果, 若成功则为 <see langword="true"/> 否则为 <see langword="false"/></returns>
		public bool Load ()
		{
			this._object.Clear ();
			try
			{
				using (TextReader textReader = new StreamReader (this._object.FileName, this._encoding))
				{
					IniConfig.ParseIni (this._object, textReader);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// 将当前修改的配置项 (Ini) 实例, 覆盖式写入文件
		/// </summary>
		/// <returns>返回读取的结果, 若成功则为 <see langword="true"/> 否则为 <see langword="false"/></returns>
		public bool Save ()
		{
			try
			{
				using (TextWriter textWriter = new StreamWriter (this._object.FileName, false, this._encoding))
				{
					textWriter.Write (this._object.ToString ());
					return true;
				}
			}
			catch { return false; }
		}
		/// <summary>
		/// 从持有的 <see cref="IObject"/> 中移除所有项
		/// </summary>
		public void Clear ()
		{
			this._object.Clear ();
		}
		/// <summary>
		/// 从持有的 <see cref="IObject"/> 中移除指定名称的 <see cref="ISection"/>
		/// </summary>
		/// <param name="sectionKey">要移除的元素的键</param>
		public void Remove (string sectionKey)
		{
			this._object.Remove (sectionKey);
		}
		/// <summary>
		/// 向 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中设置一个对象并序列化成 <see cref="ISection"/>, 若存在重复节名的对象将会覆盖整个节
		/// </summary>
		/// <param name="obj">用作要添加的元素的值的对象</param>
		/// <exception cref="ArgumentNullException">obj 为 null</exception>
		public void SetObject (object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			Type tType = obj.GetType ();

			// 获取节点名称
			string key = string.Empty;
			IniSectionAttribute sectionAttribute = tType.GetCustomAttribute<IniSectionAttribute> ();
			if (sectionAttribute != null)
			{
				key = sectionAttribute.SectionName;
			}
			else
			{
				key = tType.Name;
			}

			this.SetObject (key, obj);
		}
		/// <summary>
		/// 向 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中设置一个对象并序列化成指定节名的 <see cref="ISection"/>, 若节名已存在将会覆盖整个节
		/// </summary>
		/// <param name="sectionKey">用作要添加的元素的键的对象</param>
		/// <param name="obj">用作要添加的元素的值的对象</param>
		/// <exception cref="ArgumentException">sectionKey 为空字符串或为 <see langword="null"/></exception>
		/// <exception cref="ArgumentNullException">obj 为 null</exception>
		public void SetObject (string sectionKey, object obj)
		{
			if (string.IsNullOrEmpty (sectionKey))
			{
				throw new ArgumentException ("sectionKey 不允许为空. 原因: obj 不能设置到为未命名的节点", "sectoinKey");
			}
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			if (!this._object.ContainsKey (sectionKey))   // 如果不存在这个名称
			{
				this._object.Add (new ISection (sectionKey));
			}

			// 获取节对象
			ISection section = this._object[sectionKey];
			if (section.Count > 0)
			{
				section.Clear ();
			}

			Type tType = obj.GetType ();
			PropertyInfo[] tProperties = tType.GetProperties (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (PropertyInfo property in tProperties)
			{
				// 获取属性是否有标记 KeyName
				IniKeyAttribute keyAttribute = property.GetCustomAttribute<IniKeyAttribute> ();

				string key = string.Empty;
				if (keyAttribute != null)
				{
					key = keyAttribute.KeyName;
				}
				else
				{
					key = property.Name;
				}

				// 判断 Key 重复
				if (section.ContainsKey (key))
				{
					throw new ArgumentException ("序列化的对象内部存在一个或多个重复的 Key, 这个异常可能由于属性名和特性引起.", "obj");
				}

				// 获取只有0个参数的 Get 方法 (排除索引器)
				MethodInfo method = property.GetGetMethod (true);
				if (method.GetParameters ().Count () == 0)
				{
					section.Add (key, new IValue (method.Invoke (obj, null)));
				}
			}
		}
		/// <summary>
		///  从 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中获取一个 <see cref="ISection"/> 反序列化成指定类型的对象.
		/// </summary>
		/// <typeparam name="T">用作转换目标的元素类型</typeparam>
		/// <exception cref="ArgumentNullException">t 为 null</exception>
		/// <exception cref="ArgumentException">t 不是 <see cref="RuntimeType"/>。 或 t 是开放式泛型类型（即，<see cref="Type.ContainsGenericParameters"/> 属性将返回 <see langword="true"/>）</exception>
		/// <exception cref="NotSupportedException">t 不能为 <see cref="TypeBuilder"/>。 或 不支持创建 <see cref="TypedReference"/>、<see cref="ArgIterator"/>、<see cref="Void"/>和 <see cref="RuntimeArgumentHandle"/> 类型，或者这些类型的数组。 或 包含 t 的程序集是一个用 <see cref="AssemblyBuilderAccess.Save"/> 创建的动态程序集</exception>
		/// <exception cref="TargetInvocationException">正在被调用的构造函数引发了一个异常</exception>
		/// <exception cref="MethodAccessException">调用方没有权限调用此构造函数</exception>
		/// <exception cref="MemberAccessException">无法创建抽象类的实例，或者此成员是使用晚期绑定机制调用的</exception>
		/// <exception cref="InvalidComObjectException">未通过 Overload:<see cref="Type.GetTypeFromProgID"/> 或 Overload:<see cref="Type.GetTypeFromCLSID"/> 获取 COM 类型</exception>
		/// <exception cref="COMException">t 是一个 COM 对象，但用于获取类型的类标识符无效，或标识的类未注册</exception>
		/// <exception cref="TypeLoadException">t 不是有效类型</exception>
		/// <exception cref="TargetException">尝试调用一个不存在的属性</exception>
		/// <returns>指定的对象实例</returns>
		public T GetObject<T> ()
		{
			return (T)this.GetObject (typeof (T));
		}
		/// <summary>
		/// 从 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中获取一个指定节名的 <see cref="ISection"/> 反序列化成指定类型的对象
		/// </summary>
		/// <typeparam name="T">用作转换目标的元素类型</typeparam>
		/// <param name="sectionKey">用作要获取的元素的键的对象</param>
		/// <exception cref="ArgumentNullException">t 为 null</exception>
		/// <exception cref="ArgumentException">sectionKey 未能匹配到指定节点 或 t 不是 <see cref="RuntimeType"/>。 或 t 是开放式泛型类型（即，<see cref="Type.ContainsGenericParameters"/> 属性将返回 <see langword="true"/>）</exception>
		/// <exception cref="NotSupportedException">t 不能为 <see cref="TypeBuilder"/>。 或 不支持创建 <see cref="TypedReference"/>、<see cref="ArgIterator"/>、<see cref="Void"/>和 <see cref="RuntimeArgumentHandle"/> 类型，或者这些类型的数组。 或 包含 t 的程序集是一个用 <see cref="AssemblyBuilderAccess.Save"/> 创建的动态程序集</exception>
		/// <exception cref="TargetInvocationException">正在被调用的构造函数引发了一个异常</exception>
		/// <exception cref="MethodAccessException">调用方没有权限调用此构造函数</exception>
		/// <exception cref="MemberAccessException">无法创建抽象类的实例，或者此成员是使用晚期绑定机制调用的</exception>
		/// <exception cref="InvalidComObjectException">未通过 Overload:<see cref="Type.GetTypeFromProgID"/> 或 Overload:<see cref="Type.GetTypeFromCLSID"/> 获取 COM 类型</exception>
		/// <exception cref="COMException">t 是一个 COM 对象，但用于获取类型的类标识符无效，或标识的类未注册</exception>
		/// <exception cref="TypeLoadException">t 不是有效类型</exception>
		/// <exception cref="TargetException">尝试调用一个不存在的属性</exception>
		/// <returns>指定的对象实例</returns>
		public T GetObject<T> (string sectionKey)
		{
			return (T)this.GetObject (sectionKey, typeof (T));
		}
		/// <summary>
		/// 从 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中获取一个 <see cref="ISection"/> 反序列化成指定类型的对象.
		/// </summary>
		/// <param name="t">用作要转换目标对象的类型的对象, 同时获取的节名称由这个类型的名称或由标记的 <see cref="IniSectionAttribute"/> 特性决定</param>
		/// <exception cref="ArgumentNullException">t 为 null</exception>
		/// <exception cref="ArgumentException">t 不是 <see cref="RuntimeType"/>。 或 t 是开放式泛型类型（即，<see cref="Type.ContainsGenericParameters"/> 属性将返回 <see langword="true"/>）</exception>
		/// <exception cref="NotSupportedException">t 不能为 <see cref="TypeBuilder"/>。 或 不支持创建 <see cref="TypedReference"/>、<see cref="ArgIterator"/>、<see cref="Void"/>和 <see cref="RuntimeArgumentHandle"/> 类型，或者这些类型的数组。 或 包含 t 的程序集是一个用 <see cref="AssemblyBuilderAccess.Save"/> 创建的动态程序集</exception>
		/// <exception cref="TargetInvocationException">正在被调用的构造函数引发了一个异常</exception>
		/// <exception cref="MethodAccessException">调用方没有权限调用此构造函数</exception>
		/// <exception cref="MemberAccessException">无法创建抽象类的实例，或者此成员是使用晚期绑定机制调用的</exception>
		/// <exception cref="InvalidComObjectException">未通过 Overload:<see cref="Type.GetTypeFromProgID"/> 或 Overload:<see cref="Type.GetTypeFromCLSID"/> 获取 COM 类型</exception>
		/// <exception cref="COMException">t 是一个 COM 对象，但用于获取类型的类标识符无效，或标识的类未注册</exception>
		/// <exception cref="TypeLoadException">t 不是有效类型</exception>
		/// <exception cref="TargetException">尝试调用一个不存在的属性</exception>
		/// <returns>指定的对象实例</returns>
		public object GetObject (Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException ("t");
			}

			string key = string.Empty;
			IniSectionAttribute sectionAttribute = t.GetCustomAttribute<IniSectionAttribute> ();
			if (sectionAttribute != null)
			{
				key = sectionAttribute.SectionName;
			}
			else
			{
				key = t.Name;
			}

			return this.GetObject (key, t);
		}
		/// <summary>
		/// 从 <see cref="IniConfig"/> 维护的 <see cref="IObject"/> 中获取一个指定节名的 <see cref="ISection"/> 反序列化成指定类型的对象
		/// </summary>
		/// <param name="sectionKey">用作要获取的元素的键的对象</param>
		/// <param name="t">用作要转换目标对象的类型的对象</param>
		/// <exception cref="ArgumentNullException">t 为 null</exception>
		/// <exception cref="ArgumentException">sectionKey 未能匹配到指定节点 或 t 不是 <see cref="RuntimeType"/>。 或 t 是开放式泛型类型（即，<see cref="Type.ContainsGenericParameters"/> 属性将返回 <see langword="true"/>）</exception>
		/// <exception cref="NotSupportedException">t 不能为 <see cref="TypeBuilder"/>。 或 不支持创建 <see cref="TypedReference"/>、<see cref="ArgIterator"/>、<see cref="Void"/>和 <see cref="RuntimeArgumentHandle"/> 类型，或者这些类型的数组。 或 包含 t 的程序集是一个用 <see cref="AssemblyBuilderAccess.Save"/> 创建的动态程序集</exception>
		/// <exception cref="TargetInvocationException">正在被调用的构造函数引发了一个异常</exception>
		/// <exception cref="MethodAccessException">调用方没有权限调用此构造函数</exception>
		/// <exception cref="MemberAccessException">无法创建抽象类的实例，或者此成员是使用晚期绑定机制调用的</exception>
		/// <exception cref="InvalidComObjectException">未通过 Overload:<see cref="Type.GetTypeFromProgID"/> 或 Overload:<see cref="Type.GetTypeFromCLSID"/> 获取 COM 类型</exception>
		/// <exception cref="COMException">t 是一个 COM 对象，但用于获取类型的类标识符无效，或标识的类未注册</exception>
		/// <exception cref="TypeLoadException">t 不是有效类型</exception>
		/// <exception cref="TargetException">尝试调用一个不存在的属性</exception>
		/// <returns>指定的对象实例</returns>
		public object GetObject (string sectionKey, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException ("t");
			}
			if (!this._object.ContainsKey (sectionKey))   // 如果不存在这个名称
			{
				throw new ArgumentException ("未能找到与传入节名匹配的节点, 请检查节名是否有效", "sectionKey");
			}

			// 实例化对象
			object instance = Activator.CreateInstance (t, true);

			// 获取属性列表
			List<PropertyInfo> properties = new List<PropertyInfo> (t.GetProperties (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));

			// 获取节点
			ISection section = this._object[sectionKey];
			foreach (KeyValuePair<string, IValue> item in section)
			{
				if (properties.Count == 0)
				{
					throw new TargetException (string.Format ("正在尝试调用一个不存在的属性 {0} 引发异常", item.Key));
				}

				for (int i = 0; i < properties.Count; i++)
				{
					IniKeyAttribute keyAttribute = properties[i].GetCustomAttribute<IniKeyAttribute> ();
					if ((keyAttribute != null && keyAttribute.KeyName.Equals (item.Key)) || properties[i].Name.Equals (item.Key))
					{
						MethodInfo method = properties[i].GetSetMethod (true);
						if (method.GetParameters ().Count () == 0)
						{
							method.Invoke (instance, new object[] { item.Value });
						}

						properties.RemoveAt (i);
						break;
					}
				}
			}

			return instance;
		}
		#endregion

		#region --私有方法--
		private void InitializeIni (string filePath, Encoding encoding)
		{
			// 处理字符串
			if (!Path.IsPathRooted (filePath))
			{
				// 处理原始字符串
				StringBuilder builder = new StringBuilder (filePath);
				builder.Replace ('/', '\\');
				while (builder[0] == '\\')
				{
					builder.Remove (0, 1);
				}

				// 相对路径转绝对路径
				builder.Insert (0, AppDomain.CurrentDomain.BaseDirectory);
				filePath = builder.ToString ();
			}

			// 处理文件
			if (!File.Exists (filePath))
			{
				File.Create (filePath).Close ();
			}

			// 创建对象
			this._object = new IObject (filePath);
			if (encoding == null)
			{
				this._encoding = Encoding.Default;
			}
			else
			{
				this._encoding = encoding;
			}
		}
		private static void ParseIni (IObject iniObj, TextReader textReader)
		{
			if (iniObj == null)
			{
				throw new ArgumentNullException ("iniObj");
			}

			if (textReader == null)
			{
				throw new ArgumentNullException ("textReader");
			}

			ISection tempSection = null;
			while (textReader.Peek () != -1)
			{
				string line = textReader.ReadLine ();
				if (string.IsNullOrEmpty (line) == false && Regices[2].IsMatch (line) == false)
				{
					Match match = Regices[0].Match (line);
					if (match.Success)
					{
						tempSection = new ISection (match.Groups[1].Value);
						iniObj.Add (tempSection);
						continue;
					}

					match = Regices[1].Match (line);
					if (match.Success)
					{
						tempSection.Add (match.Groups[1].Value.Trim (), match.Groups[2].Value);
					}
				}
			}
			string temp=null;
			IValue value = new IValue(temp);
			if (tempSection != null) tempSection.SetDefault(value);
		}
		#endregion
	}
}
