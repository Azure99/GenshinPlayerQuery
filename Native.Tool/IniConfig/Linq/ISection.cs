using Native.Tool.IniConfig.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 描述配置项 (Ini) 节点的类
	/// </summary>
	public sealed class ISection : IContainer<IValue>, IEquatable<ISection>
	{
		#region --字段--
		private readonly string _sectionName;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的节点名称
		/// </summary>
		public string SectionName { get { return this._sectionName; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该示例为空且具有默认的初始容量，并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		public ISection (string sectionName)
			: base ()
		{
			this._sectionName = sectionName;
		}
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该示例为空且具有指定的初始容量，并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		/// <param name="capacity"><see cref="IContainer{TValue}"/> 可包含的初始元素数</param>
		public ISection (string sectionName, int capacity)
			: base (capacity)
		{
			this._sectionName = sectionName;
		}
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该实例为空，具有默认的初始容量并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{String}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		public ISection (string sectionName, IComparer<string> comparer)
			: base (comparer)
		{
			this._sectionName = sectionName;
		}
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该实例包含从指定的 <see cref="IDictionary{String, IniValue}"/> 中复制的元素，其容量足以容纳所复制的元素数并使用默认的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		/// <param name="dictionary"><see cref="IDictionary{String, IniValue}"/>，它的元素被复制到新 <see cref="ISection"/></param>
		public ISection (string sectionName, IDictionary<string, IValue> dictionary)
			: base (dictionary)
		{
			this._sectionName = sectionName;
		}
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该实例为空，具有指定的初始容量并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		/// <param name="capacity"><see cref="ISection"/> 可包含的初始元素数</param>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{T}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		public ISection (string sectionName, int capacity, IComparer<string> comparer)
			: base (capacity, comparer)
		{
			this._sectionName = sectionName;
		}
		/// <summary>
		/// 初始化 <see cref="ISection"/> 类的新实例，该实例包含从指定的 <see cref="IDictionary{String, TValue}"/> 中复制的元素，其容量足以容纳所复制的元素数并使用指定的 <see cref="IComparer{String}"/>
		/// </summary>
		/// <param name="sectionName"><see cref="ISection"/> 使用的节名称</param>
		/// <param name="dictionary"><see cref="IDictionary{String, TValue}"/>，它的元素被复制到新 <see cref="IComparer{String}"/></param>
		/// <param name="comparer">在对键进行比较时使用的 <see cref="IComparer{String}"/> 实现。 - 或 - 为这类键使用默认的 null，则为 <see cref="IComparer{String}"/></param>
		public ISection (string sectionName, IDictionary<string, IValue> dictionary, IComparer<string> comparer)
			: base (dictionary, comparer)
		{
			this._sectionName = sectionName;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 确定此实例是否与另一个指定的 <see cref="ISection"/> 对象具有相同的值
		/// </summary>
		/// <param name="other">要与实例进行比较的 <see cref="ISection"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public bool Equals (ISection other)
		{
			return string.Equals (this._sectionName, other._sectionName) && base.Equals (other);
		}
		/// <summary>
		/// 确定此实例是否与指定的对象（也必须是 <see cref="ISection"/> 对象）具有相同的值
		/// </summary>
		/// <param name="obj">要与实例进行比较的 <see cref="ISection"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as ISection);
		}
		/// <summary>
		/// 返回该实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this._sectionName.GetHashCode () & base.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			return string.Format ("[{0}]{1}{2}", this.SectionName, Environment.NewLine, base.ToString ());
		}
		#endregion
	}
}
