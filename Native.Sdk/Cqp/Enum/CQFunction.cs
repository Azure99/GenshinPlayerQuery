using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Native.Sdk.Cqp.Enum
{
    /// <summary>
    /// 表示酷Q消息中内含 [CQ:...] 中的类型
    /// </summary>
    [DefaultValue (CQFunction.Unknown)]
    public enum CQFunction
    {
        /// <summary>
        /// 未知类型, 同时也是默认值
        /// </summary>
        [Description ("unknown")]
        Unknown,
        /// <summary>
        /// QQ表情
        /// </summary>
        [Description ("face")]
        Face,
        /// <summary>
        /// Emoji表情
        /// </summary>
        [Description ("emoji")]
        Emoji,
        /// <summary>
        /// 原创表情
        /// </summary>
        [Description ("bface")]
        Bface,
        /// <summary>
        /// 小表情
        /// </summary>
        [Description ("sface")]
        Sface,
        /// <summary>
        /// 图片
        /// </summary>
        [Description ("image")]
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        [Description ("record")]
        Record,
        /// <summary>
        /// At默认
        /// </summary>
        [Description ("at")]
        At,
        /// <summary>
        /// 猜拳魔法表情
        /// </summary>
        [Description ("rps")]
        Rps,
        /// <summary>
        /// 掷骰子魔法表情
        /// </summary>
        [Description ("dice")]
        Dice,
        /// <summary>
        /// 戳一戳
        /// </summary>
        [Description ("shake")]
        Shake,
        /// <summary>
        /// 音乐
        /// </summary>
        [Description ("music")]
        Music,
        /// <summary>
        /// 链接分享
        /// </summary>
        [Description ("share")]
        Share,
        /// <summary>
        /// 卡片消息
        /// </summary>
        [Description ("rich")]
        Rich,
        /// <summary>
        /// 签到
        /// </summary>
        [Description ("sign")]
        Sign,
        /// <summary>
        /// 红包
        /// </summary>
        [Description ("hb")]
        Hb,
        /// <summary>
        /// 推荐
        /// </summary>
        [Description ("contact")]
        Contact,
        /// <summary>
        /// 厘米秀
        /// </summary>
        [Description ("show")]
        Show,
        /// <summary>
        /// 位置分享
        /// </summary>
        [Description ("location")]
        Location,
        /// <summary>
        /// 匿名消息
        /// </summary>
        [Description ("anonymous")]
        Anonymous
    }
}
