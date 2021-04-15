using System;

namespace ET._Game.Handler
{
    [ActorMessageHandler]
    public class R2G_GetLoginGateKeyHandler : AMActorRpcHandler<Scene, R2G_GetLoginGateKey, G2R_GetLoginGateKey>
    {
        protected override async ETTask Run(Scene scene, R2G_GetLoginGateKey request, G2R_GetLoginGateKey response, Action reply)
        {
            long key = RandomHelper.RandInt64();
            scene.GetComponent<GateSessionKeyComponent>().Add(key, request.Account);
            response.Key = key;
            response.GateId = scene.Id;
            reply();
            await ETTask.CompletedTask;
        }
    }
}