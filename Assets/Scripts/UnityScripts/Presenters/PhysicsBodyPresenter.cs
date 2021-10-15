using Logic.Components.Physics;
using Vector2 = Physics.Vector2;

namespace UnityScripts.Presenters
{
    public class PhysicsBodyPresenter
    {
        private readonly PhysicsBody _physicsBody;

        public PhysicsBodyPresenter(PhysicsBody body)
        {
            _physicsBody = body;
            _physicsBody.PositionChangedEvent += UpdatePosition;
        }
        
        private void UpdatePosition(Vector2 position)
        {
        }
    }
}
