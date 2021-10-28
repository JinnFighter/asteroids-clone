using Physics;

namespace Logic.Config
{
    public class GameFieldConfig
    {
        public GameFieldConfig(int width, int height)
        {
            Width = width;
            Height = height;
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            
            TopLeft = new Vector2(-halfWidth, -halfHeight);
            DownRight = new Vector2(halfWidth, halfHeight);
        }
        
        public int Width { get; }
        public int Height { get; }
        public Vector2 TopLeft { get; }
        public Vector2 DownRight { get; }
    }
}
