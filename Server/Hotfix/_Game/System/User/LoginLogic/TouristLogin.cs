
namespace ET._Game
{
    public class TouristLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.Tourist;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("游客登陆注册 " + dataStr);
            return await this.AutoRegister(scene, account => account.Identify.Tourist == dataStr, account => account.Identify.Tourist = dataStr);
        }
    }
}