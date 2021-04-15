using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System.Collections.Generic;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class StartProcessConfigCategory : ConfigCategory<StartProcessConfigCategory, StartProcessConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<StartProcessConfig> list { get; set; } = new List<StartProcessConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class StartProcessConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public int MachineId { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public int InnerPort { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
