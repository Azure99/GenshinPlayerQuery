using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig.Attribute
{
	/// <summary>
	/// 表示配置项 (Ini) 文件键名称的特性
	/// </summary>
	[AttributeUsage (AttributeTargets.Property)]
	public sealed class IniKeyAttribute : System.Attribute
	{
		#region --字段--
		private string _keyName; 
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置配置项 (Ini) 键名称
		/// </summary>
		public string KeyName
		{
			get { return this._keyName; }
			set
			{
				if (string.IsNullOrEmpty (value))
				{
					throw new ArgumentException ("传入的字符串不允许用来设置键名称, 原因: 键名称不能为空", "value");
				}
				this._keyName = value;
			}
		} 
		#endregion
	}
}
