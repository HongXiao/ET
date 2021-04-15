using System;
using System.Linq;
using ET.AOI;
using UnityEngine;

namespace ET
{
	[ActorMessageHandler]
	public class G2M_CreateUnitHandler : AMActorRpcHandler<Scene, G2M_CreateUnit, M2G_CreateUnit>
	{
		protected override async ETTask Run(Scene scene, G2M_CreateUnit request, M2G_CreateUnit response, Action reply)
		{
			Unit unit = EntityFactory.CreateWithId<Unit, int>(scene, IdGenerater.Instance.GenerateId(), 1001);
			unit.AddComponent<MoveComponent>();
			unit.Position = new Vector3(-10, 0, -10);
			
			NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
			numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
			
			unit.AddComponent<MailBoxComponent>();
			await unit.AddLocation();
			unit.AddComponent<UnitGateComponent, long>(request.GateSessionId);
			scene.GetComponent<UnitComponent>().Add(unit);
			response.UnitId = unit.Id;

			var aoiEntity = unit.AddComponent<AoiEntity, long>(unit.Id);
			//进入时把自己通知周围玩家 把周围玩家和自己通知自己
			aoiEntity.EnterAction = entity =>
			{
				Log.Info($"Enter View Count: {aoiEntity.ViewEntity.Count}");
				Unit aoiUnit = entity.GetParent<Unit>();
				// 把自己广播给周围的人
				M2C_CreateUnits message = new M2C_CreateUnits();
				message.Units.Add(UnitHelper.CreateUnitInfo(aoiUnit));
				aoiUnit.BroadcastAOI(message);
				// 把周围的人包括自己通知给自己
				var units = aoiUnit.GetAll();
				if (units.Count>0)
				{
					message.Units.Clear();
					message.Units.AddRange(UnitHelper.CreateUnitInfo(units));
					aoiUnit.Send(message);
				}
			};
			//离开时通知周围玩家
			aoiEntity.ExitAction = entity =>
			{
				Log.Info($"Exit View Count: {aoiEntity.ViewEntity.Count}");
				Unit enterUnit = entity.GetParent<Unit>();
				M2C_DisposeUnits message = new M2C_DisposeUnits();
				message.UnitIds.AddRange(entity.ViewEntity);
				enterUnit.BroadcastAOI(message);
			};
			//移动时下发移动
			aoiEntity.MoveAction = entity =>
			{
				Log.Info($"Move View Count: {aoiEntity.Move.ToList().Count}");
				Unit aoiUnit = entity.GetParent<Unit>();
				//Enter
				{
					var units = aoiUnit.GetAll(aoiEntity.Leave.ToList());
					if (units.Count>0)
					{
						M2C_CreateUnits message = new M2C_CreateUnits();
						message.Units.AddRange(UnitHelper.CreateUnitInfo(units));
						aoiUnit.Send(message);
					}
				}
				//Move
				{
		
				}
				//Exit 
				{
					M2C_DisposeUnits message = new M2C_DisposeUnits();
					message.UnitIds.AddRange(entity.Leave);
					aoiUnit.Send(message);
				}
			};
			reply();
		}
	}
}