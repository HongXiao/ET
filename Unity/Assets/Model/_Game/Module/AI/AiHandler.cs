
namespace ET.AI
{
    //AI 节点
    [AiHandler]
    public abstract class AiHandler
    {
        public abstract bool Check(Unit unit, AiConfig config);
        public abstract ETTask Run(Unit unit, AiConfig config, ETCancellationToken cancelToken);
    }
}