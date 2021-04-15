
namespace ET.AI
{
    public class AiEntity : Entity
    {
        public long checkTimer;
        public int configId;
        public ETCancellationToken cancelToken;
        public int Current;
        
        private bool isEnable = true;
        public bool IsEnable
        {
            get
            {
                return this.isEnable;
            }
            set
            {
                this.isEnable = value;
                if (!this.isEnable)
                {
                    this.cancelToken?.Cancel();
                    this.cancelToken = null;
                }
            }
        }
    }
}