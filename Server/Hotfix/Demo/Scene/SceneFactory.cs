

using System.Net;
using ET._Game;
using ET.AOI;

namespace ET
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> Create(Entity parent, string name, SceneType sceneType)
        {
            long id = IdGenerater.Instance.GenerateId();
            return await Create(parent, id, parent.DomainZone(), name, sceneType);
        }
        
        public static async ETTask<Scene> Create(Entity parent, long id, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, zone, sceneType, name);
            scene.Parent = parent;

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case SceneType.Realm:
                    scene.AddComponent<NetKcpComponent, IPEndPoint>(startSceneConfig.OuterIPPort);
                    scene.AddComponent<OnlineComponent>();
                    break;
                case SceneType.Gate:
                    scene.AddComponent<GateComponent, StartSceneConfig>(startSceneConfig);
                    break;
                case SceneType.Map:
                    scene.AddComponent<MapComponent, StartSceneConfig>(startSceneConfig);
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    break;
            }

            return scene;
        }
    }
}