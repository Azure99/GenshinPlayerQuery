using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 用于描述 Ini 配置项节的类
	/// </summary>
	[Serializable]
	[Obsolete ("请改用 ISection 类型")]
	public class IniSection : Dictionary<string, IniValue>
	{
		#region --字段--
		private string _name = string.Empty;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置与指定键关联的值 (此索引器允许直接对不存在的键进行设置)
		/// </summary>
		/// <param name="name">要获取或设置的值的键</param>
		/// <returns></returns>
		public new IniValue this[string name]
		{
			get
			{
				if (base.ContainsKey (name))
				{
					return base[name];
				}
				else
				{
					return IniValue.Empty;
				}
			}
			set
			{
				if (base.ContainsKey (name))
				{
					base[name] = value;
				}
				else
				{
					base.Add (name, value);
				}
			}
		}
		/// <summary>
		/// 获取或设置当前节名称
		/// </summary>
		public string Name { get { return this._name; } set { this._name = value; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 IniSection 类的新实例，该实例为空，具有默认的初始容量并为键类型使用默认的相等比较器。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		public IniSection (string name) : base ()
		{
			this._name = name;
		}
		/// <summary>
		/// 初始化 IniSection 类的新实例，该实例为空，具有指定的初始容量并为键类型使用默认的相等比较器。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="capacity">IniSection 可包含的初始元素数。</param>
		public IniSection (string name, int capacity) : base (capacity)
		{
			this._name = name;
		}
		/// <summary>
		///  初始化 IniSection 类的新实例，该实例为空，具有默认的初始容量并使用指定的 IEqualityComparer&lt;in string&gt;。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="comparer">比较键时要使用的 IEqualityComparer 实现，或者为 null，以便为键类型使用默认的 IEqualityComparer。</param>
		public IniSection (string name, IEqualityComparer<string> comparer) : base (comparer)
		{
			this._name = name;
		}
		/// <summary>
		/// 初始化 IniSection 类的新实例，该实例包含从指定的 IniSection 复制的元素并为键类型使用默认的相等比较器。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="dictionary">IDictionary&lt;string, IniValue&gt;，它的元素被复制到新 IniSection。</param>
		public IniSection (string name, IDictionary<string, IniValue> dictionary) : base (dictionary)
		{
			this._name = name;
		}
		/// <summary>
		///  初始化 IniSection 类的新实例，该实例为空，具有指定的初始容量并使用指定的 IEqualityComparer。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="capacity">IniSection 可包含的初始元素数。</param>
		/// <param name="comparer">比较键时要使用的 IEqualityComparer&lt;in string&gt;实现，或者为 null，以便为键类型使用默认的 EqualityComparer。</param>
		public IniSection (string name, int capacity, IEqualityComparer<string> comparer) : base (capacity, comparer)
		{
			this._name = name;
		}
		/// <summary>
		/// 初始化 IniSection 类的新实例，该实例包含从指定的 IDictionary&lt;string, IniValue&gt; 中复制的元素并使用指定的 IEqualityComparer&lt;in string&gt;。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="dictionary">IDictionary&lt;string, IniValue&gt;，它的元素被复制到新 IniSection。</param>
		/// <param name="comparer">比较键时要使用的 IEqualityComparer&lt;in string&gt;实现，或者为 null，以便为键类型使用默认的 EqualityComparer。</param>
		public IniSection (string name, IDictionary<string, IniValue> dictionary, IEqualityComparer<string> comparer) : base (dictionary, comparer)
		{
			this._name = name;
		}
		/// <summary>
		/// 用序列化数据初始化 IniSection 类的新实例。
		/// </summary>
		/// <param name="name">IniSection 关联的名称</param>
		/// <param name="info">一个 System.Runtime.Serialization.SerializationInfo 对象包含序列化 IniSection 所需的信息。</param>
		/// <param name="context">一个 System.Runtime.Serialization.StreamingContext 结构包含与 IniSection 关联的序列化流的源和目标。</param>
		protected IniSection (string name, SerializationInfo info, StreamingContext context) : base (info, context)
		{
			this._name = name;
		}
		/// <summary>
		/// 用序列化数据初始化 <see cref="IniSection"/> 类的新实例 
		/// </summary>
		/// <param name="serializationInfo">一个 <see cref="SerializationInfo"/> 包含 <see cref="IniSection"/> 所需的信息。</param>
		/// <param name="streamingContext">一个 <see cref="StreamingContext"/> 结构包含与 <see cref="IniSection"/> 关联的序列化流的源和目标。</param>
		protected IniSection (SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base (serializationInfo, streamingContext)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, bool value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, byte value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, char value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, DateTime value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, decimal value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, double value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, short value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, int value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, long value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, sbyte value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, float value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将建和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value, 若此参数为 null, 将用 IniSection.Empty 代替</param>
		public void Add (string key, string value)
		{
			this.Add (key, new IniValue (value));
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, ushort value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, uint value)
		{
			this.Add (key, value.ToString ());
		}

		/// <summary>
		/// 将键和值添加到 IniSection 的结尾处
		/// </summary>
		/// <param name="key">要添加到 IniSection 结尾处的对象的 Key. 此参数不可为 null 或者 string.Empty</param>
		/// <param name="value">要添加到 IniSection 结尾处的对象的 Value</param>
		public void Add (string key, ulong value)
		{
			this.Add (key, value.ToString ());
		}
		#endregion

		#region --重写方法--
		/// <summary>
		/// 将当前实例转换为其等效的字符串
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			StringBuilder iniStr = new StringBuilder ();
			using (TextWriter textWriter = new StringWriter (iniStr))
			{
				textWriter.WriteLine ("[{0}]", this.Name.Trim ());  //添加 "节"
				foreach (KeyValuePair<string, IniValue> item in this)
				{
					textWriter.WriteLine ("{0}={1}", item.Key.Trim (), item.Value.Value.Trim ());
				}
			}
			return iniStr.ToString ();
		}
		#endregion
	}
}
