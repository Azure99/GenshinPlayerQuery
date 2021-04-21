using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Native.Sdk.Cqp.Core;
using Native.Sdk.Cqp.Interface;

namespace Native.Sdk.Cqp.Expand
{
	/// <summary>
	/// 其它类扩展方法集
	/// </summary>
	public static class SystemExpand
	{
		/// <summary>
		/// 获取 Unix 时间戳的 <see cref="DateTime"/> 表示形式
		/// </summary>
		/// <param name="unixTime">unix 时间戳</param>
		/// <returns></returns>
		public static DateTime ToDateTime (this int unixTime)
		{
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970, 1, 1));
			TimeSpan toNow = new TimeSpan (long.Parse (string.Format ("{0}0000000", unixTime)));
			DateTime daTime = dtStart.Add (toNow);
			return daTime;
		}

		/// <summary>
		/// 获取当前对象的 <see cref="GCHandle"/> 实例, 该实例为 <see cref="GCHandleType.Pinned"/> 类型
		/// </summary>
		/// <param name="source">将转换的对象</param>
		/// <param name="encoding">转换的编码</param>
		/// <returns></returns>
		public static GCHandle GetStringGCHandle (this string source, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.Default;
			}
			
			byte[] buffer = encoding.GetBytes (source);
			return GCHandle.Alloc (buffer, GCHandleType.Pinned);
		}

		/// <summary>
		/// 读取指针内所有的字节数组并编码为指定字符串
		/// </summary>
		/// <param name="strPtr">字符串的 <see cref="IntPtr"/> 对象</param>
		/// <param name="encoding">目标编码格式</param>
		/// <returns></returns>
		public static string ToString (this IntPtr strPtr, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.Default;
			}

			int len = Kernel32.LstrlenA (strPtr);   //获取指针中数据的长度
			if (len == 0)
			{
				return string.Empty;
			}
			byte[] buffer = new byte[len];
			Marshal.Copy (strPtr, buffer, 0, len);
			return encoding.GetString (buffer);
		}

		/// <summary>
		/// 将对象转换为可发送的字符串, 如果待转换的对象继承自 <see cref="IToSendString"/> 将使用该接口的方法获取字符串
		/// </summary>
		/// <param name="objects">消息参数</param>
		/// <exception cref="ArgumentNullException">发现有 null 参数</exception>
		/// <returns>可发送的字符串</returns>
		public static string ToSendString (this object[] objects)
		{
			StringBuilder builder = new StringBuilder ();
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] == null)
				{
					throw new ArgumentNullException ("objects", string.Format ("第 {0} 个成员为 null", i));
				}

				IToSendString sendString = objects[i] as IToSendString;
				if (sendString != null)
				{
					builder.Append (sendString.ToSendString ());
				}
				else
				{
					builder.Append (objects[i].ToString ());
				}
			}
			return builder.ToString ();
		}

		/// <summary>
		/// 读取 <see cref="System.Enum"/> 标记 <see cref="System.ComponentModel.DescriptionAttribute"/> 的值
		/// </summary>
		/// <param name="value">原始 <see cref="System.Enum"/> 值</param>
		/// <returns></returns>
		public static string GetDescription (this System.Enum value)
		{
			if (value == null)
			{
				return string.Empty;
			}

			FieldInfo fieldInfo = value.GetType ().GetField (value.ToString ());
			DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute> (false);
			return attribute.Description;
		}
	}
}
