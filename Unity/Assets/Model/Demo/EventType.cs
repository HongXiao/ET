using System;

namespace ET
{
    namespace EventType
    {
        /// <summary>
        /// 切换场景
        /// </summary>
        public struct ChangeScene
        {
            public Scene ZoneScene;
            public string SceneType;
        }
        
        /// <summary>
        /// 打开提示选择界面
        /// </summary>
        public struct OpenMessageBox
        {
            public Scene ZoneScene;
            public int MessageType;
            public string Tips;
            public Action<bool> CallBack;
        }
        
        /// <summary>
        /// 显示飘字提示
        /// </summary>
        public struct ShowTips
        {
            public Scene ZoneScene;
            public string Tips;
        }
        
        public struct AppStart
        {
        }

        public struct ChangePosition
        {
            public Unit Unit;
        }

        public struct ChangeRotation
        {
            public Unit Unit;
        }

        public struct PingChange
        {
            public Scene ZoneScene;
            public long Ping;
        }
        
        public struct AfterCreateZoneScene
        {
            public Scene ZoneScene;
        }
        
        public struct AfterCreateLoginScene
        {
            public Scene LoginScene;
        }

        public struct AppStartInitFinish
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish
        {
            public Scene ZoneScene;
        }

        public struct LoadingBegin
        {
            public Scene Scene;
        }

        public struct LoadingFinish
        {
            public Scene Scene;
        }

        public struct EnterMapFinish
        {
            public Scene ZoneScene;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }
        
        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }
    }
}