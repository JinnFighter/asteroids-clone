namespace Physics
{
    public readonly struct Vector2
    {
        public readonly int X;
        public readonly int Y;

        public override bool Equals(object obj) => obj is Vector2 vector2 && vector2.X == X && vector2.Y == Y;

        public bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
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
    }
}
