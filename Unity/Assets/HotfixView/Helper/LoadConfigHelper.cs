using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    public static class LoadConfigHelper
    {
        public static void LoadAllConfigBytes(Dictionary<string, byte[]> output)
        {
            string config = "config.unity3d";
            ResourcesComponent.Instance.LoadBundle(config);
            Dictionary<string, UnityEngine.Object> keys = ResourcesComponent.Instance.GetBundleAll(config);

            foreach (var kv in keys)
            {
                TextAsset v = kv.Value as TextAsset;
                string key = kv.Key;
                output[key] = v.bytes;
                Log.Debug($"加载配置表 {key}");
            }
            ResourcesComponent.Instance.UnloadBundle(config);
        }
    }
}