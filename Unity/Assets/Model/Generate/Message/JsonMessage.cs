using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[ResponseType(typeof(M2C_TestJsonResponse))]
	[Message(JsonOpcode.C2M_TestJsonRequest)]
	[ProtoContract]
	public partial class C2M_TestJsonRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(93)]
		public long ActorId { get; set; }

		[ProtoMember(1)]
		public string request { get; set; }

	}

	[Message(JsonOpcode.M2C_TestJsonResponse)]
	[ProtoContract]
	public partial class M2C_TestJsonResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string response { get; set; }

	}

}
