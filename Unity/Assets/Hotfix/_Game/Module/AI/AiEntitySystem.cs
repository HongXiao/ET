namespace ET.AI
{
    public class AiEntityAwakeSystem : AwakeSystem<AiEntity>
    {
        public override void Awake(AiEntity self)
        {
            self.checkTimer = TimerComponent.Instance.NewRepeatedTimer(1000, self.Check);
        }
    }
    
    public class AiEntityDestroySystem : DestroySystem<AiEntity>
    {
        public override void Destroy(AiEntity self)
        {
            self.IsEnable = false;
            self.cancelToken = null;
        }
    }
    
    public static class AiEntitySystem
    {
        public static void Check(this AiEntity self)
        {
            if (self.Parent == null)
            {
                TimerComponent.Instance.Remove(ref self.checkTimer);
                return;
            }
     
            var ai = AiConfigCategory.Instance.GetAI(self.configId);
            Unit unit = self.GetParent<Unit>();
            foreach (AiConfig config in ai.Values)
            {
                if (self.IsDisposed || !self.IsEnable)
                    break;
                AiHandler handler = self.Domain.GetComponent<AiComponent>().Get(config.Name);
                if (handler == null)
                    continue;
                if (!handler.Check(unit, config))
                    continue;
                if (self.Current == config.Id)
                    break;
                self.Cancel(); // 取消之前的行为
                ETCancellationToken cancellationToken = new ETCancellationToken();
                self.cancelToken = cancellationToken;
                self.Current = config.Id;

                handler.Run(unit, config, cancellationToken).Coroutine();
                break;
            }
        }

        public static void Cancel(this AiEntity self)
        {
            
        }
    }
}