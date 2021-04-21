using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Native.Sdk.Cqp.Expand
{
	/// <summary>
	/// <see cref="BinaryReader"/> 类的扩展方法集
	/// </summary>
	public static class BinaryReaderExpand
	{
		#region --公开方法--
		/// <summary>
		/// 获取基础流的剩余长度
		/// </summary>
		/// <param name="binary"></param>
		/// <returns></returns>
		public static long Length (this BinaryReader binary)
		{
			return binary.BaseStream.Length - binary.BaseStream.Position;
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取所有字节。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <returns>读入 buffer 的字节数。 如果可用的字节没有请求的那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static byte[] ReadAll_Ex (this BinaryReader binary)
		{
			return GetBinary (binary, binary.BaseStream.Length, false);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取指定字节长度。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <param name="len">要读取的字节数。</param>
		/// <returns>读入 buffer 的字节数。 如果可用的字节没有请求的那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentOutOfRangeException">len 为负数。</exception>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static byte[] ReadBin_Ex (this BinaryReader binary, long len)
		{
			return GetBinary (binary, len);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取 2 字节长度并反序为 <see cref="int"/> 值。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <returns>读入 2 字节的结果值，如果可用的字节没有那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static short ReadInt16_Ex (this BinaryReader binary)
		{
			return BitConverter.ToInt16 (GetBinary (binary, 2, true), 0);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取 4 字节长度并反序为 <see cref="int"/> 值。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <returns>读入 4 字节的结果值，如果可用的字节没有那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static int ReadInt32_Ex (this BinaryReader binary)
		{
			return BitConverter.ToInt32 (GetBinary (binary, 4, true), 0);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取 8 字节长度并反序为 <see cref="long"/> 值。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <returns>读入 8 字节的结果值，如果可用的字节没有那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static long ReadInt64_Ex (this BinaryReader binary)
		{
			return BitConverter.ToInt64 (GetBinary (binary, 8, true), 0);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取指定字节长度。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <returns>读入 buffer 的字节数。 如果可用的字节没有请求的那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentOutOfRangeException">len 为负数。</exception>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static byte[] ReadToken_Ex (this BinaryReader binary)
		{
			short len = ReadInt16_Ex (binary);
			return GetBinary (binary, len);
		}

		/// <summary>
		/// 从字节数组中的指定点开始，从流中读取指定编码的字符串。
		/// </summary>
		/// <param name="binary">基础 <see cref="BinaryWriter"/> 对象</param>
		/// <param name="encoding"></param>
		/// <returns>读入 buffer 的字节数。 如果可用的字节没有请求的那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
		/// <exception cref="ArgumentOutOfRangeException">len 为负数。</exception>
		/// <exception cref="ArgumentException">已解码要读取的字符数超过了边界。</exception>
		/// <exception cref="ObjectDisposedException">流已关闭。</exception>
		/// <exception cref="IOException">出现 I/O 错误。</exception>
		public static string ReadString_Ex (this BinaryReader binary, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.ASCII;
			}

			return encoding.GetString (ReadToken_Ex (binary));
		}
		#endregion

		#region --私有方法--
		private static byte[] GetBinary (BinaryReader binary, long len, bool isReverse = false)
		{
			byte[] buffer = new byte[len];
			binary.Read (buffer, 0, buffer.Length);
			if (isReverse)
			{
				buffer = buffer.Reverse ().ToArray ();
			}
			return buffer;
		}
		#endregion
	}
}
