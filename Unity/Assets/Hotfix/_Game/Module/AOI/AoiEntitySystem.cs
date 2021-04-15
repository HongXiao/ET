using System.Collections.Generic;
using UnityEngine;

namespace ET.AOI
{
    public class AoiEntityAwakeSystem: AwakeSystem<AoiEntity>
    {
        public override void Awake(AoiEntity self)
        {
            self.Awake(self.GetParent<Unit>().Id);
        }
    }

    public class AoiEntityAwakeSystem1: AwakeSystem<AoiEntity, long>
    {
        public override void Awake(AoiEntity self, long key)
        {
            self.Awake(key);
        }
    }

    public class AoiEntityDestroySystem: DestroySystem<AoiEntity>
    {
        public override void Destroy(AoiEntity self)
        {
            self.ExitAction?.Invoke(self);
            AoiComponent aoiComponent = self.Domain.GetComponent<AoiComponent>();
            aoiComponent.Exit(self);
        }
    }


    public static class AoiEntitySystem
    {
        public static void Awake(this AoiEntity self, long key)
        {
            self.Key = key;
            self.ViewEntity = new HashSet<long>();
            self.ViewEntityBak = new HashSet<long>();
            Unit unit = self.GetParent<Unit>(); 
            AoiComponent aoiComponent = self.Domain.GetComponent<AoiComponent>();
            aoiComponent.Enter(self, unit.Position.x, unit.Position.z, Vector2.one*10);
            self.EnterAction?.Invoke(self);
        }
    }
    
}