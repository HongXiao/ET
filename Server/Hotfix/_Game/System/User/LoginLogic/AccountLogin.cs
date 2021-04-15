using System.Collections.Generic;

namespace ET._Game
{
    public class AccountLogin : ILogin
    {
        public override int LoginType => EnumType.LoginType.AccountLogin;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("账号密码登陆 " + dataStr);
            string[] param = dataStr.Split("|");
            if (param.Length != 2)
            {
                response.Message = $"密码里面不能有 | 线 {dataStr}";
                return null;
            }
            List<UserAccount> accountInfos = await DBComponent.Instance.Query<UserAccount>(accountInfo => accountInfo.Identify.Account == param[0]);
            if (accountInfos.Count == 0)
            {
                response.Message = $"该账号还未注册 {param[0]}";
                return null;
            }

            if (accountInfos[0].Identify.Password != param[1])
            {
                response.Message = $"密码错误请重新输入 {param[1]}";
                return null;
            }
            return accountInfos[0];
        }
    }
}