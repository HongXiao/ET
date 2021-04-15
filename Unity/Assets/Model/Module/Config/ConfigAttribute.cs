using System;

namespace ET
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ConfigAttribute: BaseAttribute
	{
		public E_ConfigType ConfigType = E_ConfigType.Proto;

		public ConfigAttribute()
		{
			
		}
		
		public ConfigAttribute(E_ConfigType configType)
		{
			this.ConfigType = configType;
		}
	}
}