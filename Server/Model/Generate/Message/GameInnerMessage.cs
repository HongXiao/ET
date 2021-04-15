using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[ResponseType(typeof(G2R_GetLoginKey))]
	[Message(GameInnerOpcode.R2G_GetLoginGateKey)]
	[ProtoContract]
	public partial class R2G_GetLoginGateKey: Object, IActorRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(93)]
		public long ActorId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

	}

	[Message(GameInnerOpcode.G2R_GetLoginGateKey)]
	[ProtoContract]
	public partial class G2R_GetLoginGateKey: Object, IActorResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long Key { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

	}

	[ResponseType(typeof(R2G_UserOnline))]
	[Message(GameInnerOpcode.G2R_UserOnline)]
	[ProtoContract]
	public partial class G2R_UserOnline: Object, IActorRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long UserId { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

	}

	[Message(GameInnerOpcode.R2G_UserOnline)]
	[ProtoContract]
	public partial class R2G_UserOnline: Object, IActorResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

}
