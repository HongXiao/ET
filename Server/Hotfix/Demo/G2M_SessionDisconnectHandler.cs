

namespace ET
{
	[ActorMessageHandler]
	public class G2M_SessionDisconnectHandler : AMActorLocationHandler<Unit, G2M_SessionDisconnect>
	{
		protected override async ETTask Run(Unit unit, G2M_SessionDisconnect message)
		{
			UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
			unitComponent.Remove(unit.Id);
			await ETTask.CompletedTask;
		}
	}
}