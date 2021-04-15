using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.AOI
{
    public sealed class AoiEntity : Entity
    {
        public long Key;
        public AoiNode X;
        public AoiNode Y;
        public HashSet<long> ViewEntity;//当前玩家视野内的对象
        public HashSet<long> ViewEntityBak;
        public IEnumerable<long> Move => ViewEntity.Union(ViewEntityBak);//当前感知玩家移动的对象
        public IEnumerable<long> Leave => ViewEntityBak.Except(ViewEntity);//当前离开玩家视野的对象

        public Action<AoiEntity> EnterAction;
        public Action<AoiEntity> ExitAction;
        public Action<AoiEntity> MoveAction;
        public AoiEntity(long key)
        {
            Key = key;
            ViewEntity = new HashSet<long>();
            ViewEntityBak = new HashSet<long>();
        }
        
        public AoiEntity(){}
    }
}