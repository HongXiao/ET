using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System.Collections.Generic;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class AiConfigCategory : ConfigCategory<AiConfigCategory, AiConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<AiConfig> list { get; set; } = new List<AiConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class AiConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public int ConfigId { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public int Order { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public int[] NodeParams { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
