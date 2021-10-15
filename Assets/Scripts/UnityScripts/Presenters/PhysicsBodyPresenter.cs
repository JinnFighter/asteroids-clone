using Logic.Components.Physics;
using Logic.OutboundEvents;
using UnityScripts.Views;

namespace UnityScripts.Presenters
{
    public class PhysicsBodyPresenter
    {
        private readonly IPhysicsBodyView _physicsBodyView;
        
        public PhysicsBodyPresenter(PhysicsBody body, IPhysicsBodyView physicsBodyView)
        {
            body.PositionChangedEvent += UpdatePosition;

            _physicsBodyView = physicsBodyView;
        }

        private void UpdatePosition(PositionChangedEvent position) =>
            _physicsBodyView.UpdatePosition(position.X, position.Y);
    }
}
