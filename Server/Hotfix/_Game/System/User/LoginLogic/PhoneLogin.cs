
namespace ET._Game
{
    public class PhoneLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.Phone;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("手机登陆注册 " + dataStr);
            return await this.AutoRegister(scene, account => account.Identify.Phone == dataStr, account => account.Identify.Phone = dataStr);
        }
    }
}