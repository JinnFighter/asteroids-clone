using Common;

namespace Logic.Config
{
    public class PhysicsConfiguration
    {
        public readonly Vector2 Gravity;

        public PhysicsConfiguration()
        {
            Gravity = new Vector2(0, -9.8f);
        }
    }
}
