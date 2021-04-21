using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Attribute
{
	/// <summary>
	/// 表示 IniConfig 在序列化时将表示为键的特性
	/// </summary>
	[Obsolete("该类型跟随 IniConvert 类型过期")]
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = false)]
	public sealed class IniConfigKeyAttribute : System.Attribute
	{
		/// <summary>
		/// 表示 IniConfig 文件中存储的键名称
		/// </summary>
		public string KeyName { get; set; }

		/// <summary>
		/// 初始化 <see cref="IniConfigKeyAttribute"/> 类的新实例
		/// </summary>
		public IniConfigKeyAttribute ()
			: this (null)
		{ }

		/// <summary>
		/// 初始化 <see cref="IniConfigKeyAttribute"/> 类的新实例
		/// </summary>
		/// <param name="keyName">用于表示键值对 "键" 的名称</param>
		public IniConfigKeyAttribute (string keyName)
		{
			this.KeyName = keyName;
		}
	}
}
