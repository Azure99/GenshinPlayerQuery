using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Native.Tool.Http
{
	/// <summary>
	/// Http 工具类
	/// </summary>
	public static class HttpTool
	{
		/// <summary>
		/// 使用默认编码对 URL 进行编码
		/// </summary>
		/// <param name="url">要编码的地址</param>
		/// <returns>编码后的地址</returns>
		public static string UrlEncode (string url)
		{
			return HttpUtility.UrlEncode (url);
		}
		
		/// <summary>
		/// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行编码
		/// </summary>
		/// <param name="url">要编码的地址</param>
		/// <param name="encoding">编码类型</param>
		/// <returns>编码后的地址</returns>
		public static string UrlEncode (string url, Encoding encoding)
		{
			return HttpUtility.UrlEncode (url, encoding);
		}
		
		/// <summary>
		/// 使用默认编码对 URL 进行解码
		/// </summary>
		/// <param name="url">要解码的地址</param>
		/// <returns>编码后的地址</returns>
		public static string UrlDecode (string url)
		{
			return HttpUtility.UrlDecode (url);
		}
		
		/// <summary>
		/// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行解码
		/// </summary>
		/// <param name="url">要解码的地址</param>
		/// <param name="encoding">编码类型</param>
		/// <returns>编码后的地址</returns>
		public static string UrlDecode (string url, Encoding encoding)
		{
			return HttpUtility.UrlDecode (url, encoding);
		}
	}
}
