using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using me.cqp.luohuaming.GenshinQuery.Code;
using Native.Sdk.Cqp.Interface;

namespace Native.Core
{
	/// <summary>
	/// 酷Q应用主入口类
	/// </summary>
	public class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="container">用于注册的 IOC 容器 </param>
		public static void Register (IUnityContainer unityContainer)
		{
			unityContainer.RegisterType<IGroupMessage, MainExport>("群消息处理");
			unityContainer.RegisterType<IPrivateMessage, MainExport>("私聊消息处理");
			unityContainer.RegisterType<ICQStartup, Event_StartUp>("酷Q启动事件");
		}
	}
}
