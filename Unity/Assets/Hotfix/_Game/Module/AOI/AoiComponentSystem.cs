using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.AOI
{
    #region Awake System
    public class AoiComponentAwakeSystem: AwakeSystem<AoiComponent>
    {
        public override void Awake(AoiComponent self)
        {
            self._xLinks = new AoiLinkedList();
            self._yLinks = new AoiLinkedList();
        }
    }
    public class AoiComponentAwakeSystem1: AwakeSystem<AoiComponent, float, float>
    {
        public override void Awake(AoiComponent self, float xLimit, float yLimit)
        {
            self._xLinks = new AoiLinkedList(limit:xLimit);
            self._yLinks = new AoiLinkedList(limit:yLimit);
            //TestAoi(self);
        }
        private void TestAoi(AoiComponent self)
        {
            // 添加500个玩家。
            for (var i = 1; i <= 500; i++)
                self.Enter(i, i, i);
            var area = new Vector2(3, 3);
            // 测试移动。
            // while (true)
            // {
            //     Log.Debug("1");
            //     zone.Refresh(new Random().Next(0, 500), new Random().Next(0, 500), new Random().Next(0, 500), area);
            //     Log.Debug("2");
            // }
            // 刷新key为3的信息。
            {
                var entity = self.Refresh(3, area);
                Log.Debug("---------------加入玩家范围的玩家列表--------------");
                foreach (var aoiKey in entity.ViewEntity)
                {
                    var findEntity = self[aoiKey];
                    Log.Debug($"X:{findEntity.X.Value} Y:{findEntity.Y.Value}");
                }
            }
            // 更新key为50的坐标。
            {
                var entity = self.Refresh(3, 5, 5, area);
                Log.Debug("---------------离开玩家范围的玩家列表--------------");
                foreach (var aoiKey in entity.Leave)
                {
                    var findEntity = self[aoiKey];
                    Log.Debug($"X:{findEntity.X.Value} Y:{findEntity.Y.Value}");
                }
                Log.Debug("---------------移动玩家范围的玩家列表--------------");
                foreach (var aoiKey in entity.Move)
                {
                    var findEntity = self[aoiKey];
                    Log.Debug($"X:{findEntity.X.Value} Y:{findEntity.Y.Value}");
                }
                Log.Debug("---------------key为3移动后加入玩家范围的玩家列表--------------");
                foreach (var aoiKey in entity.ViewEntity)
                {
                    var findEntity = self[aoiKey];
                    Log.Debug($"X:{findEntity.X.Value} Y:{findEntity.Y.Value}");
                }
                Log.Debug("---------------key为3移动前玩家范围的玩家列表--------------");
                foreach (var aoiKey in entity.ViewEntityBak)
                {
                    var findEntity = self[aoiKey];
                    Log.Debug($"X:{findEntity.X.Value} Y:{findEntity.Y.Value}");
                }
            }
            // 离开当前AOI
            self.Exit(50);
        }
    }
    public class AoiComponentAwakeSystem2: AwakeSystem<AoiComponent, int, float, float>
    {
        public override void Awake(AoiComponent self, int maxLayer, float xLimit, float yLimit)
        {
            self._xLinks = new AoiLinkedList(maxLayer, xLimit);
            self._yLinks = new AoiLinkedList(maxLayer, yLimit);
        }
    }
    #endregion
    
    public static class AoiComponentSystem
    {
        #region API by key
        public static AoiEntity Enter(this AoiComponent self, long key, float x, float y)
        {
            if (!self._entityList.TryGetValue(key, out var entity))
            {
                entity = new AoiEntity(key);
                entity.X = self._xLinks.Add(x, entity);
                entity.Y = self._yLinks.Add(y, entity);
                self._entityList.Add(key, entity);
            }
            return entity;
        }
        public static AoiEntity Enter(this AoiComponent self, long key, float x, float y, Vector2 area)
        {
            var entity = self.Enter(key, x, y);
            self.Refresh(key, area);
            return entity;
        }
        public static AoiEntity Refresh(this AoiComponent self, long key, Vector2 area)
        {
            if (!self._entityList.TryGetValue(key, out var entity)) 
                return null;
            self.Find(entity, ref area);
            return entity;
        }
        public static AoiEntity Refresh(this AoiComponent self, long key, float x, float y, Vector2 area)
        {
            if (!self._entityList.TryGetValue(key, out var entity)) return null;
            var isFind = false;
            if (Math.Abs(entity.X.Value - x) > 0)
            {
                isFind = true;
                self._xLinks.Move(entity.X, ref x);
            }
            if (Math.Abs(entity.Y.Value - y) > 0)
            {
                isFind = true;
                self._yLinks.Move(entity.Y, ref y);
            }

            if (isFind)
            {
                self.Find(entity, ref area);
                entity.MoveAction?.Invoke(entity);
            }
            return entity;
        }
        public static void Exit(this AoiComponent self, long key)
        {
            if (!self._entityList.TryGetValue(key, out var entity)) 
                return;
            self.Exit(entity);
        }
        #endregion

        #region API by entity
        public static AoiEntity Enter(this AoiComponent self, AoiEntity entity, float x, float y)
        {
            if (!self._entityList.ContainsKey(entity.Key))
            {
                entity.X = self._xLinks.Add(x, entity);
                entity.Y = self._yLinks.Add(y, entity);
                self._entityList.Add(entity.Key, entity);
                Log.Info($"对象加入AOI, key:{entity.Key}");
            }
            return entity;
        }
        
        public static AoiEntity Enter(this AoiComponent self, AoiEntity entity, float x, float y, Vector2 area)
        {
            self.Enter(entity, x, y);
            self.Refresh(entity.Key, area);
            return entity;
        }

        public static AoiEntity Refresh(this AoiComponent self, AoiEntity entity, Vector2 area)
        {
            return self.Refresh(entity.Key, area);
        }
        
        public static AoiEntity Refresh(this AoiComponent self, AoiEntity entity, float x, float y, Vector2 area)
        {
            return self.Refresh(entity.Key, x, y, area);
        }
        public static void Exit(this AoiComponent self, AoiEntity entity)
        {
            self._xLinks.Remove(entity.X);
            self._yLinks.Remove(entity.Y);
            self._entityList.Remove(entity.Key); ;
        }
        #endregion

        #region Tools func
        /// <summary>
        /// Look for nodes in range
        /// </summary>
        /// <param name="node"></param>
        /// <param name="area"></param>
        /// <returns>news entity</returns>
        private static void Find(this AoiComponent self, AoiEntity node, ref Vector2 area)
        {
            SwapViewEntity(ref node.ViewEntity, ref node.ViewEntityBak);
            #region xLinks
            for (var i = 0; i < 2; i++)
            {
                var cur = i == 0 ? node.X.Right : node.X.Left;
                while (cur != null)
                {
                    if (Math.Abs(Math.Abs(cur.Value) - Math.Abs(node.X.Value)) > area.x)
                        break;
                    if (Math.Abs(Math.Abs(cur.Entity.Y.Value) - Math.Abs(node.Y.Value)) <= area.y)
                        if (self.Distance(new Vector2(node.X.Value, node.Y.Value), new Vector2(cur.Entity.X.Value, cur.Entity.Y.Value)) <= area.x)
                            node.ViewEntity.Add(cur.Entity.Key);
                    cur = i == 0 ? cur.Right : cur.Left;
                }
            }
            #endregion
            #region yLinks
            for (var i = 0; i < 2; i++)
            {
                var cur = i == 0 ? node.Y.Right : node.Y.Left;
                while (cur != null)
                {
                    if (Math.Abs(Math.Abs(cur.Value) - Math.Abs(node.Y.Value)) > area.y)
                        break;
                    if (Math.Abs(Math.Abs(cur.Entity.X.Value) - Math.Abs(node.X.Value)) <= area.x)
                        if (self.Distance(new Vector2(node.X.Value, node.Y.Value), new Vector2(cur.Entity.X.Value, cur.Entity.Y.Value)) <= area.x)
                            node.ViewEntity.Add(cur.Entity.Key);
                    cur = i == 0 ? cur.Right : cur.Left;
                }
            }
            #endregion
        }
        private static double Distance(this AoiComponent self, Vector2 a, Vector2 b)
        {
            return Math.Pow((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y), 0.5);
        }
        /// <summary>
        /// SwapViewEntity
        /// </summary>
        /// <param name="viewEntity"></param>
        /// <param name="viewEntityBak"></param>
        private static void SwapViewEntity(ref HashSet<long> viewEntity, ref HashSet<long> viewEntityBak)
        {
            viewEntityBak.Clear();
            var t3 = viewEntity;
            viewEntity = viewEntityBak;
            viewEntityBak = t3;
        }
        #endregion
    }
}