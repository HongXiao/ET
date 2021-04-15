using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.AOI
{
    public sealed class AoiComponent: Entity
    {
        #region Propetry
        public AoiLinkedList _xLinks;
        public AoiLinkedList _yLinks;
        public Dictionary<long, AoiEntity> _entityList = new Dictionary<long, AoiEntity>();
        public AoiEntity this[long key] => !_entityList.TryGetValue(key, out var entity) ? null : entity;
        /// <summary>
        /// Count
        /// </summary>
        public int Count => _entityList.Count;
        #endregion
    }
}