using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class StartSceneConfigCategory : ConfigCategory<StartSceneConfigCategory, StartSceneConfig>
    {
        [BsonElement]
        [ProtoMember(1)]
        protected override List<StartSceneConfig> list { get; set; } = new List<StartSceneConfig>();
		
        [ProtoAfterDeserialization]
        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
        }
    }
    
    
    [ProtoContract]
	public partial class StartSceneConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public int Process { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public int Zone { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public string SceneType { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(6, IsRequired  = true)]
		public int OuterPort { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
