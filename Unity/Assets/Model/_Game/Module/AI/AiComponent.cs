using System.Collections.Generic;

namespace ET.AI
{
    public class AiComponent : Entity
    {
        public Dictionary<string, AiHandler> Handlers = new Dictionary<string, AiHandler>();
    }
}