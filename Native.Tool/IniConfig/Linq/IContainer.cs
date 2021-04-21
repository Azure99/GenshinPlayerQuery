using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 描述配置项 (Ini) 文件内容的类, 该类是抽象的
	/// </summary>
	/// <typeparam name="TValue">容器内容的类型</typeparam>
	public abstract class IContainer<TValue> : IDictionary<string, TValue>, ICollection<KeyValuePair<string, TValue>>, IEnumerable<KeyValuePair<string, TValue>>, IEnumerable, IEquatable<IContainer<TValue>>
	{
		#region --字段--
		private readonly SortedList<string, TValue> _sortedList;
		private readonly bool _readOnly = false;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置与指定的键关联的值
		/// </summary>
		/// <param name="key">要获取或设置其值的键</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <exception cref="KeyNotFoundException">已检索该属性且集合中不存在 key</exception>
		/// <returns>与指定的键相关联的值。 如果找不到指定的键，则 get 操作会引发一个 <see cref="KeyNotFoundException"/>，而set 操作会创建一个使用指定键的新元素。</returns>
		public TValue this[string key]
		{
			get
			{
				try
				{					
					return this._sortedList[key];
				}
				catch (KeyNotFoundException)
				{
					try
					{
						this._sortedList.Add(key, tv);
						return this._sortedList[key];
					}
					catch
					{
						return default;
					}
				}
			}
			set { this._sortedList[key] = value; }
		}
		static TValue tv;
		/// <summary>
		/// 获取一个按排序顺序包含 <see cref="IContainer{T}"/> 中的键的集合
		/// </summary>
		public ICollection<string> Keys
		{
			get
			{
				return this._sortedList.Keys;
			}
		}
		/// <summary>
		/// 获得一个包含 <see cref="IContainer{T}"/> 中的值的集合
		/// </summary>
		public ICollection<TValue> Values
		{
			get { return this._sortedList.Values; }
		}
		/// <summary>
		/// 获取包含在 <see cref="IContainer{T}"/> 中的键/值对的数目
		/// </summary>
		public int Count
		{
			get { return this._sortedList.Count; }
		}
		/// <summary>
		/// 获取一个值，该值指示当前 <see cref="IContainer{T}"/> 是否为只读
		/// </summary>
		public bool IsReadOnly
		{
			get { return this._readOnly; }
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该示例为空且具有默认的初始容量，并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		protected IContainer ()
		{
			this._sortedList = new SortedList<string, TValue> ();
		}
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该示例为空且具有指定的初始容量，并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="capacity"><see cref="IContainer{TValue}"/> 可包含的初始元素数</param>
		protected IContainer (int capacity)
		{
			this._sortedList = new SortedList<string, TValue> (capacity);
		}
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该实例为空，具有默认的初始容量并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{String}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		protected IContainer (IComparer<string> comparer)
		{
			this._sortedList = new SortedList<string, TValue> (comparer);
		}
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该实例包含从指定的 <see cref="IDictionary{String, TValue}"/> 中复制的元素，其容量足以容纳所复制的元素数并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="dictionary"><see cref="IDictionary{String, TValue}"/>，它的元素被复制到新 <see cref="IContainer{TValue}"/></param>
		protected IContainer (IDictionary<string, TValue> dictionary)
		{
			this._sortedList = new SortedList<string, TValue> (dictionary);
		}
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该实例为空，具有指定的初始容量并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="capacity"><see cref="IContainer{TValue}"/> 可包含的初始元素数</param>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{T}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		protected IContainer (int capacity, IComparer<string> comparer)
		{
			this._sortedList = new SortedList<string, TValue> (capacity, comparer);
		}
		/// <summary>
		/// 初始化 <see cref="IContainer{TValue}"/> 类的新实例，该实例包含从指定的 <see cref="IDictionary{String, TValue}"/> 中复制的元素，其容量足以容纳所复制的元素数并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="dictionary"><see cref="IDictionary{String, TValue}"/>，它的元素被复制到新 <see cref="IComparer{String}"/></param>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{String}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		protected IContainer (IDictionary<string, TValue> dictionary, IComparer<string> comparer)
		{
			this._sortedList = new SortedList<string, TValue> (dictionary, comparer);
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 确定两个指定的 <see cref="IContainer{TValue}"/> 对象是否具有相同的值
		/// </summary>
		/// <param name="objA">要比较的第一个 <see cref="IContainer{TValue}"/>，或 <see langword="null"/></param>
		/// <param name="objB">要比较的第二个 <see cref="IContainer{TValue}"/>，或 <see langword="null"/></param>
		/// <returns>如果 <see langword="true"/> 的值与 objA" 的值相同，则为 objB；否则为 <see langword="false"/>。 如果 objA 和 objB 均为 <see langword="null"/>，此方法将返回 <see langword="true"/></returns>
		public static bool Equals (IContainer<TValue> objA, IContainer<TValue> objB)
		{
			if (objA == objB)
			{
				return true;
			}

			if (!(objA.Count == objB.Count) || objA == null || objB == null)
			{
				return false;
			}

			for (int i = 0; i < objA.Count; i++)
			{
				string keyA = objA.Keys.ElementAt (i);
				string KeyB = objB.Keys.ElementAt (i);

				TValue valueA = objA.Values.ElementAt (i);
				TValue valueB = objB.Values.ElementAt (i);

				if (!(keyA.Equals (KeyB) && object.Equals (valueA, valueB)))
				{
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// 向 <see cref="IContainer{TValue}"/> 添加一个带有所提供的键和值的元素
		/// </summary>
		/// <param name="key">用作要添加的元素的键的对象</param>
		/// <param name="value">用作要添加的元素的值的对象</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <exception cref="ArgumentException"><see cref="IContainer{TValue}"/> 中已存在具有相同键的元素</exception>
		/// <exception cref="NotSupportedException"><see cref="IContainer{TValue}"/> 为只读</exception>
		public void Add (string key, TValue value)
		{
			this._sortedList.Add (key, value);
		}
		public void SetDefault(TValue value)
		{
			tv = value;
		}
		/// <summary>
		/// 向 <see cref="IContainer{TValue}"/> 添加一个带有所提供的键和值的元素
		/// </summary>
		/// <param name="item">用作要添加的元素的键值对的对象</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <exception cref="ArgumentException"><see cref="IContainer{TValue}"/> 中已存在具有相同键的元素</exception>
		/// <exception cref="NotSupportedException"><see cref="IContainer{TValue}"/> 为只读</exception>
		public void Add (KeyValuePair<string, TValue> item)
		{
			this.Add (item.Key, item.Value);
		}
		/// <summary>
		/// 从 <see cref="IContainer{TValue}"/> 中移除所有项
		/// </summary>
		public void Clear ()
		{
			this._sortedList.Clear ();
		}
		/// <summary>
		/// 确定 <see cref="IContainer{TValue}"/> 是否包含特定值
		/// </summary>
		/// <param name="item">要在 <see cref="IContainer{TValue}"/> 中定位的对象</param>
		/// <returns>如果在 <see langword="true"/> 中找到 item，则为 <see cref="IContainer{TValue}"/>；否则为 <see langword="false"/></returns>
		public bool Contains (KeyValuePair<string, TValue> item)
		{
			return this._sortedList.Contains (item);
		}
		/// <summary>
		/// 确定是否 <see cref="IContainer{TValue}"/> 包含带有指定键的元素
		/// </summary>
		/// <param name="key">要在 <see cref="IContainer{TValue}"/> 中定位的键</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <returns>如果 <see langword="true"/> 包含具有键的元素，则为 <see cref="IContainer{TValue}"/>；否则为 <see langword="false"/></returns>
		public bool ContainsKey (string key)
		{
			return this._sortedList.ContainsKey (key);
		}
		/// <summary>
		/// 确定 <see cref="IContainer{TValue}"/> 是否包含特定值
		/// </summary>
		/// <param name="value">要在 <see cref="IContainer{TValue}"/> 中定位的值。</param>
		/// <returns>如果 <see langword="true"/> 包含具有指定值的元素，则为 <see cref="IContainer{TValue}"/>；否则为 <see langword="false"/>。</returns>
		public bool ContainsValue (TValue value)
		{
			return this._sortedList.ContainsValue (value);
		}
		/// <summary>
		/// 在整个 <see cref="IContainer{TValue}"/> 中搜索指定键并返回从零开始的索引
		/// </summary>
		/// <param name="key">要在 <see cref="IContainer{TValue}"/> 中定位的键</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <returns>如果找到，则为整个 key 中 <see cref="IContainer{TValue}"/> 的从零开始的索引；否则为 -1。</returns>
		public int IndexOfKey (string key)
		{
			return this._sortedList.IndexOfKey (key);
		}
		/// <summary>
		/// 在整个 <see cref="IContainer{TValue}"/> 中搜索指定的值，并返回第一个匹配项的从零开始的索引。
		/// </summary>
		/// <param name="value">要在 <see cref="IContainer{TValue}"/> 中定位的值。</param>
		/// <returns>如果找到，则为整个 value 中 <see cref="IContainer{TValue}"/> 第一个匹配项的从零开始的索引；否则为-1。</returns>
		public int IndexOfValue (TValue value)
		{
			return this._sortedList.IndexOfValue (value);
		}
		/// <summary>
		/// 从特定的 <see cref="IContainer{TValue}"/> 索引处开始，将 <see cref="Array"/> 的元素复制到一个 <see cref="Array"/> 中
		/// </summary>
		/// <param name="array">一维 <see cref="Array"/>，它是从 <see cref="IContainer{TValue}"/> 复制的元素的目标。 <see cref="Array"/> 必须具有从零开始的索引</param>
		/// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制</param>
		/// <exception cref="ArgumentNullException">array 为 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">arrayIndex 小于 0</exception>
		/// <exception cref="ArgumentException">源中的元素数目 <see cref="IContainer{TValue}"/> 大于从的可用空间 arrayIndex 目标从头到尾 array</exception>
		public void CopyTo (KeyValuePair<string, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException ("array");
			}

			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException ("arrayIndex", "arrayIndex 不能小于 0");
			}

			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException ("arrayIndex 等于或大于数组的长度。");
			}

			if (this._sortedList.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException ("源 IContainer 中的元素数量大于从 arrayIndex 到目标数组末尾的可用空间");
			}

			int num = 0;
			foreach (KeyValuePair<string, TValue> item in this._sortedList)
			{
				array[arrayIndex + num] = item;
				num++;
			}
		}
		/// <summary>
		/// 返回一个循环访问集合的枚举器
		/// </summary>
		/// <returns>用于循环访问集合的枚举数</returns>
		public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator ()
		{
			return this._sortedList.GetEnumerator ();
		}
		/// <summary>
		/// 返回循环访问集合的枚举数
		/// </summary>
		/// <returns>一个可用于循环访问集合的 <see cref="IEnumerator"/> 对象</returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this._sortedList.GetEnumerator ();
		}
		/// <summary>
		/// 从 <see cref="IContainer{TValue}"/> 中移除带有指定键的元素
		/// </summary>
		/// <param name="key">要移除的元素的键</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <exception cref="NotSupportedException"><see cref="IContainer{TValue}"/> 为只读</exception>
		/// <returns>如果该元素已成功移除，则为 <see langword="true"/>；否则为 <see langword="false"/>。 如果在原始 <see langword="false"/> 中没有找到 key，则此方法也会返回 <see cref="IContainer{TValue}"/></returns>
		public bool Remove (string key)
		{
			return this._sortedList.Remove (key);
		}
		/// <summary>
		/// 从 <see cref="IContainer{TValue}"/> 中移除特定对象的第一个匹配项
		/// </summary>
		/// <param name="item">要从 <see cref="IContainer{TValue}"/> 中删除的对象</param>
		/// <returns>如果从 <see langword="true"/> 中成功移除 item，则为 <see cref="IContainer{TValue}"/>；否则为 <see langword="false"/>。<see langword="false"/> 中没有找到 item，该方法也会返回 <see cref="IContainer{TValue}"/></returns>
		public bool Remove (KeyValuePair<string, TValue> item)
		{
			return this.Remove (item.Key);
		}
		/// <summary>
		/// 获取与指定键关联的值
		/// </summary>
		/// <param name="key">要获取其值的键</param>
		/// <param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 value 参数的类型的默认值。 此参数未经初始化即被传递</param>
		/// <exception cref="ArgumentNullException">key 为 null</exception>
		/// <returns><see langword="true"/> 如果该对象的实现 <see cref="IContainer{TValue}"/> 包含具有指定的元素键; 否则为 <see langword="false"/>。</returns>
		public bool TryGetValue (string key, out TValue value)
		{
			return this._sortedList.TryGetValue (key, out value);
		}
		/// <summary>
		/// 确定此实例是否与另一个指定的 <see cref="IContainer{TValue}"/> 对象具有相同的值
		/// </summary>
		/// <param name="other">要与实例进行比较的 <see cref="IContainer{TValue}"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public bool Equals (IContainer<TValue> other)
		{
			return other != null && IContainer<TValue>.Equals (this, other);
		}
		/// <summary>
		/// 确定此实例是否与指定的对象（也必须是 <see cref="IContainer{TValue}"/> 对象）具有相同的值
		/// </summary>
		/// <param name="obj">要与实例进行比较的 <see cref="IContainer{TValue}"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as IContainer<TValue>);
		}
		/// <summary>
		/// 返回该实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this._sortedList.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			foreach (KeyValuePair<string, TValue> item in this)
			{
				builder.Append (item.Key);
				builder.Append ("=");
				if (item.Value == null)
				{
					builder.Append ("null");
				}
				else
				{
					builder.Append (item.Value.ToString ());
				}
				builder.AppendLine ();
			}
			return builder.ToString ();
		}
		#endregion
	}
}
