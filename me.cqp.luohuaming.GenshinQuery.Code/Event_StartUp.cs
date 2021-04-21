using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using me.cqp.luohuaming.GenshinQuery.Code.OrderFunctions;
using me.cqp.luohuaming.GenshinQuery.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.GenshinQuery.Sdk.Cqp.Interface;
using me.cqp.luohuaming.GenshinQuery.PublicInfos;

namespace me.cqp.luohuaming.GenshinQuery.Code
{
    public class Event_StartUp : ICQStartup
    {
        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            try
            {
                MainSave.AppDirectory = e.CQApi.AppDirectory;
                MainSave.CQApi = e.CQApi;
                MainSave.CQLog = e.CQLog;
                MainSave.ImageDirectory = CommonHelper.GetAppImageDirectory();
                //这里写处理逻辑
                MainSave.Instances.Add(new QueryMain());//这里需要将指令实例化填在这里
            }
            catch (Exception ex)
            {
                e.CQLog.Info("Error", ex.Message, ex.StackTrace);
            }
        }
    }
}
