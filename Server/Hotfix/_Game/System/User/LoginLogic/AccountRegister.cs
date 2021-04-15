using System.Collections.Generic;

namespace ET._Game
{
    public class AccountRegister : ILogin
    {
        public override int LoginType => EnumType.LoginType.AccountRegister;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("账号密码注册 " + dataStr);
            string[] param = dataStr.Split("|");
            if (param.Length != 4)
            {
                response.Message = $"账号密码密保问题里面不能有 | 线 {dataStr}";
                return null;
            }
            if (!uint.TryParse(param[2], out uint answerIndex))
            {
                response.Message = $"密保问题索引异常 {param[2]}";
                return null;
            }
            List<UserAccount> accountInfos = await DBComponent.Instance.Query<UserAccount>(accountInfo => accountInfo.Identify.Account == param[0]);
            if (accountInfos.Count > 0)
            {
                response.Message = $"该账号已被注册 {param[0]}";
                return null;
            }
            //创建账号
            UserAccount userAccount = EntityFactory.Create<UserAccount>(scene);
            userAccount.Identify.Account = param[0];
            userAccount.Identify.Password = param[1];
            userAccount.Answers.Index = answerIndex;
            userAccount.Answers.Answer = param[3];
            userAccount.IsNew = true;
            return userAccount;
        }
    }
}