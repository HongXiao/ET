using UnityEngine;

namespace ET
{
    //连接状态
    public static class E_NetworkState
    {
        public const string BebeingConnect = "BebeingConnect";
        public const string Disconnect = "Disconnect";
        public const string Connect = "Connect";
    }
    
    public class LoginStateComponent : Entity
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        public string NetworkState = E_NetworkState.Disconnect;
        /// <summary>
        /// 重连次数
        /// </summary>
        public int ReconnectionCount = 0;
        /// <summary>
        /// 连接中 已经连上或者正在连接
        /// </summary>
        public bool IsConnecting 
        {
            get
            {
                return this.NetworkState == E_NetworkState.Connect || this.NetworkState == E_NetworkState.BebeingConnect;
            }
        }
        /// <summary>
        /// 已连接
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.NetworkState == E_NetworkState.Connect;
            }
        }
        /// <summary>
        /// 是否可以自动登陆和重连
        /// </summary>
        public bool IsAutoConnect => IsAutoLogin && !string.IsNullOrEmpty(LoginData) && !string.IsNullOrEmpty(Address);

        /// <summary>
        /// 是否自动登陆
        /// </summary>
        public bool IsAutoLogin
        {
            get
            {
                return PlayerPrefs.GetInt("IsAutoLogin", 0) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("IsAutoLogin", value? 1 : 0);
            }
        }
        /// <summary>
        /// 当前连接地址
        /// </summary>
        public string Address
        {
            get
            {
                return PlayerPrefs.GetString("Address");
            }
            set
            {
                PlayerPrefs.SetString("Address",value);
            }
        }
        /// <summary>
        /// 当前登陆数据
        /// </summary>
        public string LoginData
        {
            get
            {
                return PlayerPrefs.GetString("LoginData");
            }
            set
            {
                PlayerPrefs.SetString("LoginData",value);
            }
        }
    }
}