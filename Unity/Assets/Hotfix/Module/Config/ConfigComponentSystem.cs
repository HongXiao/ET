using System;
using System.Collections.Generic;

namespace ET
{
    public class ConfigAwakeSystem : AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent self)
        {
	        ConfigComponent.Instance = self;
        }
    }
    
    public class ConfigDestroySystem : DestroySystem<ConfigComponent>
    {
	    public override void Destroy(ConfigComponent self)
	    {
		    ConfigComponent.Instance = null;
	    }
    }
    
    public static class ConfigComponentSystem
	{
		public static T Get<T>(this ConfigComponent self) where T : ProtoObject, IConfig
		{
			if (self.AllConfig.TryGetValue(typeof(T), out object config))
			{
				return config as T;
			}
			return null;
		}
		public static async ETTask LoadAsync(this ConfigComponent self, Action<Dictionary<string, byte[]>> loadAllBytes)
		{
			self.AllConfig.Clear();
			HashSet<Type> types = Game.EventSystem.GetTypes(typeof (ConfigAttribute));
			
			Dictionary<string, byte[]> configBytes = new Dictionary<string, byte[]>();
			//ConfigComponent.GetAllConfigBytes(configBytes);
			loadAllBytes?.Invoke(configBytes);

#if SERVER
			List<System.Threading.Tasks.Task> listTasks = new List<System.Threading.Tasks.Task>();
			foreach (Type type in types)
			{
				System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() => self.LoadOneInThread(type, configBytes));
				listTasks.Add(task);
			}
			await System.Threading.Tasks.Task.WhenAll(listTasks.ToArray());
#else
			List<ETTask> listTasks = new List<ETTask>();
			foreach (Type type in types)
			{
				listTasks.Add(self.LoadOneAsync(type, configBytes));
			}
			await ETTaskHelper.WaitAll(listTasks);
#endif
		}

		private static E_ConfigType GetConfigType(Type configType)
		{
			var attributes = configType.GetCustomAttributes(false);
			foreach (object attribute in attributes)
				if (attribute is ConfigAttribute config)
					return config.ConfigType;
			return E_ConfigType.None;
		}

		private static object FromBytes(Type configType, byte[] bytes)
		{
			object category = null;
			E_ConfigType type = GetConfigType(configType);
			switch (type)
			{
				case E_ConfigType.Json:
					string json = System.Text.Encoding.UTF8.GetString(bytes);
					category = JsonHelper.FromJson(configType, json);
					break;
				case E_ConfigType.Proto:
					category = ProtobufHelper.FromBytes(configType, bytes, 0, bytes.Length);
					break;
				default:
					Log.Error($"反序列化配置表 {configType.Name} 失败! 不支持的格式 {type}");
					break;
			}
			return category;
		}

		private static async ETTask LoadOneAsync(this ConfigComponent self, Type configType, Dictionary<string, byte[]> configBytes)
		{
			if (!configBytes.ContainsKey(configType.Name))
			{
				Log.Error($"反序列化配置表 {configType.Name} 失败! 没有加载资源");
				return;
			}
			object category = FromBytes(configType, configBytes[configType.Name]);
			if (category == null)
				return;
			self.AllConfig[configType] = category;	
			await ETTask.CompletedTask;
		}

		private static void LoadOneInThread(this ConfigComponent self, Type configType, Dictionary<string, byte[]> configBytes)
		{
			if (!configBytes.ContainsKey(configType.Name))
			{
				Log.Error($"反序列化配置表 {configType.Name} 失败! 没有加载资源");
				return;
			}
			object category = FromBytes(configType, configBytes[configType.Name]);
			if (category == null)
				return;
			lock (self)
			{
				self.AllConfig[configType] = category;	
			}
		}
	}
}