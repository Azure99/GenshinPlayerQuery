using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Attribute
{
	/// <summary>
	/// 表示 IniConfig 在序列化时将表示为节的特性
	/// </summary>
	[Obsolete ("该类型跟随 IniConvert 类型过期")]
	[AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class IniConfigSectionAttribute : System.Attribute
	{
		/// <summary>
		/// 表示 IniConfig 中存储的节名称
		/// </summary>
		public string SectionName { get; set; }

		/// <summary>
		/// 初始化 <see cref="IniConfigSectionAttribute"/> 类的新实例
		/// </summary>
		public IniConfigSectionAttribute ()
			: this (null)
		{ }

		/// <summary>
		/// 初始化 <see cref="IniConfigSectionAttribute"/> 类的新实例
		/// </summary>
		/// <param name="sectionName">用于表示节点的节名称</param>
		public IniConfigSectionAttribute (string sectionName)
		{
			this.SectionName = sectionName;
		}
	}
}
