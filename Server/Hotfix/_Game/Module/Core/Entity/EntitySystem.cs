namespace ET
{
    public static class EntitySystem
    {
        public static void DBSaveLate(this Entity self)
        {
            DBComponent.Instance.Save(self).Coroutine();
        }
        
        public static async ETTask DBSave(this Entity self)
        {
            await DBComponent.Instance.Save(self);
        }
    }
}