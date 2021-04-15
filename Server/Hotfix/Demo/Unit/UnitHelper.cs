﻿using System.Collections.Generic;

namespace ET
{
    public static class UnitHelper
    {
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.X = unit.Position.x;
            unitInfo.Y = unit.Position.y;
            unitInfo.Z = unit.Position.z;
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.Ks.Add(key);
                unitInfo.Vs.Add(value);
            }

            return unitInfo;
        }

        public static List<UnitInfo> CreateUnitInfo(ICollection<Unit> units)
        {
            List<UnitInfo> unitInfos = new List<UnitInfo>();
            foreach (Unit unit in units)
            {
                unitInfos.Add(CreateUnitInfo(unit));
            }
            return unitInfos;
        }
    }
}