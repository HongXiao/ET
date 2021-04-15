using System;

namespace ET._Game.Handler
{
    [MessageHandler]
    public class C2R_LoginGameHandler : AMRpcHandler<C2R_LoginGame, R2C_LoginGame>
    {
        protected override async ETTask Run(Session session, C2R_LoginGame request, R2C_LoginGame response, Action reply)
        {
            try
            {
                UserAccount userAccount = await LoginHelper.LoginOrRegister(request, response, session);
                if (string.IsNullOrEmpty(response.Message))
                {
                    // 随机分配一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(session.DomainZone());
                    //Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
			
                    // 向gate请求一个key,客户端可以拿着这个key连接gate
                    G2R_GetLoginGateKey g2rGetLoginKey = (G2R_GetLoginGateKey) await ActorMessageSenderComponent.Instance.Call(config.SceneId, new R2G_GetLoginGateKey() {Account = userAccount.Id.ToString()});
                    response.Address = config.OuterIPPort.ToString();
                    response.Key = g2rGetLoginKey.Key;
                    response.GateId = g2rGetLoginKey.GateId;
                    response.RealmId = session.DomainScene().Id;
                }
            }
            catch (Exception e)
            {
                response.Message = $"登陆注册异常 {e}";
            }

            if (!string.IsNullOrEmpty(response.Message))
            {
                Log.Error(response.Message);
            }
            reply();
        }
    }
}