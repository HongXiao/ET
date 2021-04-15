
namespace ET._Game
{
    public class WechatLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.Wechat;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("微信登陆注册 " + dataStr);
            return await this.AutoRegister(scene, account => account.Identify.WeChat == dataStr, account => account.Identify.WeChat = dataStr);
        }
    }
}