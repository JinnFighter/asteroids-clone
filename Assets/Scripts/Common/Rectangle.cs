namespace Common
{
    public class Rectangle
    {
        private Vector2 _position;
        
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                RecalculateMinMax();
            }
        }

        private float _width;
        private float _height;

        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                RecalculateMinMax();
            }
        }

        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                RecalculateMinMax();
            }
        }
        
        public Vector2 Size
        {
            get => new Vector2(_width, _height);
        }
        
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }

        public Rectangle(Vector2 position, float width, float height)
        {
            _width = width;
            _height = height;
            _position = position;
            RecalculateMinMax();
        }

        private void RecalculateMinMax()
        {
            var halfSize = new Vector2(_width / 2, _height / 2);
            Min = _position - halfSize;
            Max = _position + halfSize;
        }
    }
}
