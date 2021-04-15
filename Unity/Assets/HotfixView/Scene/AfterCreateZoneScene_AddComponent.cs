using ET.AI;

namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<UIComponent>();
            zoneScene.AddComponent<LoginStateComponent>();
            zoneScene.AddComponent<SessionComponent>();
            zoneScene.AddComponent<AiComponent>();
            await ETTask.CompletedTask;
        }
    }
}