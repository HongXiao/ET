using System.Net;

namespace ET._Game.System.Gate
{
    public class GateComponentAwakeSystem : AwakeSystem<GateComponent, StartSceneConfig>
    {
        public override void Awake(GateComponent self, StartSceneConfig sceneConfig)
        {
            Scene scene = self.DomainScene();
            scene.AddComponent<NetKcpComponent, IPEndPoint>(sceneConfig.OuterIPPort);
            scene.AddComponent<PlayerComponent>();
            scene.AddComponent<GateSessionKeyComponent>();
        }
    }
    public static class GateComponentSystem
    {
        
    }
}