using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Sdk.Cqp.Model
{
	/// <summary>
	/// 表示当前插件的一些基本信息的类
	/// </summary>
	public class AppInfo
	{
		#region --属性--
		/// <summary>
		/// 获取当前应用的 AppID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 获取当前应用的返回码
		/// </summary>
		public int ResultCode { get; private set; }

		/// <summary>
		/// 获取当前应用的 Api 版本
		/// </summary>
		public int ApiVersion { get; private set; }

		/// <summary>
		/// 获取当前应用的名称
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 获取当前应用的版本号
		/// </summary>
		public Version Version { get; private set; }

		/// <summary>
		/// 获取当前应用的顺序版本
		/// </summary>
		public int VersionId { get; private set; }

		/// <summary>
		/// 获取当前应用的作者名
		/// </summary>
		public string Author { get; private set; }

		/// <summary>
		/// 获取当前应用的说明文本
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// 获取当前应用的验证码
		/// </summary>
		public int AuthCode { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="AppInfo"/> 类的新实例
		/// </summary>
		/// <param name="id">当前应用appid</param>
		/// <param name="resCode">返回码</param>
		/// <param name="apiVer">api版本</param>
		/// <param name="name">应用名称</param>
		/// <param name="version">版本号</param>
		/// <param name="versionId">版本id</param>
		/// <param name="author">应用作者</param>
		/// <param name="description">应用说明</param>
		/// <param name="authCode">应用授权码</param>
		public AppInfo (string id, int resCode, int apiVer, string name, string version, int versionId, string author, string description, int authCode)
		{
			this.Id = id;
			this.ResultCode = resCode;
			this.ApiVersion = apiVer;
			this.Name = name;
			this.Version = new Version (version);
			this.VersionId = versionId;
			this.Author = author;
			this.Description = description;
			this.AuthCode = authCode;
		}
		#endregion
	}
}
