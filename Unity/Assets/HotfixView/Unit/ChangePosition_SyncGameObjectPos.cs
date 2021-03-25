using UnityEngine;

namespace ET
{
    public class ChangePosition_SyncGameObjectPos: AEvent<EventType.ChangePosition>
    {
        protected override async ETTask Run(EventType.ChangePosition args)
        {
            GameObject go = args.Unit.GetComponent<GameObjectComponent>().GameObject;
            if (go != null)
                go.transform.position = args.Unit.Position;
            await ETTask.CompletedTask;
        }
    }
}