using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Native.Sdk.Cqp.Enum
{
	/// <summary>
	/// 指示酷Q接收语音时的转换格式
	/// </summary>
	public enum CQAudioFormat
	{
        /// <summary>
        /// mp3音频格式
        /// </summary>
        [Description("mp3")]
        MPEG_Layer3,
        /// <summary>
        /// arm音频格式
        /// </summary>
        [Description ("arm")]
        AMR_NB,
        /// <summary>
        /// wma音频格式
        /// </summary>
        [Description ("wma")]
        Windows_Media_Audio,
        /// <summary>
        /// m4a音频格式
        /// </summary>
        [Description ("m4a")]
        MPEG4,
        /// <summary>
        /// spx音频格式
        /// </summary>
        [Description ("spx")]
        Speex,
        /// <summary>
        /// ogg音频格式
        /// </summary>
        [Description ("ogg")]
        OggVorbis,
        /// <summary>
        /// wav音频格式
        /// </summary>
        [Description ("wav")]
        WAVE,
        /// <summary>
        /// flac音频格式
        /// </summary>
        [Description ("flac")]
        FLAC
	}
}
