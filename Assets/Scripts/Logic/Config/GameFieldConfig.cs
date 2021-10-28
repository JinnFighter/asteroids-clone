namespace Logic.Config
{
    public class GameFieldConfig
    {
        public GameFieldConfig(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int Width { get; }
        public int Height { get; }
    }
}
