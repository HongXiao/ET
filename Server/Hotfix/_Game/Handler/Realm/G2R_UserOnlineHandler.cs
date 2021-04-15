using System;

namespace ET._Game.Handler
{
    [ActorMessageHandler]
    public class G2R_UserOnlineHandler : AMActorRpcHandler<Scene, G2R_UserOnline, R2G_UserOnline>
    {
        protected override async ETTask Run(Scene scene, G2R_UserOnline request, R2G_UserOnline response, Action reply)
        {
            //验证账号是否在线，在线则踢下线
            int gateId = scene.GetComponent<OnlineComponent>().Get(request.UserId);
            if (gateId > 0)
            {
                
            }
            //踢人下线
            await LoginHelper.KickoutPlayer(scene, request.UserId, 1);
            //上线
            Game.Scene.GetComponent<OnlineComponent>().Add(request.UserId, (int)request.GateId);
            reply();
            await ETTask.CompletedTask;
        }
    }
}