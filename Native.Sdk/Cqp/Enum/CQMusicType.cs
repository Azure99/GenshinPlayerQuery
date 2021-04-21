using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Enum
{
    /// <summary>
    /// 指示点歌时使用的音乐来源
    /// </summary>
    [DefaultValue (CQMusicType.Tencent)]
    public enum CQMusicType
    {
        /// <summary>
        /// QQ 音乐
        /// </summary>
        [Description ("qq")]
        Tencent,
        /// <summary>
        /// 网易云音乐
        /// </summary>
        [Description ("163")]
        Netease,
        /// <summary>
        /// 虾米音乐
        /// </summary>
        [Description ("xiami")]
        XiaMi
    }
}
