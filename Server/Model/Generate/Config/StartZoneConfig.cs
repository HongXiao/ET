using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class StartZoneConfigCategory : ConfigCategory<StartZoneConfigCategory, StartZoneConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<StartZoneConfig> list { get; set; } = new List<StartZoneConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class StartZoneConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string DBConnection { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string DBName { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
