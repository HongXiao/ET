namespace ET
{
    public class OpenMessageBox : AEvent<EventType.OpenMessageBox>
    {
        protected override async ETTask Run(EventType.OpenMessageBox args)
        {
            Log.Debug($"OpenMessageBox type:{args.MessageType} tips:{args.Tips}");
            //测试代码 随机选择是否重连
            args.CallBack?.Invoke(RandomHelper.RandomBool());
        }
    }
}