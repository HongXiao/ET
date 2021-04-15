using System.Collections.Generic;

namespace ET._Game
{
    public class ResetPassword : ILogin
    {
        public override int LoginType => EnumType.LoginType.ResetPassword;
        public override async ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response)
        {
            Log.Debug("账号登陆重置密码 " + dataStr);
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
            if (accountInfos.Count == 0)
            {
                response.Message = $"该账号还未注册 {param[0]}";
                return null;
            }

            if (accountInfos[0].Answers.Index != answerIndex || accountInfos[0].Answers.Answer != param[3])
            {
                response.Message = $"密保问题验证错误 {param[3]}";
                return null;
            }
            accountInfos[0].Identify.Password = param[1];
            return accountInfos[0];
        }
    }
}