using System.Collections.Generic;
using System.Text;
using me.cqp.luohuaming.GenshinQuery.Sdk.Cqp.EventArgs;

namespace me.cqp.luohuaming.GenshinQuery.PublicInfos
{
    public interface IOrderModel
    {
        string GetOrderStr();
        bool Judge(string destStr);
        FunctionResult Progress(CQGroupMessageEventArgs e);
        FunctionResult Progress(CQPrivateMessageEventArgs e);
    }
}
