
namespace ET._Game
{
    public class QQLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.QQ;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("QQ登陆注册 " + dataStr);
            return await this.AutoRegister(scene, account => account.Identify.QQ == dataStr, account => account.Identify.QQ = dataStr);
        }
    }
}