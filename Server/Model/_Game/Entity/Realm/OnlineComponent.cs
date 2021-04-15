using System.Collections.Generic;

namespace ET._Game
{
    /// <summary>
    /// 用户在线管理组件
    /// </summary>
    public class OnlineComponent : Entity
    {
        //userId～GateId map
        public Dictionary<long, int> Map = new Dictionary<long, int>();
    }
}