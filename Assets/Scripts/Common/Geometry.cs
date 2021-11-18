using System;

namespace Common
{
    public class Geometry
    {
        public static bool HasIntersectionRayAndRectangle(Vector2 direction, Rectangle rectangle)
        {
            var point1 = rectangle.Min - direction;
            var point2 = rectangle.Max - direction;

            var t1 = new Vector2(point1.X * direction.X, point1.Y * direction.Y);
            var t2 = new Vector2(point2.X * direction.X, point2.Y * direction.Y);

            var tmin = Math.Min(t1.X, t2.X);
            var tmax = Math.Max(t1.X, t2.X);

            tmin = Math.Max(tmin, Math.Min(t1.Y, t2.Y));
            tmax = Math.Min(tmax, Math.Max(t1.Y, t2.Y));

            return tmax >= tmin;
        }
        
        public static bool HasCollisionRectangleAndRectangle(Rectangle first, Rectangle second)
        {
            var d1 = second.Min - first.Max;
            var d2 = first.Min - second.Max;

            return !(d1.X > 0 || d1.Y > 0 || d2.X > 0 || d2.Y > 0);
        }
    }
}
