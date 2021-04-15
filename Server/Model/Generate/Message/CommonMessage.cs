using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
//账户信息
	[Message(CommonOpcode.AccountData)]
	[ProtoContract]
	public partial class AccountData: Object, IMessage
	{
		[ProtoMember(1)]
		public long UserId { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }

		[ProtoMember(3)]
		public string IdCard { get; set; }

		[ProtoMember(4)]
		public long CreateTime { get; set; }

		[ProtoMember(10)]
		public Identify Identify { get; set; }

		[ProtoMember(11)]
		public LastLoginData LastLogin { get; set; }

		[ProtoMember(12)]
		public BlockData Block { get; set; }

		[ProtoMember(13)]
		public List<SafeQuestion> Answers = new List<SafeQuestion>();

	}

//账号信息
	[Message(CommonOpcode.Identify)]
	[ProtoContract]
	public partial class Identify: Object
	{
		[ProtoMember(1)]
		public string WeChat { get; set; }

		[ProtoMember(2)]
		public string QQ { get; set; }

		[ProtoMember(3)]
		public string Phone { get; set; }

		[ProtoMember(4)]
		public string Email { get; set; }

		[ProtoMember(5)]
		public string Tourist { get; set; }

		[ProtoMember(6)]
		public string Account { get; set; }

		[ProtoMember(7)]
		public string Password { get; set; }

	}

//最近登陆信息
	[Message(CommonOpcode.LastLoginData)]
	[ProtoContract]
	public partial class LastLoginData: Object
	{
		[ProtoMember(1)]
		public int LoginType { get; set; }

		[ProtoMember(2)]
		public long LastLoginTime { get; set; }

		[ProtoMember(3)]
		public long LastLogoutTime { get; set; }

		[ProtoMember(4)]
		public string LastLoginIP { get; set; }

	}

//账户封禁
	[Message(CommonOpcode.BlockData)]
	[ProtoContract]
	public partial class BlockData: Object
	{
		[ProtoMember(1)]
		public bool Block { get; set; }

		[ProtoMember(2)]
		public long BlockTime { get; set; }

		[ProtoMember(3)]
		public string BlockReasons { get; set; }

		[ProtoMember(4)]
		public long DeblockTime { get; set; }

	}

//安全提问
	[Message(CommonOpcode.SafeQuestion)]
	[ProtoContract]
	public partial class SafeQuestion: Object
	{
		[ProtoMember(1)]
		public uint Index { get; set; }

		[ProtoMember(2)]
		public string Answer { get; set; }

	}

}
