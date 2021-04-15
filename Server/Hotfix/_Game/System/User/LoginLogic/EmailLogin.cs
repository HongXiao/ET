
namespace ET._Game
{
    public class EmailLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.Email;

        public override bool IsEnabel => false;

        public override string Message => "暂未开放邮件注册登陆功能";

        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("邮箱登陆注册 " + dataStr);
            return await this.AutoRegister(scene, account => account.Identify.Email == dataStr, account => account.Identify.Email = dataStr);
        }
    }
}