using System;
using System.Collections.Generic;

namespace ET._Game
{
    public static class LoginHelper
    {
        #region 注册登陆
        private static readonly Dictionary<int, ILogin> loginMap = new Dictionary<int, ILogin>();
        static LoginHelper()
        {
            AddLogic(new QQLogin());
            AddLogic(new WechatLogin());
            AddLogic(new TouristLogin());
            AddLogic(new EmailLogin());
            AddLogic(new PhoneLogin());
            AddLogic(new AccountLogin());
            AddLogic(new AccountRegister());
            AddLogic(new ResetPassword());
        }
        private static void AddLogic(ILogin login)
        {
            if (loginMap.ContainsKey(login.LoginType))
            {
                throw new Exception($"登陆类型重复添加 add:{login.LoginType} old:{loginMap[login.LoginType].GetType()} new:{login.GetType()}");
            }
            loginMap.Add(login.LoginType, login);
        }
        #endregion


        /// <summary>
        /// 登陆注册相关
        /// </summary>
        public static async ETTask<UserAccount> LoginOrRegister(C2R_LoginGame request, R2C_LoginGame response, Session session)
        {
            try
            {
                //没有对应登陆方式
                if (!loginMap.TryGetValue(request.LoginType, out ILogin login))
                {
                    response.Message = $"没有找到对应对登陆注册方式 :{request.LoginType}";
                }
                //该功能系统未开放
                else if (!login.IsEnabel)
                {
                    response.Message = login.Message;
                }
                //正常登陆或者注册
                else
                {
                    UserAccount userAccount = await login.LoginOrRegister(session.DomainScene(), request.LoginData, response);
                    //登陆注册正常
                    if (string.IsNullOrEmpty(response.Message))
                    {
                        //更新登陆注册信息
                        userAccount.UpdateLoginData(request, session.RemoteAddress);
                        await userAccount.DBSave();
                        return userAccount;
                    }
                }
            }
            catch (Exception e)
            {
                response.Message = $"登陆注册发生异常: {e}";
            }
            return null;
        }

        /// <summary>
        /// 踢人下线 
        /// </summary>
        public static async ETTask KickoutPlayer(Scene scene, long userId, int offlineType)
        {
            //验证账号是否在线，在线则踢下线
            int gateId = scene.GetComponent<OnlineComponent>().Get(userId);
            //Log.Info("开始验证账号");
            if (gateId > 0)
            {
                // long m_playerIDInPlayerComponent = Game.Scene.GetComponent<OnlineComponent>().GetPlayerIdInPlayerComponent(playerID);
                // //Log.Info($"发送断线信息,playerID:{playerID}");
                // Player player = Game.Scene.GetComponent<PlayerComponent>().Get(m_playerIDInPlayerComponent);
                // if (player == null)
                //     Log.Error("没有获取到player");
                // long playerSessionId = player.GetComponent<UnitGateComponent>().GateSessionActorId;
                // Session lastGateSession = Game.Scene.GetComponent<NetOuterComponent>().Get(playerSessionId);
                // if (lastGateSession == null)
                //     Log.Info("没有获取到Session");
                // //Log.Info("开始发送下线消息");
                // lastGateSession.Send(new G2C_PlayerOffline() { MPlayerOfflineType = offlineType });
                //
                
                 // 延时1s，保证消息发送完成
                 await TimerComponent.Instance.WaitAsync(1000);
                 Log.Debug("下线消息发送完成");
                 //正式移除旧的客户端连接
                 
                // Game.Scene.GetComponent<OnlineComponent>().Remove(playerID);
                // Game.Scene.GetComponent<NetOuterComponent>().Remove(playerSessionId);
                // //Log.Info($"玩家{playerID}已被踢下线");
            }
        }
        
    }
}