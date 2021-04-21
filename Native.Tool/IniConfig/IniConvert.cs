using Native.Tool.IniConfig.Attribute;
using Native.Tool.IniConfig.Exception;
using Native.Tool.IniConfig.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Native.Tool.IniConfig
{
	/// <summary>
	/// 提供了普通对象针对 IniConfig 快速转换的类
	/// </summary>
	[Obsolete ("请改用 IniConfig 类型做转换")]
	public static class IniConvert
	{
		/// <summary>
		/// 将多个对象一并序列化为 <see cref="IniObject"/> 的形式
		/// </summary>
		/// <param name="objs">需要序列化的对象, 该参数可以无限添加</param>
		/// <returns>序列化成功返回 <see cref="IniObject"/></returns>
		public static IniObject SerializeObject (params object[] objs)
		{
			IniObject iniobj = new IniObject ();
			foreach (object obj in objs)
			{
				iniobj.Add (SerializeObject (obj));
			}
			return iniobj;
		}

		/// <summary>
		/// 将对象序列化为 <see cref="IniSection"/> 的形式
		/// </summary>
		/// <param name="obj">需要序列化的对象</param>
		/// <exception cref="ArgumentNullException">参数: obj 是 null</exception>
		/// <returns>序列化成功返回 <see cref="IniSection"/></returns>
		public static IniSection SerializeObject (object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			Type objType = obj.GetType ();

			IniSection inisect = null;
			IniConfigSectionAttribute sectionAttribute = objType.GetCustomAttribute<IniConfigSectionAttribute> ();
			if (sectionAttribute != null)
			{
				inisect = new IniSection (sectionAttribute.SectionName);
			}
			else
			{
				inisect = new IniSection (objType.Name);
			}

			foreach (PropertyInfo property in objType.GetRuntimeProperties ())
			{
				// 跳过不准备序列化的属性
				if (property.GetCustomAttribute<IniNonSerializeAttribute> () != null)
				{
					continue;
				}

				// 获取 Key
				string key = null;
				IniConfigKeyAttribute keyAttribute = property.GetCustomAttribute<IniConfigKeyAttribute> ();
				if (keyAttribute != null)
				{
					key = keyAttribute.KeyName;
				}
				else
				{
					key = property.Name;
				}

				// 只序列化 0 个参数的属性
				MethodInfo methodInfo = property.GetGetMethod (true);
				if (methodInfo.GetParameters ().Count () == 0)
				{
					inisect.Add (key, new IniValue (Convert.ToString (methodInfo.Invoke (obj, null))));
				}
			}
			return inisect;
		}

		/// <summary>
		/// 将 <see cref="IniSection"/> 实例反序列化为指定类型的对象
		/// </summary>
		/// <param name="obj"><see cref="IniSection"/>, 用来复制到对象中</param>
		/// <param name="type">对象的类型</param>
		/// <exception cref="ArgumentNullException">参数: obj 或 type 是 null</exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static object DeserializeObject (IniSection obj, Type type)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			if (type == null)
			{
				throw new ArgumentNullException ("type");
			}

			List<PropertyInfo> properties = new List<PropertyInfo> (type.GetRuntimeProperties ());

			object instance = Activator.CreateInstance (type);
			// 遍历 节 中的键值对
			foreach (KeyValuePair<string, IniValue> iniPair in obj)
			{
				// 获取属性
				PropertyInfo property = type.GetRuntimeProperty (iniPair.Key);
				if (property == null)
				{
					IEnumerable<PropertyInfo> linqProperties = from temp in properties
															   where temp.GetCustomAttribute<IniConfigKeyAttribute> ().KeyName.CompareTo (iniPair.Key) == 0
															   select temp;
					try
					{
						property = linqProperties.First ();
					}
					catch (ArgumentNullException ex)
					{
						throw new PropertyNotFoundException (string.Format ("无法在类型: {0} 中找到对应的 Key: {1}", type.Name, iniPair.Key), ex);
					}
					catch (InvalidOperationException ex)
					{
						throw new PropertyNotFoundException (string.Format ("无法在类型: {0} 中找到对应的 Key: {1}", type.Name, iniPair.Key), ex);
					}
				}

				// 设置属性的值
				object value = Convert.ChangeType (iniPair.Value.Value, property.PropertyType);
				if (value == null)
				{
					value = string.Empty;
				}
				property.SetValue (instance, value);
			}

			return instance;
		}

		/// <summary>
		/// 将 <see cref="IniObject"/> 实例中指定位置的 "节" 反序列化为指定类型的对象
		/// </summary>
		/// <param name="obj"><see cref="IniObject"/>, 用来查找 "节"</param>
		/// <param name="index">节的位置</param>
		/// <param name="type">对象的类型</param>
		/// <exception cref="ArgumentNullException">参数: obj 或 type 是 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">index 小于 0。 - 或 - index 等于或大于 <see cref="IniObject"/></exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static object DeserializeObject (IniObject obj, int index, Type type)
		{
			try
			{
				return DeserializeObject (obj[index], type);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw;
			}
		}

		/// <summary>
		/// 将 <see cref="IniObject"/> 实例中指定名称的 "节" 反序列化为指定类型的对象
		/// </summary>
		/// <param name="obj"><see cref="IniObject"/>, 用来查找 "节"</param>
		/// <param name="sectionName">节的名称</param>
		/// <param name="type">对象的类型</param>
		/// <exception cref="ArgumentNullException">参数: obj 或 type 是 null</exception>
		/// <exception cref="SectionNotFoundException">无法通过名字定位 <see cref="IniSection"/> 的位置</exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static object DeserializeObject (IniObject obj, string sectionName, Type type)
		{
			try
			{
				return DeserializeObject (obj[sectionName], type);
			}
			catch (SectionNotFoundException)
			{
				throw;
			}
		}

		/// <summary>
		/// 将 <see cref="IniSection"/> 实例反序列化为指定类型的对象
		/// </summary>
		/// <typeparam name="T">需要转换的类型</typeparam>
		/// <param name="obj"><see cref="IniSection"/>, 用来复制到对象中</param>
		/// <exception cref="ArgumentNullException">参数: obj 是 null</exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static T DeserializeObject<T> (IniSection obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException ("obj");
			}

			return (T)DeserializeObject (obj, typeof (T));
		}

		/// <summary>
		/// 将 <see cref="IniObject"/> 实例中指定位置的 "节" 反序列化为指定类型的对象
		/// </summary>
		/// <typeparam name="T">需要转换的类型</typeparam>
		/// <param name="obj"><see cref="IniObject"/>, 用来查找 "节"</param>
		/// <param name="index">节的位置</param>
		/// <exception cref="ArgumentNullException">参数: obj 是 null</exception>
		/// <exception cref="ArgumentOutOfRangeException">index 小于 0。 - 或 - index 等于或大于 <see cref="IniObject"/></exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static T DeserializeObject<T> (IniObject obj, int index)
		{
			try
			{
				return (T)DeserializeObject (obj, index, typeof (T));
			}
			catch (ArgumentOutOfRangeException)
			{
				throw;
			}
		}

		/// <summary>
		/// 将 <see cref="IniObject"/> 实例中指定名称的 "节" 反序列化为指定类型的对象
		/// </summary>
		/// <typeparam name="T">需要转换的类型</typeparam>
		/// <param name="obj"><see cref="IniObject"/>, 用来查找 "节"</param>
		/// <param name="sectionName">节的名称</param>
		/// <exception cref="ArgumentNullException">参数: obj 是 null</exception>
		/// <exception cref="SectionNotFoundException">无法通过名字定位 <see cref="IniSection"/> 的位置</exception>
		/// <returns>反序列化成功返回指定类型的对象</returns>
		public static T DeserializeObject<T> (IniObject obj, string sectionName)
		{
			try
			{
				return (T)DeserializeObject (obj[sectionName], typeof (T));
			}
			catch (SectionNotFoundException)
			{
				throw;
			}
		}
	}
}
