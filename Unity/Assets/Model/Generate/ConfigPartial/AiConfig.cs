
using System.Collections.Generic;

namespace ET
{
    public partial class AiConfigCategory
    {
        public Dictionary<int, SortedDictionary<int, AiConfig>> AiConfigs = new Dictionary<int, SortedDictionary<int, AiConfig>>();

        public SortedDictionary<int, AiConfig> GetAI(int aiConfigId)
        {
            return this.AiConfigs[aiConfigId];
        }
		
        public override void EndInit()
        {
            base.EndInit();
			
            foreach (var kv in this.GetAll())
            {
                SortedDictionary<int, AiConfig> aiConfig;
                if (!this.AiConfigs.TryGetValue(kv.Value.ConfigId, out aiConfig))
                {
                    aiConfig = new SortedDictionary<int, AiConfig>();
                    this.AiConfigs.Add(kv.Value.ConfigId, aiConfig);
                }
				
                aiConfig.Add(kv.Key, kv.Value);
            }
        }
    }
}