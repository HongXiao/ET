using System.Net;

namespace ET._Game
{
    public static class UserAccountSystem
    {
        /// <summary>
        /// 更新账号登陆信息
        /// </summary>
        public static void UpdateLoginData(this UserAccount self, C2R_LoginGame request, IPEndPoint ipEndPoint)
        {
            self.LastLogin.LoginType = request.LoginType;
            self.LastLogin.LoginTime = TimeHelper.DateTimeNow();
            self.LastLogin.IP = ipEndPoint.ToString();
            self.LastLogin.Client = request.Client;
            //新注册账号
            if (self.IsNew)
            {
                self.FirstLogin.LoginType = self.LastLogin.LoginType;
                self.FirstLogin.LoginTime = self.LastLogin.LoginTime;
                self.FirstLogin.IP = self.LastLogin.IP;
                self.FirstLogin.Client = self.LastLogin.Client;
            }
        }
    }
}