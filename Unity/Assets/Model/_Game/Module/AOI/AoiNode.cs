namespace ET.AOI
{
    public sealed class AoiNode
    {
        public float Value;
        public readonly int Layer;
        public readonly AoiEntity Entity;
        public AoiNode Left;
        public AoiNode Right;
        public AoiNode Top;
        public AoiNode Down;

        public AoiNode(int layer, float v = 0, AoiEntity entity = null, AoiNode left = null, AoiNode right = null, AoiNode top = null, AoiNode down = null)
        {
            this.Layer = layer;
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Down = down;
            this.Value = v;
            this.Entity = entity;
        }
    }
}