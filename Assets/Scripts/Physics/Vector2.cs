namespace Physics
{
    public readonly struct Vector2
    {
        public readonly float X;
        public readonly float Y;

        private static readonly Vector2 _zero = new Vector2(0, 0);
        
        private static readonly Vector2 _one = new Vector2(1, 1);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is Vector2 vector2 && vector2.X == X && vector2.Y == Y;

        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public static Vector2 operator *(Vector2 vector2, float d) => new Vector2(vector2.X * d, vector2.Y * d);
        
        public static Vector2 operator *(float d, Vector2 vector2) => new Vector2(vector2.X * d, vector2.Y * d);
        
        public static Vector2 operator +(Vector2 left, Vector2 right) =>
            new Vector2(left.X + right.X, left.Y + right.Y);

        public static Vector2 Zero => _zero;

        public static Vector2 One => _one;
    }
}
