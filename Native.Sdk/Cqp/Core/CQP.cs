using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.Sdk.Cqp.Core
{
    internal static class CQP
    {
        /* 
		 * 官方SDK更新日志:
		 * 
         *  V9 应用机制内的变动（即 V9.x 内的变动）通常**不会**影响已发布的应用，但有可能需要开发者在更新旧版本应用时，对代码进行细微调整。
         *  
         *  V9.25 (2019-10-8)
         *  -----------------
         *  新增 取群信息 Api
         *   * 支持获取群当前人数与人数上限。如果您此前使用其他方式获取，建议迁移至本接口。
         *   
         *  新增 群禁言事件
         *  
         *  V9.23 (2019-9-3)
         *  ----------------
         *  新增 扩展 Cookies 适用范围
         *   * 获取 Cookies 时，填写 Cookies 将要使用的域名，如 api.example.com，可以获得该域名的强登录态 Cookies。
         *     强登录态 Cookies 仅支持部分域名，由于协议差异，酷Q Air 及 酷Q Pro 支持的域名不同。
         *  
		 *	V9.20 (2019-3-3)
		 *	----------------
		 *
		 *	修改 接收语音 Api
		 *	 * 新版 SDK 中，该 Api 将返回本地**绝对**路径（旧版 SDK 是返回相对路径）。
		 *	   若此前用到了该 Api，需要对代码做相应调整。
		 *
		 *	新增 接收图片 Api
		 *	 * 可以使用该 Api，让酷Q直接下载收到的图片，并返回本地**绝对**路径。
		 *	   更新应用时，需要切换至该 Api，在未来获得更好的兼容性。
		 *
		 *	新增 是否支持发送图片() 及 是否支持发送语音() Api
		 *	 * 在开发图片及语音相关的应用时，可用该 Api 处理不支持对应功能的情况（如酷Q Air）。
		 */

        #region --常量--
        private const string DllName = "CQP.dll";
        #endregion

        #region --CqpApi--
        [DllImport (DllName, EntryPoint = "CQ_sendPrivateMsg")]
        public static extern int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg);

        [DllImport (DllName, EntryPoint = "CQ_sendGroupMsg")]
        public static extern int CQ_sendGroupMsg (int authCode, long groupId, IntPtr msg);

        [DllImport (DllName, EntryPoint = "CQ_sendDiscussMsg")]
        public static extern int CQ_sendDiscussMsg (int authCode, long discussId, IntPtr msg);

        [DllImport (DllName, EntryPoint = "CQ_deleteMsg")]
        public static extern int CQ_deleteMsg (int authCode, long msgId);

        [DllImport (DllName, EntryPoint = "CQ_sendLikeV2")]
        public static extern int CQ_sendLikeV2 (int authCode, long qqId, int count);

        [DllImport (DllName, EntryPoint = "CQ_getCookiesV2")]
        public static extern IntPtr CQ_getCookiesV2 (int authCode, IntPtr domain);

        [DllImport (DllName, EntryPoint = "CQ_getRecordV2")]
        public static extern IntPtr CQ_getRecordV2 (int authCode, IntPtr file, IntPtr format);

        [DllImport (DllName, EntryPoint = "CQ_getCsrfToken")]
        public static extern int CQ_getCsrfToken (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_getAppDirectory")]
        public static extern IntPtr CQ_getAppDirectory (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_getLoginQQ")]
        public static extern long CQ_getLoginQQ (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_getLoginNick")]
        public static extern IntPtr CQ_getLoginNick (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_setGroupKick")]
        public static extern int CQ_setGroupKick (int authCode, long groupId, long qqId, bool refuses);

        [DllImport (DllName, EntryPoint = "CQ_setGroupBan")]
        public static extern int CQ_setGroupBan (int authCode, long groupId, long qqId, long time);

        [DllImport (DllName, EntryPoint = "CQ_setGroupAdmin")]
        public static extern int CQ_setGroupAdmin (int authCode, long groupId, long qqId, bool isSet);

        [DllImport (DllName, EntryPoint = "CQ_setGroupSpecialTitle")]
        public static extern int CQ_setGroupSpecialTitle (int authCode, long groupId, long qqId, IntPtr title, long durationTime);

        [DllImport (DllName, EntryPoint = "CQ_setGroupWholeBan")]
        public static extern int CQ_setGroupWholeBan (int authCode, long groupId, bool isOpen);

        [DllImport (DllName, EntryPoint = "CQ_setGroupAnonymousBan")]
        public static extern int CQ_setGroupAnonymousBan (int authCode, long groupId, IntPtr anonymous, long banTime);

        [DllImport (DllName, EntryPoint = "CQ_setGroupAnonymous")]
        public static extern int CQ_setGroupAnonymous (int authCode, long groupId, bool isOpen);

        [DllImport (DllName, EntryPoint = "CQ_setGroupCard")]
        public static extern int CQ_setGroupCard (int authCode, long groupId, long qqId, IntPtr newCard);

        [DllImport (DllName, EntryPoint = "CQ_setGroupLeave")]
        public static extern int CQ_setGroupLeave (int authCode, long groupId, bool isDisband);

        [DllImport (DllName, EntryPoint = "CQ_setDiscussLeave")]
        public static extern int CQ_setDiscussLeave (int authCode, long disscussId);

        [DllImport (DllName, EntryPoint = "CQ_setFriendAddRequest")]
        public static extern int CQ_setFriendAddRequest (int authCode, IntPtr identifying, int requestType, IntPtr appendMsg);

        [DllImport (DllName, EntryPoint = "CQ_setGroupAddRequestV2")]
        public static extern int CQ_setGroupAddRequestV2 (int authCode, IntPtr identifying, int requestType, int responseType, IntPtr appendMsg);

        [DllImport (DllName, EntryPoint = "CQ_addLog")]
        public static extern int CQ_addLog (int authCode, int priority, IntPtr type, IntPtr msg);

        [DllImport (DllName, EntryPoint = "CQ_setFatal")]
        public static extern int CQ_setFatal (int authCode, IntPtr errorMsg);

        [DllImport (DllName, EntryPoint = "CQ_getGroupMemberInfoV2")]
        public static extern IntPtr CQ_getGroupMemberInfoV2 (int authCode, long groudId, long qqId, bool isCache);

        [DllImport (DllName, EntryPoint = "CQ_getGroupMemberList")]
        public static extern IntPtr CQ_getGroupMemberList (int authCode, long groupId);

        [DllImport (DllName, EntryPoint = "CQ_getGroupList")]
        public static extern IntPtr CQ_getGroupList (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_getStrangerInfo")]
        public static extern IntPtr CQ_getStrangerInfo (int authCode, long qqId, bool notCache);

        [DllImport (DllName, EntryPoint = "CQ_canSendImage")]
        public static extern int CQ_canSendImage (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_canSendRecord")]
        public static extern int CQ_canSendRecord (int authCode);

        [DllImport (DllName, EntryPoint = "CQ_getImage")]
        public static extern IntPtr CQ_getImage (int authCode, IntPtr file);

        [DllImport(DllName, EntryPoint = "CQ_getGroupInfo")]
        public static extern IntPtr CQ_getGroupInfo (int authCode, long groupId, bool notCache);

		[DllImport (DllName, EntryPoint = "CQ_getFriendList")]
		public static extern IntPtr CQ_getFriendList (int authCode, bool reserved);
		#endregion
	}
}
