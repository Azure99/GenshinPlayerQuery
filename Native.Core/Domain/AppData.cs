using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp;
using Unity;

namespace Native.Core.Domain
{
	public static class AppData
	{
		/// <summary>
		/// 获取当前 App 使用的 酷Q Api 接口实例
		/// </summary>
		public static CQApi CQApi { get; private set; }

		/// <summary>
		/// 获取当前 App 使用的 酷Q Log 接口实例
		/// </summary>
		public static CQLog CQLog { get; private set; }

		/// <summary>
		/// 获取当前 App 使用的依赖注入容器实例
		/// </summary>
		public static IUnityContainer UnityContainer { get; private set; }
	}
}
