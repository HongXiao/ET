
namespace ET._Game
{
    public static class OnlineComponentSystem
    {
        public static void Add(this OnlineComponent self, long userId, int id)
        {
            self.Map[userId] = id;
        }
        
        public static void Remove(this OnlineComponent self, long userId)
        {
            self.Map.Remove(userId);
        }
        
        public static int Get(this OnlineComponent self, long userId)
        {
            int gateId = -1;
            if (self.Map.ContainsKey(userId))
                gateId = self.Map[userId];
            return gateId;
        }

        public static void Notice(this OnlineComponent self, string content, int count = 1)
        {
            
        }
    }
}