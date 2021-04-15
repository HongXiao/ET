using ET.AI;
using ET.AOI;
using UnityEngine;

namespace ET._Game
{
    public class MapComponentAwakeSystem : AwakeSystem<MapComponent, StartSceneConfig>
    {
        public override void Awake(MapComponent self, StartSceneConfig sceneConfig)
        {
            Scene scene = self.DomainScene();
            scene.AddComponent<UnitComponent>();
            scene.AddComponent<AoiComponent, float, float>(.001f, .001f);
            scene.AddComponent<AiComponent>();
            //解析mapId
            if (int.TryParse(sceneConfig.Name, out int mapId))
            {
                scene.AddComponent<RecastPathComponent,int>(mapId);
                self.LoadNPC(mapId).Coroutine();
                self.LoadMonster(mapId).Coroutine();
            }
            else
            {
                Log.Error($"Map服 mapId isError {sceneConfig.Name}");
            }
     
        }
    }
    public static class MapComponentSystem
    {
        public static async ETVoid LoadNPC(this MapComponent self, int mapId)
        {
            Scene scene = self.DomainScene();
            for (int i = 0; i < 5; i++)
            {
                Unit unit = EntityFactory.CreateWithId<Unit, int>(scene, IdGenerater.Instance.GenerateId(), 1001);
                unit.Position = 10 * new Vector3(RandomHelper.RandFloat01() * 2 - 1, 0, RandomHelper.RandFloat01() * 2 - 1);
                unit.AddComponent<MailBoxComponent>();
                await unit.AddLocation();
                scene.GetComponent<UnitComponent>().Add(unit);
                unit.AddComponent<AoiEntity, long>(unit.Id);
            }
        }
        
        public static async ETVoid LoadMonster(this MapComponent self, int mapId)
        {
            Scene scene = self.DomainScene();
            for (int i = 0; i < 5; i++)
            {
                Unit unit = EntityFactory.CreateWithId<Unit, int>(scene, IdGenerater.Instance.GenerateId(), 1001);
                unit.Position = 10 * new Vector3(RandomHelper.RandFloat01() * 2 - 1, 0, RandomHelper.RandFloat01() * 2 - 1);
                unit.AddComponent<MailBoxComponent>();
                await unit.AddLocation();
                scene.GetComponent<UnitComponent>().Add(unit);
                unit.AddComponent<AoiEntity, long>(unit.Id);
            }
        }
    }
}