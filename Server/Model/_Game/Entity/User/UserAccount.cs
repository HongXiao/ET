using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET._Game
{
    public partial class UserAccount : Entity
    {
        public Certification Certification = new Certification();//实名信息

        public Identify Identify = new Identify();//账号标识信息
        
        public LoginData FirstLogin = new LoginData();//创建账号信息

        public LoginData LastLogin = new LoginData();//最近登陆信息

        public BlockData Block = new BlockData();//封号信息

        public SafeQuestion Answers = new SafeQuestion();//安全问题

        [BsonIgnore]
        public bool IsNew = false;//是否是新注册
    }
    
    
    //登陆信息
    public partial class LoginData : Object
    {
        public int LoginType { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }

        public string IP { get; set; }

        public string Client { get; set; }
    }
    
    //账户封禁
    public partial class BlockData : Object
    {
        public bool Block { get; set; }//是否封号

        public DateTime BlockTime { get; set; }//封号时间

        public DateTime DeblockTime { get; set; }//解封时间

        public string BlockReasons { get; set; }//封号原因

        public int ErrorCount { get; set; }//错误次数
    }
    
    //实名认证
    public partial class Certification : Object
    {
        public string Name { get; set; }//真名

        public string IdCard { get; set; }//身份证号
        
        public DateTime Time { get; set; }//实名时间
    }
}