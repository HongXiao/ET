using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class StartMachineConfigCategory : ConfigCategory<StartMachineConfigCategory, StartMachineConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<StartMachineConfig> list { get; set; } = new List<StartMachineConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class StartMachineConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string InnerIP { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string OuterIP { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
