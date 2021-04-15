using System.Collections.Generic;
using ET.AOI;

namespace ET
{
    public class UnitAwakeSystem: AwakeSystem<Unit, int>
    {
        public override void Awake(Unit self, int configId)
        {
            self.ConfigId = configId;
        }
    }

    public static class UnitSystem
    {
        //发消息给自己
        public static void Send(this Unit self, IActorMessage actorMessage)
        {
            var unitGate = self.GetComponent<UnitGateComponent>();
            if (unitGate == null)
                return;
            MessageHelper.SendActor(unitGate.GateSessionActorId, actorMessage);
        }
        //发消息给周围玩家
        public static void BroadcastAOI(this Unit self, IActorMessage actorMessage, bool hasSelf = true)
        {
            var units = self.GetAll(hasSelf);
            foreach (Unit unit in units)
            {
                unit.Send(actorMessage);
            }
        }

        public static List<Unit> GetAll(this Unit self, bool hasSelf = false)
        {
            UnitComponent unitComponent = self.Domain.GetComponent<UnitComponent>();
            var units = unitComponent.GetAll(self.GetComponent<AoiEntity>().ViewEntity);
            if (hasSelf)
                units.Add(self);
            return units;
        }

        public static List<Unit> GetAll(this Unit self, ICollection<long> ids, bool hasSelf = false)
        {
            UnitComponent unitComponent = self.Domain.GetComponent<UnitComponent>();
            var units = unitComponent.GetAll(ids);
            if (hasSelf)
                units.Add(self);
            return units;
        }
        
        //发消息给当前地图所有玩家
        public static void Broadcast(this Unit self, IActorMessage actorMessage)
        {
            MessageHelper.Broadcast(self, actorMessage);
        }
    }
}