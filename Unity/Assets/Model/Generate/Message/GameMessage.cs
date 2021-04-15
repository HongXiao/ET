using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
//登陆游戏
	[ResponseType(typeof(R2C_LoginGame))]
	[Message(GameOpcode.C2R_LoginGame)]
	[ProtoContract]
	public partial class C2R_LoginGame: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int LoginType { get; set; }

		[ProtoMember(2)]
		public string LoginData { get; set; }

		[ProtoMember(3)]
		public string Client { get; set; }

	}

//登陆游戏返回
	[Message(GameOpcode.R2C_LoginGame)]
	[ProtoContract]
	public partial class R2C_LoginGame: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Address { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

		[ProtoMember(4)]
		public long RealmId { get; set; }

	}

//登陆游戏网关
	[ResponseType(typeof(G2C_LoginGate))]
	[Message(GameOpcode.C2G_LoginGameGate)]
	[ProtoContract]
	public partial class C2G_LoginGameGate: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Key { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

		[ProtoMember(3)]
		public long RealmId { get; set; }

	}

//登陆游戏网关返回
	[Message(GameOpcode.G2C_LoginGameGate)]
	[ProtoContract]
	public partial class G2C_LoginGameGate: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long PlayerId { get; set; }

	}

}
