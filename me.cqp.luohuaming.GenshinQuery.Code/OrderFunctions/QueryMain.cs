using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GenshinPlayerQuery.Core;
using GenshinPlayerQuery.Model;
using me.cqp.luohuaming.GenshinQuery.Sdk.Cqp;
using me.cqp.luohuaming.GenshinQuery.Sdk.Cqp.EventArgs;
using me.cqp.luohuaming.GenshinQuery.PublicInfos;

namespace me.cqp.luohuaming.GenshinQuery.Code.OrderFunctions
{
    public class QueryMain : IOrderModel
    {
        public string GetOrderStr() => "#原神查询";

        public bool Judge(string destStr) => destStr.StartsWith(GetOrderStr());
        [STAThread]
        public FunctionResult Progress(CQGroupMessageEventArgs e)
        {
            FunctionResult result = new FunctionResult
            {
                Result = true,
                SendFlag = true,
            };
            SendText sendText = new SendText
            {
                SendID = e.FromGroup,
            };
            result.SendObject.Add(sendText);
            if (e.Message.Text.Trim().Length == GetOrderStr().Length)
            {
                sendText.MsgToSend.Add($"UID 不可为空哦~");
                return result;
            }

            long QQID = 0;
            if (long.TryParse(e.Message.Text.Substring(GetOrderStr().Length), out QQID) is false)
            {
                e.FromGroup.SendGroupMessage("请输入正确的UID格式");
            }
            e.FromGroup.SendGroupMessage("少女祈祷中……");
            Thread thread = new Thread(() =>
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(MainSave.ImageDirectory, "GenshinQuery"));
                    string filePath = Path.Combine("GenshinQuery", DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                    WebBrowser genShinweb = new WebBrowser();//创建WebBrowser对象
                    genShinweb.ScrollBarsEnabled = false;//关闭滚动条
                    genShinweb.Navigate("about:blank");//重新为创建WebBrowser对象导航navigate，否者html赋值不成功
                    if (MainSave.HasSnaped is false)
                    {
                        genShinweb.Document.Write(Properties.Resources.ResourceManager.GetString("example"));
                        Helper.SnapWeb(genShinweb);
                        MainSave.HasSnaped = true;
                    }
                    PlayerQueryResult playerQueryResult = GenshinApi.GetPlayerData(QQID.ToString(), "cn_gf01");//查询官服信息
                    if (!playerQueryResult.Success)
                    {
                        playerQueryResult = GenshinApi.GetPlayerData(QQID.ToString(), "cn_qd01");//查询渠道服信息
                        if (!playerQueryResult.Success)
                        {
                            e.FromGroup.SendGroupMessage("未查询到相关角色信息......");
                            return;
                        }
                        genShinweb.Document.Write(PlayerPageRender.RenderHtml(playerQueryResult));
                        Thread.Sleep(1500);
                        Bitmap bmp = Helper.SnapWeb(genShinweb);
                        bmp.Save(Path.Combine(MainSave.ImageDirectory, filePath));
                        bmp.Dispose();
                    }
                    else
                    {
                        genShinweb.Document.Write(PlayerPageRender.RenderHtml(playerQueryResult));
                        Thread.Sleep(1500);
                        Bitmap bmp = Helper.SnapWeb(genShinweb);
                        bmp.Save(Path.Combine(MainSave.ImageDirectory, filePath));
                        bmp.Dispose();
                    }
                    genShinweb.Dispose();
                    e.FromGroup.SendGroupMessage(CQApi.CQCode_Image(filePath).ToSendString());
                }
                catch (Exception ex)
                {
                    e.CQLog.Info("查询失败", ex.Message + ex.StackTrace);
                    e.FromGroup.SendGroupMessage($"查询失败，错误信息: {ex.Message}");
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return result;
        }

        public FunctionResult Progress(CQPrivateMessageEventArgs e)//私聊处理
        {
            throw new NotImplementedException();
        }
    }
}
