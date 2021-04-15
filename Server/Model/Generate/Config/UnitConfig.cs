using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System.Collections.Generic;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class UnitConfigCategory : ConfigCategory<UnitConfigCategory, UnitConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<UnitConfig> list { get; set; } = new List<UnitConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class UnitConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string Desc { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public int Sex { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public int Pro { get; set; }
		[ProtoMember(6, IsRequired  = true)]
		public int Position { get; set; }
		[ProtoMember(7, IsRequired  = true)]
		public int Height { get; set; }
		[ProtoMember(8, IsRequired  = true)]
		public int Weight { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
