using System;

namespace ET.AI
{
    public class AIComponentAwakeSystem : AwakeSystem<AiComponent>
    {
        public override void Awake(AiComponent self)
        {
            self.Load();
        }
    }
    
    public class AIComponentLoadSystem : LoadSystem<AiComponent>
    {
        public override void Load(AiComponent self)
        {
            self.Load();
        }
    }
    
    public class AIComponentDestroySystem : DestroySystem<AiComponent>
    {
        public override void Destroy(AiComponent self)
        {
            self.Handlers.Clear();
        }
    }
    public static class AiComponentSystem
    {
        public static void Load(this AiComponent self)
        {
            self.Handlers.Clear();
            var types = Game.EventSystem.GetTypes(typeof (AiHandlerAttribute));
            foreach (Type type in types)
            { 
                AiHandler handler = Activator.CreateInstance(type) as AiHandler;
                if (handler == null)
                {
                    Log.Error($"Ai Handler is not AiHandler: {type.Name}");
                    continue;
                }
                self.Handlers.Add(type.Name, handler);
            }
        }

        public static AiHandler Get(this AiComponent self, string handlerName)
        {
            if (self.Handlers.TryGetValue(handlerName, out AiHandler handler))
            {
                return handler;
            }
            Log.Error($"Ai Handler is not find: {handlerName}");
            return null;
        }
        
    }
}