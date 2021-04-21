using Native.Sdk.Cqp.Core;
using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Expand;
using System.Runtime.InteropServices;

namespace Native.Sdk.Cqp
{
	/// <summary>
	/// 表示 酷Q日志 的封装类
	/// </summary>
	public class CQLog
	{
		#region --字段--
		private readonly int _authCode = 0;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的验证码
		/// </summary>
		public int AuthCode { get { return _authCode; } }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQLog"/> 类的新实例, 该实例由 <code>Initialize</code> 函数进行授权
		/// </summary>
		/// <param name="authCode">授权码</param>
		public CQLog (int authCode)
		{
			this._authCode = authCode;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 向酷Q跟踪器发送一条日志, 并且拥有优先级和类型
		/// </summary>
		/// <param name="level">日志的优先级</param>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog WriteLine (CQLogLevel level, string type, params object[] contents)
		{
			GCHandle toSendStringHandle = contents.ToSendString ().GetStringGCHandle ();
			GCHandle typeHandle = type.GetStringGCHandle ();
			try
			{
				CQP.CQ_addLog (this.AuthCode, (int)level, typeHandle.AddrOfPinnedObject (), toSendStringHandle.AddrOfPinnedObject ());
				return this;
			}
			finally
			{
				toSendStringHandle.Free ();
			}
		}

		/// <summary>
		/// 向酷Q跟踪器发送一条 <see cref="CQLogLevel.Debug"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog Debug (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.Debug, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.Info"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog Info (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.Info, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.InfoSuccess"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog InfoSuccess (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.InfoSuccess, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.InfoReceive"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog InfoReceive (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.InfoReceive, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.InfoSend"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog InfoSend (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.InfoSend, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.Warning"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog Warning (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.Warning, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.Error"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog Error (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.Error, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.Fatal"/> 级别的日志, 并拥有自己的类型
		/// </summary>
		/// <param name="type">显示在窗体中的类型</param>
		/// <param name="contents">日志详细信息</param>
		/// <returns>返回当前实例 <see cref="CQLog"/></returns>
		public CQLog Fatal (string type, params object[] contents)
		{
			return WriteLine (CQLogLevel.Error, type, contents);
		}

		/// <summary>
		/// 向酷Q日志跟踪器发送一条 <see cref="CQLogLevel.Fatal"/> 级别的日志, 并弹窗提示错误
		/// </summary>
		/// <param name="contents">日志详细信息</param>
		public void SetFatalMessage (params object[] contents)
		{
			GCHandle handle = contents.ToSendString ().GetStringGCHandle ();
			try
			{
				CQP.CQ_setFatal (_authCode, handle.AddrOfPinnedObject ());
			}
			finally
			{
				handle.Free ();
			}
		}
		#endregion
	}
}
