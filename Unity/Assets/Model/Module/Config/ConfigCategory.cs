using System;
using System.Collections.Generic;

namespace ET
{

    //[ProtoContract]
    public abstract partial class ConfigCategory<TCategory, TConfig> : ProtoObject where TCategory : ConfigCategory<TCategory, TConfig>, new() where TConfig : ProtoObject, IConfig
    {
        public static TCategory Instance;

        public ConfigCategory()
        {
            Instance = this as TCategory;
        }
        
        private Dictionary<int, TConfig> dict = new Dictionary<int, TConfig>();
		
        protected virtual List<TConfig> list { get; set; }
        
        public virtual void AfterDeserialization()
        {
            //Log.Debug($"AfterDeserialization {this.GetType()}  Instance ? {Instance==null}");
            foreach (TConfig config in list)
            {
                //Log.Debug($"Add id:{config.Id} {config}");
                this.dict.Add(config.Id, config);
            }
            list.Clear();
            this.EndInit();
        }
        
        public TConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: { typeof(TConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TConfig> GetAll()
        {
            return this.dict;
        }

        public TConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }
}