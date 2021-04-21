using Native.Tool.IniConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Exception
{
	/// <summary>
	/// 表示属性找不到的异常
	/// </summary>
	[Obsolete ("该类型跟随 IniConfigException 类型过期")]
	public sealed class PropertyNotFoundException : IniConfigException
	{
		/// <summary>
		/// 初始化 <see cref="PropertyNotFoundException"/> 类的新实例
		/// </summary>
		public PropertyNotFoundException ()
		{
		}

		/// <summary>
		/// 用指定的错误消息初始化 <see cref="PropertyNotFoundException"/> 类的新实例
		/// </summary>
		/// <param name="message">描述错误的消息</param>
		public PropertyNotFoundException (string message) 
			: base (message)
		{
		}

		/// <summary>
		/// 使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="PropertyNotFoundException"/> 类的新实例
		/// </summary>
		/// <param name="message">解释异常原因的错误消息</param>
		/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）</param>
		public PropertyNotFoundException (string message, System.Exception innerException) 
			: base (message, innerException)
		{
		}
	}
}
