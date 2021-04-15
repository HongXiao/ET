namespace ET
{
	public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long>
	{
		public override void Awake(UnitGateComponent self, long gateSessionId)
		{
			self.GateSessionActorId = gateSessionId;
		}
	}
	
	public class UnitGateComponentDestroySystem : DestroySystem<UnitGateComponent>
	{
		public override void Destroy(UnitGateComponent self)
		{
			self.GateSessionActorId = 0;
		}
	}

	public class UnitGateComponent : Entity, ISerializeToEntity
	{
		public long GateSessionActorId;
	}
}