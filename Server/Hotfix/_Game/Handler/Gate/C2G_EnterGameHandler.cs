using System;

namespace ET._Game.Handler
{
    public class C2G_EnterGameHandler : AMRpcHandler<C2R_LoginGame, R2C_LoginGame>
    {
        protected override async ETTask Run(Session session, C2R_LoginGame request, R2C_LoginGame response, Action reply)
        {
            try
            {
                await Game.EventSystem.Publish(new EventType.EnterGame());
            }
            catch (Exception e)
            {
                response.Message = $"玩家进入游戏异常 {e}";
            }
            reply();
        }
    }
}