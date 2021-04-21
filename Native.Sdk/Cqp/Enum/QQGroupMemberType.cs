using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Native.Sdk.Cqp.Enum
{
    /// <summary>
    /// 表示群成员对于所在群的群成员类型
    /// </summary>
    public enum QQGroupMemberType
    {
        /// <summary>
        /// 成员
        /// </summary>
        [Description ("成员")]
        Member = 1,
        /// <summary>
        /// 管理
        /// </summary>
        [Description ("管理员")]
        Manage = 2,
        /// <summary>
        /// 群主
        /// </summary>
        [Description ("群主")]
        Creator = 3
    }
}
