using System.Collections.Generic;

namespace PublicInfos
{
    /// <summary>
    /// 函数处理结果
    /// </summary>
    public class FunctionResult
    {
        /// <summary>
        /// 用于发送的文本
        /// </summary>
        public List<SendText> SendObject { get; set; } = new List<SendText>();
        /// <summary>
        /// 标志本次函数处理阻塞或者是继续
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 发送消息标识
        /// </summary>
        public bool SendFlag { get; set; }
    }
    public class SendText
    {
        /// <summary>
        /// 每消息执行一次发送
        /// </summary>
        public List<string> MsgToSend { get; set; } = new List<string>();
        /// <summary>
        /// 发送对方的群号(或者是QQ号(请忽略，未实现))
        /// </summary>
        public long SendID { get; set; }
        /// <summary>
        /// 处理情况,作用自己发挥
        /// </summary>
        public bool HandlingFlag { get; set; } = true;
    }
}
