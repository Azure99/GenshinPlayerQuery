using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Attribute
{
	/// <summary>
	/// 表示配置项 (Ini) 文件节名称的特性
	/// </summary>
	[AttributeUsage (AttributeTargets.Class)]
	public sealed class IniSectionAttribute : System.Attribute
	{
		#region --字段--
		private string _sectionName;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置配置项 (Ini) 节名称
		/// </summary>
		public string SectionName
		{
			get { return this._sectionName; }
			set
			{
				if (string.IsNullOrEmpty (value))
				{
					throw new ArgumentException ("传入的字符串不允许用来设置节名称, 原因: 节名称不能为空", "value");
				}
				this._sectionName = value;
			}
		}
		#endregion
	}
}
