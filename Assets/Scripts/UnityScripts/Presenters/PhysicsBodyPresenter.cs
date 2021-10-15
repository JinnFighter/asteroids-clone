using Logic.Components.Physics;
using Logic.OutboundEvents;

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
        
        private void UpdatePosition(PositionChangedEvent position)
        {
        }
    }
}
