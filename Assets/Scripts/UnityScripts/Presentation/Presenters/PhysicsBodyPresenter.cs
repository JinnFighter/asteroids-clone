using Logic.Components.Physics;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class PhysicsBodyPresenter
    {
        private readonly IPhysicsBodyView _physicsBodyView;
        
        public PhysicsBodyPresenter(ref PhysicsBody body, IPhysicsBodyView physicsBodyView)
        {
            body.PositionChangedEvent += UpdatePosition;

            _physicsBodyView = physicsBodyView;
        }

        private void UpdatePosition(float x, float y) =>
            _physicsBodyView.UpdatePosition(x, y);
    }
}
