using System;
using System.Net;

namespace ET._Game.Handler
{
    [MessageHandler]
    public class C2G_LoginGameGateHandler : AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            Scene scene = session.DomainScene();
            GateSessionKeyComponent gateSessionKey = scene.GetComponent<GateSessionKeyComponent>();
            string userId = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (userId == null)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate key验证失败!";
                reply();
                return;
            }
            //已使用key立即失效
            gateSessionKey.Remove(request.Key);
            //登陆上Gate 对gate session进行组件添加 离线组件等
            session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
            await ActorMessageSenderComponent.Instance.Call(request.RealmId, new G2R_UserOnline(){UserId = long.Parse(userId), GateId = request.GateId});

            //返回当前已经创建对角色列表 最近登陆角色对索引index
            reply();
            await ETTask.CompletedTask;
        }
    }
}