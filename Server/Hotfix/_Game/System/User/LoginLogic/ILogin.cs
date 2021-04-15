
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ET._Game
{
    public abstract class ILogin
    {
        //登陆类型
        public abstract int LoginType { get; }
        //是否开放
        public virtual bool IsEnabel { get; } = true;
        //不开放原因
        public virtual string Message { get; } = "系统暂时未开放";
        /// <summary>
        /// 登陆注册逻辑
        /// </summary>
        public abstract ETTask<UserAccount> LoginOrRegister(Scene scene, string dataStr, IResponse response);
        
        /// <summary>
        /// 进行默认登陆或注册
        /// </summary>
        protected async ETTask<UserAccount> AutoRegister(Scene scene, Expression<Func<UserAccount, bool>> isRegister, Action<UserAccount> setIdentify)
        {
            List<UserAccount> accountInfos = await DBComponent.Instance.Query(isRegister);
            if (accountInfos.Count > 0)
                return accountInfos[0];
            //创建账号
            UserAccount userAccount = EntityFactory.Create<UserAccount>(scene);
            userAccount.IsNew = true;
            setIdentify.Invoke(userAccount);
            return userAccount;
        }
    }
}