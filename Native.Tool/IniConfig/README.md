## IniConfig 自述
	
	IniConfig 是基于 C# 开发, 针对于 Windows 平台下 Ini 配置文件的一款工具类, 
	该工具能快速的将 Ini 配置文件的 "节点", "键", "值", "注释" 分开, 在轻松实现对
	Ini 配置文件的增删改查的同时, 可直接移植到其它平台使用.

## IniConfig 示例

> 1. 创建一个新的 Ini 配置项

```C#
// 适用于 VS2012 ~ VS2017 的通用方式

IniSection iSection = new IniSection ("节点");    // 创建一个新的 "节点" 对象, 并且名称叫做 "节点", 相当于 [节点]
iSection.Add ("键1", "值1");	                  // 在一个节点中添加一个键值对, 相当于 键1=值1
iSection.Add ("key2", true);                      // Add 方法, 支持所有的基础数据类型进行自动转换
iSection.Add ("key3", 0x01);
iSection.Add ("key4", 10.456);
iSection.Add ("Key5", DateTime.Now);              // 甚至支持时间的直接添加, 具体请看重载列表

IniObject iObject = new IniObject ();
iObject.Add (iSection);     // 将一个 "节点" 添加到文件中
iObject.Save ("1.ini");	    // 将该文件保存, 参数1: 填写具体保存的路径, 可以是相对路径, 也可以是绝对路径

// 适用于 VS2017 的新方式

IniObject iObject = new IniObject ()
{
	new IniSection ("节点1")
	{
		{ "键1", "值1"},
		{ "Key2", 123456},
		{ "Key3", true}
	},
	new IniSection ("节点2")
	{
		{ "key1", DateTime.Now}
	}
};
iObject.Save ("1.ini");
```

>2. 从文件中读取 Ini 配置项

```C#
// 适用于 VS2012 ~ VS2017 的通用方式

IniObject iObject = IniObject.Load ("1.ini", Encoding.Default);     // 从指定的文件中读取 Ini 配置项, 参数1: 文件路径, 参数2: 编码格式 [默认: ANSI]
iObject = IniObject.Parse ("[节点]\n键1=值1");	                    // 当然也可以通过字符串进行解析
```

>3. 查询 Ini 配置文件

```C#
// 适用于 VS2012 ~ VS2017 的通用方式

// 对节点进行查询, 通过 "索引" 或 "名称" 的方式快速获取
IniSection section1 = iObject[0];       // 通过 索引
IniSection section2 = iObject["节点"];  // 通过 名称

// 对节点中内容进行查询, 通过 "名称" 的方式快速获取
IniValue value1 = section1["键1"];      // 通过 名称

// 拿取 Value 的值
value1.ToString ();             // 提供众多方式, 对基础数据类型的转换提供支持
value1.ToInt32 ();
value1.ToDouble ();
value1.ToByte ();
Convert.ToDateTime (value1);	// 当然也可以使用 Convert

// 快速拿取 Value 
IniValue value2 = iObject["节点"]["键1"];
```

>4. 修改 Ini 配置文件

```C#
//适用于 VS2012 ~ VS2017 的通用方式

IniObject iObject = IniObject.Load ("1.ini");
iObject["节点1"]["键1"] = new IniValue ("更新值");    // 因为无法重载 = 运算符, 所以没办法只能 new 对象
iObject["节点1"]["键1"] = new IniValue (10);
iObject["节点1"]["键1"].Value = "更新值";		// 适用于字符串的时候
iObject.Save ();
```
