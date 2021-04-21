using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Native.Tool.IniConfig.Utilities;

namespace Native.Tool.IniConfig.Linq
{
	/// <summary>
	/// 描述配置项 (Ini) 值的类
	/// </summary>
	public sealed class IValue : IEquatable<IValue>, IComparable<IValue>, IComparable, ICloneable, IConvertible
	{
		#region --字段--
		private IValueType _valueType;
		private object _value;
		private static readonly IValueType[] ConvertBigIntegerTypes = new IValueType[]
		{
			IValueType.Integer,
			IValueType.Float,
			IValueType.String,
			IValueType.Boolean,
			IValueType.Bytes
		};
		private static readonly IValueType[] ConvertBooleanTypes = new IValueType[]
		{
			IValueType.Integer,
			IValueType.Float,
			IValueType.String,
			IValueType.Boolean
		};
		private static readonly IValueType[] ConvertBytesTypes = new IValueType[]
		{
			IValueType.Bytes,
			IValueType.String,
			IValueType.Integer
		};
		private static readonly IValueType[] ConvertCharTypes = new IValueType[]
		{
			IValueType.Integer,
			IValueType.Float,
			IValueType.String
		};
		private static readonly IValueType[] ConvertDateTimeTypes = new IValueType[]
		{
			IValueType.DateTime,
			IValueType.String,
		};
		private static readonly IValueType[] ConvertGuidTypes = new IValueType[]
		{
			IValueType.String,
			IValueType.Guid,
			IValueType.Bytes
		};
		private static readonly IValueType[] ConvertNumberTypes = new IValueType[]
		{
			IValueType.Integer,
			IValueType.Float,
			IValueType.String,
			IValueType.Boolean
		};
		private static readonly IValueType[] ConvertStringTypes = new IValueType[]
		{
			IValueType.Empty,
			IValueType.DateTime,
			IValueType.Integer,
			IValueType.Float,
			IValueType.String,
			IValueType.Boolean,
			IValueType.Bytes,
			IValueType.Guid,
			IValueType.TimeSpan,
			IValueType.Uri
		};
		private static readonly IValueType[] ConvertTimeSpanTypes = new IValueType[]
		{
			IValueType.String,
			IValueType.TimeSpan
		};
		private static readonly IValueType[] ConvertUirTypes = new IValueType[]
		{
			IValueType.String,
			IValueType.Uri
		};
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例描述值的类型
		/// </summary>
		public IValueType Type
		{
			get { return this._valueType; }
		}
		/// <summary>
		/// 获取当前实例描述的值
		/// </summary>
		public object Value
		{
			get { return this._value; }
			set
			{
				this._value = value;
				this._valueType = IValue.GetValueType (value);
			}
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		public IValue ()
			: this (null, IValueType.Empty)
		{ }
		/// <summary>
		/// 使用 <see cref="bool"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="bool"/> 对象</param>
		public IValue (bool value)
			: this (value, IValueType.Boolean)
		{ }
		/// <summary>
		/// 使用 <see cref="sbyte"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="sbyte"/> 对象</param>
		public IValue (sbyte value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="byte"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="byte"/> 对象</param>
		public IValue (byte value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="char"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="char"/> 对象</param>
		public IValue (char value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="short"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="short"/> 对象</param>
		public IValue (short value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="ushort"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="ushort"/> 值</param>
		public IValue (ushort value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="int"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="int"/> 值</param>
		public IValue (int value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="uint"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="uint"/> 值</param>
		public IValue (uint value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="long"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="long"/> 值</param>
		public IValue (long value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="ulong"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="ulong"/> 值</param>
		public IValue (ulong value)
			: this (value, IValueType.Integer)
		{ }
		/// <summary>
		/// 使用 <see cref="float"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="float"/> 值</param>
		public IValue (float value)
			: this (value, IValueType.Float)
		{ }
		/// <summary>
		/// 使用 <see cref="double"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="double"/> 值</param>
		public IValue (double value)
			: this (value, IValueType.Float)
		{ }
		/// <summary>
		/// 使用 <see cref="decimal"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="decimal"/> 值</param>
		public IValue (decimal value)
			: this (value, IValueType.Float)
		{ }
		/// <summary>
		/// 使用 <see cref="DateTime"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="DateTime"/> 值</param>
		public IValue (DateTime value)
			: this (value, IValueType.DateTime)
		{ }
		/// <summary>
		/// 使用 <see cref="DateTimeOffset"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="DateTimeOffset"/> 值</param>
		public IValue (DateTimeOffset value)
			: this (value, IValueType.DateTime)
		{ }
		/// <summary>
		/// 使用 <see cref="TimeSpan"/> 值来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="TimeSpan"/> 值</param>
		public IValue (TimeSpan value)
			: this (value, IValueType.TimeSpan)
		{ }
		/// <summary>
		/// 使用 <see cref="byte"/> 数组来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="byte"/> 数组</param>
		public IValue (byte[] value)
			: this (value, IValueType.Bytes)
		{ }
		/// <summary>
		/// 使用 <see cref="string"/> 对象来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="string"/> 对象</param>
		public IValue (string value)
			: this (value, IValueType.String)
		{ }
		/// <summary>
		/// 使用 <see cref="Guid"/> 对象来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="Guid"/> 对象</param>
		public IValue (Guid value)
			: this (value, IValueType.Guid)
		{ }
		/// <summary>
		/// 使用 <see cref="Uri"/> 对象来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个 <see cref="Uri"/> 对象</param>
		public IValue (Uri value)
			: this (value, IValueType.Uri)
		{ }
		/// <summary>
		/// 使用基础数据类型的可空对象来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">一个可空的基础数据类型对象</param>
		public IValue (object value)
			: this (value, IValue.GetValueType (value))
		{ }
		/// <summary>
		/// 使用指定的值和类型来初始化 <see cref="IValue"/> 类的新实例
		/// </summary>
		/// <param name="value">初始化的值</param>
		/// <param name="valueType">值的Ini类型</param>
		internal IValue (object value, IValueType valueType)
		{
			this._value = value;
			this._valueType = valueType;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 确定两个指定的 <see cref="IValue"/> 对象是否具有相同的值
		/// </summary>
		/// <param name="v1">要比较的第一个 <see cref="IValue"/>，或 <see langword="null"/></param>
		/// <param name="v2">要比较的第二个 <see cref="IValue"/>，或 <see langword="null"/></param>
		/// <returns>如果 <see langword="true"/> 的值与 v1 的值相同，则为 v2；否则为 <see langword="false"/>。 如果 v1 和 v2 均为 <see langword="null"/>，此方法将返回 <see langword="true"/></returns>
		public static bool Equals (IValue v1, IValue v2)
		{
			if (v1 == v2)
			{
				return true;
			}

			return v1 != null && v2 != null && v1._valueType == v2._valueType && IValue.Compare (v1, v2) == 0;
		}
		/// <summary>
		/// 获取当前实例的值, 转换为指定的类型. 若键不存在将返回 T 的默认值
		/// </summary>
		/// <typeparam name="T">转换目标值的类型</typeparam>
		/// <exception cref="InvalidOperationException">当前类型不是泛型类型。 也就是说，<see cref="Type.IsGenericType"/> 返回 <see langword="false"/></exception>
		/// <exception cref="NotSupportedException">基类不支持调用的方法。 派生类必须提供一个实现</exception>
		/// <exception cref="InvalidCastException">不支持此转换</exception>
		/// <exception cref="FormatException">转换的目标格式不是 provider 可识别的 T 的格式</exception>
		/// <exception cref="OverflowException">原始值表示不在 T 的范围内的数字</exception>
		/// <returns>获取关联的值并转换为目标类型</returns>
		public T GetValue<T> ()
		{
			return GetValueOrDefault<T> (default (T));
		}
		/// <summary>
		/// 获取当前实例的值, 转换为指定的类型. 若键不存在将返回 defaultValue
		/// </summary>
		/// <typeparam name="T">转换目标值的类型</typeparam>
		/// <param name="defaultValue">当键不存在时返回的默认值</param>
		/// <exception cref="InvalidOperationException">当前类型不是泛型类型。 也就是说，<see cref="Type.IsGenericType"/> 返回 <see langword="false"/></exception>
		/// <exception cref="NotSupportedException">基类不支持调用的方法。 派生类必须提供一个实现</exception>
		/// <exception cref="InvalidCastException">不支持此转换</exception>
		/// <exception cref="FormatException">转换的目标格式不是 provider 可识别的 T 的格式</exception>
		/// <exception cref="OverflowException">原始值表示不在 T 的范围内的数字</exception>
		/// <returns>获取关联的值并转换为目标类型</returns>
		public T GetValueOrDefault<T> (T defaultValue)
		{
			if (this.Value == null)
			{
				return defaultValue;
			}

			object objValue = this.Value;
			if (objValue is T)
			{
				return (T)objValue;
			}

			Type type = typeof (T);
			if (ReflectionUtils.IsNullableType (type))
			{
				if (objValue == null)
				{
					return defaultValue;
				}
				type = Nullable.GetUnderlyingType (type);
			}
			return (T)Convert.ChangeType (objValue, type, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 创建作为当前实例副本的新对象
		/// </summary>
		/// <returns>作为此实例副本的新对象</returns>
		public object Clone ()
		{
			return new IValue (this._value, this._valueType);
		}
		/// <summary>
		/// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
		/// </summary>
		/// <param name="obj">与此实例进行比较的对象</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。 返回值的含义如下： 值 含义 小于零 此实例在排序顺序中位于 obj 之前。 零 此实例在排序顺序中的同一位置中发生obj。大于零 此实例在排序顺序中位于 obj 之后。</returns>
		public int CompareTo (object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			IValue value = obj as IValue;
			if (value != null)
			{
				return IValue.Compare (this, value);
			}

			return Comparer<object>.Default.Compare (this._value, obj);
		}
		/// <summary>
		/// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
		/// </summary>
		/// <param name="other">与此实例进行比较的对象</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。 返回值的含义如下： 值 含义 小于零 此实例在排序顺序中位于 other 之前。 零 此实例中出现的相同位置在排序顺序中是other。 大于零 此实例在排序顺序中位于 other 之后。</returns>
		public int CompareTo (IValue other)
		{
			if (other == null)
			{
				return 1;
			}

			if (this._valueType == IValueType.String && this._valueType != other._valueType)
			{
				return IValue.Compare (other, this);
			}
			else
			{
				return IValue.Compare (this, other);
			}
		}
		/// <summary>
		/// 确定此实例是否与另一个指定的 <see cref="IValue"/> 对象具有相同的值
		/// </summary>
		/// <param name="other">要与实例进行比较的 <see cref="IValue"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public bool Equals (IValue other)
		{
			return other != null && IValue.Equals (this, other);
		}
		/// <summary>
		/// 确定此实例是否与指定的对象（也必须是 <see cref="IValue"/> 对象）具有相同的值
		/// </summary>
		/// <param name="obj">要与实例进行比较的 <see cref="IValue"/></param>
		/// <returns>如果 <see langword="true"/> 参数的值与此实例的值相同，则为 value；否则为 <see langword="false"/>。 如果 value 为 null，则此方法返回 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as IValue);
		}
		/// <summary>
		/// 返回该实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this._value.GetHashCode ();
		}
		/// <summary>
		/// 返回此实例的 <see cref="TypeCode"/>
		/// </summary>
		/// <returns>枚举的常数，它是 <see cref="TypeCode"/> 实现此接口的类或值类型</returns>
		public TypeCode GetTypeCode ()
		{
			if (object.Equals (this._value, null))
			{
				return TypeCode.Empty;
			}

			if (this._value is IConvertible)
			{
				return ((IConvertible)this._value).GetTypeCode ();
			}

			return TypeCode.Object;
		}
		/// <summary>
		/// 将此实例的值转换为等效的布尔值使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns>一个与此实例的值等效的布尔值</returns>
		public bool ToBoolean ()
		{
			return this.ToBoolean (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效的布尔值使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>一个与此实例的值等效的布尔值</returns>
		public bool ToBoolean (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效的 Unicode 字符使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns>一个与此实例的值等效的 Unicode 字符</returns>
		public char ToChar ()
		{
			return this.ToChar (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效的 Unicode 字符使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"> </param>
		/// <returns>一个与此实例的值等效的 Unicode 字符</returns>
		public char ToChar (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 8 位有符号整数
		/// </summary>
		/// <returns>为此实例的值等效的 8 位有符号的整数</returns>
		public sbyte ToSByte ()
		{
			return this.ToSByte (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 8 位有符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>为此实例的值等效的 8 位有符号的整数</returns>
		public sbyte ToSByte (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 8 位无符号整数
		/// </summary>
		/// <returns>为此实例的值等效的 8 位无符号的整数</returns>
		public byte ToByte ()
		{
			return this.ToByte (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 8 位无符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>为此实例的值等效的 8 位无符号的整数</returns>
		public byte ToByte (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 16 位有符号整数
		/// </summary>
		/// <returns>此实例的值等效的 16 位有符号的整数</returns>
		public short ToInt16 ()
		{
			return this.ToInt16 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 16 位有符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>此实例的值等效的 16 位有符号的整数</returns>
		public short ToInt16 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 16 位无符号整数
		/// </summary>
		/// <returns>为此实例的值等效的 16 位无符号的整数</returns>
		public ushort ToUInt16 ()
		{
			return this.ToUInt16 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 16 位无符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>为此实例的值等效的 16 位无符号的整数</returns>
		public ushort ToUInt16 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 32 位有符号整数
		/// </summary>
		/// <returns>此实例的值等效的 32 位有符号的整数</returns>
		public int ToInt32 ()
		{
			return this.ToInt32 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 32 位有符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>此实例的值等效的 32 位有符号的整数</returns>
		public int ToInt32 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效使用指定的区域性特定格式设置信息的 32 位无符号整数
		/// </summary>
		/// <returns>向此实例的值等效的 32 位无符号的整数</returns>
		public uint ToUInt32 ()
		{
			return this.ToUInt32 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效使用指定的区域性特定格式设置信息的 32 位无符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>向此实例的值等效的 32 位无符号的整数</returns>
		public uint ToUInt32 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 64 位有符号整数
		/// </summary>
		/// <returns>向此实例的值等效的 64 位有符号的整数</returns>
		public long ToInt64 ()
		{
			return this.ToInt64 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为使用指定的区域性特定格式设置信息的等效 64 位有符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>向此实例的值等效的 64 位有符号的整数</returns>
		public long ToInt64 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效使用指定的区域性特定格式设置信息的 64 位无符号整数
		/// </summary>
		/// <returns>向此实例的值等效的 64 位无符号的整数</returns>
		public ulong ToUInt64 ()
		{
			return this.ToUInt64 (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效使用指定的区域性特定格式设置信息的 64 位无符号整数
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>向此实例的值等效的 64 位无符号的整数</returns>
		public ulong ToUInt64 (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效单精度浮点数使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns>此实例的值等效的单精度浮点数</returns>
		public float ToSingle ()
		{
			return this.ToSingle (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效单精度浮点数使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>此实例的值等效的单精度浮点数</returns>
		public float ToSingle (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效双精度浮点数使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns>此实例的值等效的双精度浮点数</returns>
		public double ToDouble ()
		{
			return this.ToDouble (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效双精度浮点数使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>此实例的值等效的双精度浮点数</returns>
		public double ToDouble (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="decimal"/> 数字使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns>一个 <see cref="decimal"/> 的此实例的值等效的数</returns>
		public decimal ToDecimal ()
		{
			return this.ToDecimal (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="decimal"/> 数字使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns>一个 <see cref="decimal"/> 的此实例的值等效的数</returns>
		public decimal ToDecimal (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="DateTime"/> 使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns><see cref="DateTime"/> 的此实例的值等效的实例</returns>
		public DateTime ToDateTime ()
		{
			return this.ToDateTime (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="DateTime"/> 使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns><see cref="DateTime"/> 的此实例的值等效的实例</returns>
		public DateTime ToDateTime (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="string"/> 使用指定的区域性特定格式设置信息
		/// </summary>
		/// <returns><see cref="string"/> 的此实例的值等效的实例</returns>
		public override string ToString ()
		{
			return this.ToString (CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例的值转换为等效 <see cref="string"/> 使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns><see cref="string"/> 的此实例的值等效的实例</returns>
		public string ToString (IFormatProvider provider)
		{
			return this;
		}
		/// <summary>
		/// 将此实例与的值转换 <see cref="object"/> 指定 <see cref="System.Type"/>，具有等效值，使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="conversionType"><see cref="System.Type"/> 此实例的值转换为</param>
		/// <returns><see cref="object"/> 类型的实例 conversionType 其值等效于此实例的值</returns>
		public object ToType (Type conversionType)
		{
			return Convert.ChangeType (this._value, conversionType, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 将此实例与的值转换 <see cref="object"/> 指定 <see cref="System.Type"/>，具有等效值，使用指定的区域性特定格式设置信息
		/// </summary>
		/// <param name="conversionType"><see cref="System.Type"/> 此实例的值转换为</param>
		/// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定格式设置信息</param>
		/// <returns><see cref="object"/> 类型的实例 conversionType 其值等效于此实例的值</returns>
		public object ToType (Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType (this._value, conversionType, provider);
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 获取值属于的 Ini 值类型
		/// </summary>
		/// <param name="value">获取类型的值</param>
		/// <exception cref="ArgumentException"/>
		/// <returns>返回 <see cref="IValueType"/> 枚举的值</returns>
		private static IValueType GetValueType (object value)
		{
			if (value == null)
			{
				return IValueType.Empty;
			}

			if (value is bool)
			{
				return IValueType.Boolean;
			}

			if (value is sbyte || value is byte || value is char || value is short || value is ushort || value is int || value is uint || value is long || value is ulong)
			{
				return IValueType.Integer;
			}

			if (value is float || value is double || value is decimal)
			{
				return IValueType.Float;
			}

			if (value is DateTime || value is DateTimeOffset)
			{
				return IValueType.DateTime;
			}

			if (value is TimeSpan)
			{
				return IValueType.TimeSpan;
			}

			if (value is byte[])
			{
				return IValueType.Bytes;
			}

			if (value is string)
			{
				return IValueType.String;
			}

			if (value is Guid)
			{
				return IValueType.Guid;
			}

			if (value is Uri)
			{
				return IValueType.Uri;
			}

			throw new ArgumentException (string.Format ("无法确定类型为 Ini 可描述的类型 {0}", value.GetType ().Name));
		}
		/// <summary>
		/// 获取是否可以转换到目标类型
		/// </summary>
		/// <param name="sourceType">源类型</param>
		/// <param name="targetTypes">目标类型</param>
		/// <param name="nullable">是否可为null</param>
		/// <returns>可以转换则为 <see langword="true"/> 否则为 <see langword="false"/></returns>
		private static bool IsConvert (IValueType sourceType, IValueType[] targetTypes, bool nullable)
		{
			return Array.IndexOf<IValueType> (targetTypes, sourceType) != -1 || (nullable && (sourceType == IValueType.Empty));
		}
		/// <summary>
		/// 比较两个指定的 <see cref="IValue"/> 对象，并返回一个指示二者在排序顺序中的相对位置的整数
		/// </summary>
		/// <param name="valueA">要比较的第一个对象</param>
		/// <param name="valueB">要比较的第二个对象</param>
		/// <returns>一个 32 位带符号整数，指示两个比较数之间的关系。</returns>
		private static int Compare (IValue valueA, IValue valueB)
		{
			#region --引用判断--
			if (valueA == valueB)
			{
				return 0;
			}
			if (valueB == null)
			{
				return 1;
			}
			if (valueA == null)
			{
				return -1;
			}
			#endregion

			#region --比较数据--
			switch (valueA._valueType)
			{
				case IValueType.String:
					{
						string strA = Convert.ToString (valueA._value, CultureInfo.InvariantCulture);
						string strB = Convert.ToString (valueB._value, CultureInfo.InvariantCulture);
						return string.CompareOrdinal (strA, strB);
					}
				case IValueType.Integer:
					{
						if (valueA._value is BigInteger)
						{
							BigInteger numA = (BigInteger)valueA._value;
							return IValue.CompareBigInteger (numA, valueB._value);
						}
						if (valueB._value is BigInteger)
						{
							BigInteger numB = (BigInteger)valueB._value;
							return -IValue.CompareBigInteger (numB, valueA._value);
						}
						if (valueA._value is ulong || valueB._value is ulong || valueA._value is decimal || valueB._value is decimal)
						{
							decimal decimalA = Convert.ToDecimal (valueA._value, CultureInfo.InvariantCulture);
							decimal decimalB = Convert.ToDecimal (valueB._value, CultureInfo.InvariantCulture);
							return decimalA.CompareTo (decimalB);
						}
						if (valueA._value is float || valueB._value is float || valueA._value is double || valueB._value is double)
						{
							return IValue.CompareFloat (valueA._value, valueB._value);
						}
						long l1 = Convert.ToInt64 (valueA._value, CultureInfo.InvariantCulture);
						long l2 = Convert.ToInt64 (valueB._value, CultureInfo.InvariantCulture);
						return l1.CompareTo (l2);
					}
				case IValueType.Float:
					{
						if (valueA._value is BigInteger)
						{
							BigInteger numA = (BigInteger)valueA._value;
							return IValue.CompareBigInteger (numA, valueB._value);
						}
						if (valueB._value is BigInteger)
						{
							BigInteger numB = (BigInteger)valueB._value;
							return -IValue.CompareBigInteger (numB, valueA._value);
						}
						if (valueA._value is ulong || valueB._value is ulong || valueA._value is decimal || valueB._value is decimal)
						{
							decimal decimalA = Convert.ToDecimal (valueA._value, CultureInfo.InvariantCulture);
							decimal decimalB = Convert.ToDecimal (valueB._value, CultureInfo.InvariantCulture);
							return decimalA.CompareTo (decimalB);
						}
						return IValue.CompareFloat (valueA._value, valueB._value);
					}
				case IValueType.Boolean:
					{
						bool b1 = Convert.ToBoolean (valueA._value, CultureInfo.InvariantCulture);
						bool b2 = Convert.ToBoolean (valueB._value, CultureInfo.InvariantCulture);
						return b1.CompareTo (b2);
					}
				case IValueType.DateTime:
					{
						if (valueA._value is DateTime)
						{
							DateTime dtA = (DateTime)valueA._value;
							DateTime dtB;
							if (valueB._value is DateTimeOffset)
							{
								dtB = ((DateTimeOffset)valueB._value).DateTime;
							}
							else
							{
								dtB = Convert.ToDateTime (valueB._value, CultureInfo.InvariantCulture);
							}
							return dtA.CompareTo (dtB);
						}
						DateTimeOffset offsetA = (DateTimeOffset)valueA._value;
						DateTimeOffset offsetB;
						if (valueB._value is DateTimeOffset)
						{
							offsetB = (DateTimeOffset)valueB._value;
						}
						else
						{
							offsetB = new DateTimeOffset (Convert.ToDateTime (valueB._value, CultureInfo.InvariantCulture));
						}
						return offsetA.CompareTo (offsetB);
					}
				case IValueType.TimeSpan:
					{
						if (!(valueB._value is TimeSpan))
						{
							throw new ArgumentException ("对象必须是 TimeSpan 类型");
						}
						TimeSpan span1 = (TimeSpan)valueA._value;
						TimeSpan span2 = (TimeSpan)valueB._value;
						return span1.CompareTo (span2);
					}
				case IValueType.Bytes:
					{
						byte[] bytesA = valueA._value as byte[];
						byte[] bytesB = valueB._value as byte[];
						if (bytesB == null)
						{
							throw new ArgumentException ("对象必须是 byte[] 类型");
						}
						return MiscellaneousUtils.ByteArrayCompare (bytesA, bytesB);
					}
				case IValueType.Uri:
					{
						Uri uriA = (Uri)valueA._value;
						Uri uriB = valueB._value as Uri;
						if (uriB == null)
						{
							throw new ArgumentException ("对象必须是 Uri 类型");
						}
						return Comparer<string>.Default.Compare (uriA.ToString (), uriB.ToString ());
					}
				case IValueType.Guid:
					{
						if (!(valueB._value is Guid))
						{
							throw new ArgumentException ("对象必须是 Guid 类型");
						}
						Guid guidA = (Guid)valueA._value;
						Guid guidB = (Guid)valueB._value;
						return guidA.CompareTo (guidB);
					}
			}
			#endregion

			throw new ArgumentException ("无法比较 valueA 和 valueB, 因为遇到意外的值类型");
		}
		/// <summary>
		/// 比较 <see cref="BigInteger"/> 和 <see cref="object"/>, 并返回一个指示二者在排序顺序中的相对位置的整数
		/// </summary>
		/// <param name="intA">要比较的 <see cref="BigInteger"/></param>
		/// <param name="objB">要比较的 <see cref="object"/></param>
		/// <returns>一个 32 位带符号整数，指示两个比较数之间的关系。</returns>
		private static int CompareBigInteger (BigInteger intA, object objB)
		{
			int num = intA.CompareTo (ConvertUtils.ToBigInteger (objB));
			if (num != 0)
			{
				return num;
			}
			if (objB is decimal)
			{
				decimal num2 = (decimal)objB;
				return 0m.CompareTo (Math.Abs (num2 - Math.Truncate (num2)));
			}
			if (objB is double || objB is float)
			{
				double num3 = Convert.ToDouble (objB, CultureInfo.InvariantCulture);
				return 0d.CompareTo (Math.Abs (num3 - Math.Truncate (num3)));
			}
			return num;
		}
		/// <summary>
		/// 比较两个指定的 Float, 并返回一个指示二者在排序顺序中的相对位置的整数
		/// </summary>
		/// <param name="v1">要比较的第一个浮点数</param>
		/// <param name="v2">要比较的第二个浮点数</param>
		/// <returns>一个 32 位带符号整数，指示两个比较数之间的关系。</returns>
		private static int CompareFloat (object v1, object v2)
		{
			double d = Convert.ToDouble (v1, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble (v2, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals (d, num))
			{
				return 0;
			}
			return d.CompareTo (num);
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="bool"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="bool"/> 对象</param>
		public static implicit operator IValue (bool value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="sbyte"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="sbyte"/> 对象</param>
		public static implicit operator IValue (sbyte value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="byte"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="byte"/> 对象</param>
		public static implicit operator IValue (byte value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="char"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="char"/> 对象</param>
		public static implicit operator IValue (char value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="short"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="short"/> 对象</param>
		public static implicit operator IValue (short value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="ushort"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="ushort"/> 对象</param>
		public static implicit operator IValue (ushort value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="int"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="int"/> 对象</param>
		public static implicit operator IValue (int value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="uint"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="uint"/> 对象</param>
		public static implicit operator IValue (uint value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="long"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="long"/> 对象</param>
		public static implicit operator IValue (long value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="ulong"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="ulong"/> 对象</param>
		public static implicit operator IValue (ulong value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="float"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="float"/> 对象</param>
		public static implicit operator IValue (float value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="double"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="double"/> 对象</param>
		public static implicit operator IValue (double value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="decimal"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="decimal"/> 对象</param>
		public static implicit operator IValue (decimal value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="DateTime"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="DateTime"/> 对象</param>
		public static implicit operator IValue (DateTime value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="DateTimeOffset"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="DateTimeOffset"/> 对象</param>
		public static implicit operator IValue (DateTimeOffset value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="TimeSpan"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="TimeSpan"/> 对象</param>
		public static implicit operator IValue (TimeSpan value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="byte"/> 数组转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="byte"/> 数组</param>
		public static implicit operator IValue (byte[] value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="string"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="string"/> 对象</param>
		public static implicit operator IValue (string value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="Guid"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="Guid"/> 对象</param>
		public static implicit operator IValue (Guid value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="Uri"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="Uri"/> 对象</param>
		public static implicit operator IValue (Uri value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="bool"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="bool"/> 对象</param>
		public static implicit operator IValue (bool? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="sbyte"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="sbyte"/> 对象</param>
		public static implicit operator IValue (sbyte? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="byte"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="byte"/> 对象</param>
		public static implicit operator IValue (byte? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="char"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="char"/> 对象</param>
		public static implicit operator IValue (char? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="short"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="short"/> 对象</param>
		public static implicit operator IValue (short? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="ushort"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="ushort"/> 对象</param>
		public static implicit operator IValue (ushort? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="int"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="int"/> 对象</param>
		public static implicit operator IValue (int? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="uint"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="uint"/> 对象</param>
		public static implicit operator IValue (uint? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="long"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="long"/> 对象</param>
		public static implicit operator IValue (long? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="ulong"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="ulong"/> 对象</param>
		public static implicit operator IValue (ulong? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="float"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="float"/> 对象</param>
		public static implicit operator IValue (float? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="double"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="double"/> 对象</param>
		public static implicit operator IValue (double? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="decimal"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="decimal"/> 对象</param>
		public static implicit operator IValue (decimal? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="DateTime"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="DateTime"/> 对象</param>
		public static implicit operator IValue (DateTime? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="DateTimeOffset"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="DateTimeOffset"/> 对象</param>
		public static implicit operator IValue (DateTimeOffset? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="TimeSpan"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="TimeSpan"/> 对象</param>
		public static implicit operator IValue (TimeSpan? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="Guid"/> 转换为 <see cref="IValue"/> 对象
		/// </summary>
		/// <param name="value">一个 <see cref="Guid"/> 对象</param>
		public static implicit operator IValue (Guid? value)
		{
			return new IValue (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="bool"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator bool (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertBooleanTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Boolean", ivalue.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return Convert.ToBoolean ((int)ivalue);
			}

			return Convert.ToBoolean (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="sbyte"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator sbyte (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 SByte", ivalue.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (sbyte)value2;
			}

			return Convert.ToSByte (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="byte"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator byte (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Byte", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (byte)value2;
			}

			return Convert.ToByte (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="char"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator char (IValue value)
		{
			object ivalue = value._value;
			if (ivalue != null || !IValue.IsConvert (value._valueType, IValue.ConvertCharTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Char", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (char)(ushort)value2;
			}

			return Convert.ToChar (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="short"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator short (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Int16", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (short)value2;
			}

			return Convert.ToInt16 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="ushort"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator ushort (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 UInt16", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (ushort)value2;
			}

			return Convert.ToUInt16 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="int"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator int (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Int32", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (int)value2;
			}

			return Convert.ToInt32 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="uint"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator uint (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 UInt32", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (uint)value2;
			}

			return Convert.ToUInt32 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator long (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Int64", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (long)value2;
			}

			return Convert.ToInt64 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="ulong"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator ulong (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 UInt64", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (ulong)value2;
			}

			return Convert.ToUInt64 (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="float"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator float (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Single", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (float)value2;
			}

			return Convert.ToSingle (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="double"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator double (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Double", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (double)value2;
			}

			return Convert.ToDouble (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="decimal"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator decimal (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertNumberTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Decimal", value._value.GetType ().Name));
			}

			if (ivalue is BigInteger)
			{
				BigInteger value2 = (BigInteger)ivalue;
				return (decimal)value2;
			}

			return Convert.ToDecimal (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="DateTime"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator DateTime (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertDateTimeTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 DateTime", value._value.GetType ().Name));
			}

			if (ivalue is DateTimeOffset)
			{
				return ((DateTimeOffset)ivalue).DateTime;
			}

			return Convert.ToDateTime (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="DateTimeOffset"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator DateTimeOffset (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertDateTimeTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 DateTimeOffset", value._value.GetType ().Name));
			}

			if (ivalue is DateTimeOffset)
			{
				return (DateTimeOffset)ivalue;
			}

			string input = ivalue as string;
			if (input != null)
			{
				return DateTimeOffset.Parse (input, CultureInfo.InvariantCulture);
			}

			return new DateTimeOffset (Convert.ToDateTime (ivalue, CultureInfo.InvariantCulture));
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="TimeSpan"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator TimeSpan (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertTimeSpanTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 TimeSpan", value._value.GetType ().Name));
			}

			if (value._value is TimeSpan)
			{
				return (TimeSpan)value._value;
			}

			return ConvertUtils.StringToTimeSpan (Convert.ToString (ivalue, CultureInfo.InvariantCulture));
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="byte"/> 数组
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator byte[] (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertBytesTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 byte[]", value._value.GetType ().Name));
			}

			if (ivalue is string)
			{
				return Convert.FromBase64String (Convert.ToString (ivalue, CultureInfo.InvariantCulture));
			}

			if (ivalue is BigInteger)
			{
				return ((BigInteger)ivalue).ToByteArray ();
			}

			byte[] result = ivalue as byte[];
			if (result != null)
			{
				return result;
			}

			throw new ArgumentException (string.Format ("无法将 {0} 转换为 byte[]", value._value.GetType ().Name));
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator string (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			object ivalue = value._value;
			if (!IValue.IsConvert (value._valueType, IValue.ConvertStringTypes, true))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 String", value._value.GetType ().Name));
			}

			if (ivalue == null)
			{
				return null;
			}

			byte[] inArray = ivalue as byte[];
			if (inArray != null)
			{
				return Convert.ToBase64String (inArray);
			}

			if (ivalue is BigInteger)
			{
				return ((BigInteger)ivalue).ToString (CultureInfo.InvariantCulture);
			}

			return Convert.ToString (ivalue, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="Guid"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator Guid (IValue value)
		{
			object ivalue = value._value;
			if (ivalue == null || !IValue.IsConvert (value._valueType, IValue.ConvertGuidTypes, false))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Guid", value._value.GetType ().Name));
			}

			byte[] inArray = ivalue as byte[];
			if (inArray != null)
			{
				return new Guid (inArray);
			}

			if (ivalue is Guid)
			{
				return (Guid)ivalue;
			}

			return new Guid (Convert.ToString (ivalue, CultureInfo.InvariantCulture));
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="Uri"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator Uri (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			object ivalue = value._value;
			if (!IValue.IsConvert (value._valueType, IValue.ConvertUirTypes, true))
			{
				throw new ArgumentException (string.Format ("无法将 {0} 转换为 Uri", value._value.GetType ().Name));
			}

			if (ivalue == null)
			{
				return null;
			}

			Uri result = ivalue as Uri;
			if (result == null)
			{
				return new Uri (Convert.ToString (ivalue, CultureInfo.InvariantCulture));
			}
			return result;
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="bool"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator bool? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new bool? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="sbyte"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator sbyte? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new sbyte? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="byte"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator byte? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new byte? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="char"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator char? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new char? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="short"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator short? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new short? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="ushort"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator ushort? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new ushort? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="int"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator int? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new int? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="uint"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator uint? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new uint? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator long? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new long? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="ulong"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator ulong? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new ulong? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="float"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator float? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new float? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="double"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator double? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new double? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="decimal"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator decimal? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new decimal? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="DateTime"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator DateTime? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new DateTime? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="DateTimeOffset"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator DateTimeOffset? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new DateTimeOffset? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="TimeSpan"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator TimeSpan? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new TimeSpan? (value);
		}
		/// <summary>
		/// 定义将 <see cref="IValue"/> 对象转换为 <see cref="Guid"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="IValue"/> 对象</param>
		public static implicit operator Guid? (IValue value)
		{
			if (value == null)
			{
				return null;
			}

			if (value._value == null)
			{
				return null;
			}

			return new Guid? (value);
		}
		#endregion
	}
}
