using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 描述 Ini 配置项值的类
	/// </summary>
	[Obsolete("请改用 IValue 类型")]
	public class IniValue : IEquatable<IniValue>, IComparable, IComparable<IniValue>, IConvertible
	{
		#region --字段--
		private string _value;
		/// <summary>
		/// 表示 IniValue 的, 此空值字段为只读
		/// </summary>
		public static readonly IniValue Empty = new Lazy<IniValue> (() => new IniValue (string.Empty)).Value;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置一个值, 该值指示 IniValue 在文件中的表示形式值
		/// </summary>
		public string Value { get { return this._value; } set { this._value = value; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (IniValue value)
			: this (value._value)
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (bool value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (byte value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (char value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (DateTime value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (decimal value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (double value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (short value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (int value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (long value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (sbyte value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (float value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (string value)
		{
			this._value = value;
		}
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (ushort value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (uint value)
			: this (value.ToString ())
		{ }
		/// <summary>
		/// 初始化 IniValue 实例对象
		/// </summary>
		/// <param name="value">用于初始化的值</param>
		public IniValue (ulong value)
			: this (value.ToString ())
		{ }
		#endregion

		#region --公开方法--
		/// <summary>
		/// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
		/// </summary>
		/// <param name="obj">与此实例进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。 返回值的含义如下： 值 含义 小于零 此实例在排序顺序中位于 other 之前。 零 此实例中出现的相同位置在排序顺序中是 other。 大于零 此实例在排序顺序中位于 other 之后。</returns>
		public int CompareTo (object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}
			if (!(obj is IniValue))
			{
				return 1;
			}
			else
			{
				IniValue value = obj as IniValue;
				return this._value.CompareTo (value._value);
			}
		}

		/// <summary>
		/// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
		/// </summary>
		/// <param name="other">与此实例进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。 返回值的含义如下： 值 含义 小于零 此实例在排序顺序中位于 other 之前。 零 此实例中出现的相同位置在排序顺序中是 other。 大于零 此实例在排序顺序中位于 other 之后。</returns>
		public int CompareTo (IniValue other)
		{
			if (other == null)
			{
				throw new ArgumentNullException ("other");
			}
			return this._value.CompareTo (other._value);
		}

		/// <summary>
		/// 指示当前 IniValue 对象是否等于 IniValue 的另一个对象。
		/// </summary>
		/// <param name="other">与此实例比较的另一个对象</param>
		/// <returns>如果当前 IniValue 对象等于 other 参数，则为 true；否则为 false。</returns>
		public bool Equals (IniValue other)
		{
			if (other == null)
			{
				return false;
			}
			return string.Compare (this._value, other._value) == 0;
		}

		/// <summary>
		/// 指示当前对象是否等于另一个对象。
		/// </summary>
		/// <returns>如果当前对象等于 obj 参数，则为 true；否则为 false。</returns>
		public TypeCode GetTypeCode ()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			return _value.GetTypeCode ();
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将逻辑值的指定字符串表示形式转换为其等效的布尔值。
		/// </summary>
		/// <returns>如果 true 等于该实例，则为 <see cref="System.Boolean.TrueString"/>；如果 false 等于该实例 或 <see cref="System.Boolean.FalseString"/>，则为 null</returns>
		public bool ToBoolean ()
		{
			return this.ToBoolean (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将逻辑值的指定字符串表示形式转换为其等效的布尔值。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>如果 true 等于该实例，则为 System.Boolean.TrueString；如果 false 等于该实例 或 System.Boolean.FalseString，则为 null</returns>
		public bool ToBoolean (bool defaultValue)
		{
			bool result;
			return bool.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将逻辑值的指定字符串表示形式转换为其等效的布尔值。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>如果 true 等于该实例，则为 System.Boolean.TrueString；如果 false 等于该实例 或 System.Boolean.FalseString，则为 null</returns>
		public bool ToBoolean (IFormatProvider provider)
		{
			return Convert.ToBoolean (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为 8 位无符号整数。
		/// </summary>
		/// <returns>一个与实例等效的 8 位无符号整数，如果该实例为 null，则为零。</returns>
		public byte ToByte ()
		{
			return this.ToByte (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为 8 位无符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例等效的 8 位无符号整数，如果该实例为 null，则为零。</returns>
		public byte ToByte (byte defaultValue)
		{
			byte result;
			return byte.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为 8 位无符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例等效的 8 位无符号整数，如果该实例为 null，则为零。</returns>
		public byte ToByte (IFormatProvider provider)
		{
			return Convert.ToByte (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定字符串的第一个字符转换为 Unicode 字符。
		/// </summary>
		/// <returns>与实例中第一个且仅有的字符等效的 Unicode 字符。</returns>
		public char ToChar ()
		{
			return this.ToChar (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定字符串的第一个字符转换为 Unicode 字符。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>与实例中第一个且仅有的字符等效的 Unicode 字符。</returns>
		public char ToChar (char defaultValue)
		{
			char result;
			return char.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定字符串的第一个字符转换为 Unicode 字符。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>与实例中第一个且仅有的字符等效的 Unicode 字符。</returns>
		public char ToChar (IFormatProvider provider)
		{
			return Convert.ToChar (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的日期和时间。
		/// </summary>
		/// <returns>实例的值的日期和时间等效项，如果 System.DateTime.MinValue 为该实例，则为 null 的日期和时间等效项。</returns>
		public DateTime ToDateTime ()
		{
			return this.ToDateTime (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的日期和时间。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>实例的值的日期和时间等效项，如果 System.DateTime.MinValue 为该实例，则为 null 的日期和时间等效项。</returns>
		public DateTime ToDateTime (DateTime defaultValue)
		{
			DateTime result;
			return DateTime.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的日期和时间。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>实例的值的日期和时间等效项，如果 System.DateTime.MinValue 为该实例，则为 null 的日期和时间等效项。</returns>
		public DateTime ToDateTime (IFormatProvider provider)
		{
			return Convert.ToDateTime (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为等效的十进制数。
		/// </summary>
		/// <returns>与实例等效的十进制数，如果实例值为 null，则为 0（零）。</returns>
		public decimal ToDecimal ()
		{
			return this.ToDecimal (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为等效的十进制数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>与实例等效的十进制数，如果实例值为 null，则为 0（零）。</returns>
		public decimal ToDecimal (decimal defaultValue)
		{
			decimal result;
			return decimal.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将指定对象的值转换为等效的十进制数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>与实例等效的十进制数，如果实例值为 null，则为 0（零）。</returns>
		public decimal ToDecimal (IFormatProvider provider)
		{
			return Convert.ToDecimal (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的双精度浮点数。
		/// </summary>
		/// <returns>与实例中数字等效的双精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public double ToDouble ()
		{
			return this.ToDouble (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的双精度浮点数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>与实例中数字等效的双精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public double ToDouble (double defaultValue)
		{
			double result;
			return double.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的双精度浮点数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>与实例中数字等效的双精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public double ToDouble (IFormatProvider provider)
		{
			return Convert.ToDouble (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位带符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 16 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public short ToInt16 ()
		{
			return this.ToInt16 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位带符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 16 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public short ToInt16 (short defaultValue)
		{
			short result;
			return short.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位带符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 16 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public short ToInt16 (IFormatProvider provider)
		{
			return Convert.ToInt16 (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位带符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 32 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public int ToInt32 ()
		{
			return this.ToInt32 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位带符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 32 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public int ToInt32 (int defaultValue)
		{
			int result;
			return int.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位带符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 32 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public int ToInt32 (IFormatProvider provider)
		{
			return Convert.ToInt32 (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位带符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 64 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public long ToInt64 ()
		{
			return this.ToInt64 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位带符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 64 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public long ToInt64 (long defaultValue)
		{
			long result;
			return long.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位带符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 64 位带符号整数，如果实例为 null，则为 0（零）。</returns>
		public long ToInt64 (IFormatProvider provider)
		{
			return Convert.ToInt64 (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 8 位带符号整数。
		/// </summary>
		/// <returns> 一个与实例等效的 8 位带符号整数。</returns>
		public sbyte ToSByte ()
		{
			return this.ToSByte (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 8 位带符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns> 一个与实例等效的 8 位带符号整数。</returns>
		public sbyte ToSByte (sbyte defaultValue)
		{
			sbyte result;
			return sbyte.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 8 位带符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns> 一个与实例等效的 8 位带符号整数。</returns>
		public sbyte ToSByte (IFormatProvider provider)
		{
			return Convert.ToSByte (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的单精度浮点数。
		/// </summary>
		/// <returns>与实例中数字等效的单精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public float ToSingle ()
		{
			return this.ToSingle (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的单精度浮点数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>与实例中数字等效的单精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public float ToSingle (float defaultValue)
		{
			float result;
			return float.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的单精度浮点数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>与实例中数字等效的单精度浮点数，如果实例为 null，则为 0（零）。</returns>
		public float ToSingle (IFormatProvider provider)
		{
			return Convert.ToSingle (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息将指定对象的值转换为其等效的字符串表示形式。
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return this.ToString (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息将指定对象的值转换为其等效的字符串表示形式。
		/// </summary>
		/// <param name="provider"> 一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns></returns>
		public string ToString (IFormatProvider provider)
		{
			return this._value;
		}

		/// <summary>
		/// 返回指定类型的对象，其值等效于指定对象。 参数提供区域性特定的格式设置信息。
		/// </summary>
		/// <param name="conversionType">要返回的对象的类型。</param>
		/// <returns>一个对象，其类型为 conversionType，并且其值等效于实例。 - 或 - 实例，前提是 System.Type 的实例和 conversionType 相等。 - 或 - 如果 Nothing 为实例，并且 null 不是值类型，则为空引用（在 Visual Basic中为 conversionType）。</returns>
		public object ToType (Type conversionType)
		{
			return ToType (conversionType, null);
		}

		/// <summary>
		/// 返回指定类型的对象，其值等效于指定对象。 参数提供区域性特定的格式设置信息。
		/// </summary>
		/// <param name="conversionType">要返回的对象的类型。</param>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个对象，其类型为 conversionType，并且其值等效于实例。 - 或 - 实例，前提是 System.Type 的实例和 conversionType 相等。 - 或 - 如果 Nothing 为实例，并且 null 不是值类型，则为空引用（在 Visual Basic中为 conversionType）。</returns>
		public object ToType (Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType (this._value, conversionType, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位无符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 16 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ushort ToUInt16 ()
		{
			return this.ToUInt16 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位无符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 16 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ushort ToUInt16 (ushort defaultValue)
		{
			ushort result;
			return ushort.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位无符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 16 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ushort ToUInt16 (IFormatProvider provider)
		{
			return Convert.ToUInt16 (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位无符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 32 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public uint ToUInt32 ()
		{
			return this.ToUInt32 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位无符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 32 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public uint ToUInt32 (uint defaultValue)
		{
			uint result;
			return uint.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位无符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 32 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public uint ToUInt32 (IFormatProvider provider)
		{
			return Convert.ToUInt32 (this._value, provider);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位无符号整数。
		/// </summary>
		/// <returns>一个与实例中数字等效的 64 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ulong ToUInt64 ()
		{
			return this.ToUInt64 (null);
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位无符号整数。
		/// </summary>
		/// <param name="defaultValue">若转换失败, 则返回该值</param>
		/// <returns>一个与实例中数字等效的 64 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ulong ToUInt64 (ulong defaultValue)
		{
			ulong result;
			return ulong.TryParse (this.Value, out result) ? result : defaultValue;
		}

		/// <summary>
		/// 使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位无符号整数。
		/// </summary>
		/// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
		/// <returns>一个与实例中数字等效的 64 位无符号整数，如果实例为 null，则为 0（零）。</returns>
		public ulong ToUInt64 (IFormatProvider provider)
		{
			return Convert.ToUInt64 (this._value, provider);
		}
		#endregion
	}
}
