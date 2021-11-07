using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class PhysicsBodyPresenter
    {
        private readonly PhysicsBodyModel _physicsBodyModel;
        private readonly IPhysicsBodyView _physicsBodyView;

        public PhysicsBodyPresenter(PhysicsBodyModel physicsBodyModel, IPhysicsBodyView physicsBodyView)
        {
            _physicsBodyModel = physicsBodyModel;
            _physicsBodyModel.PositionChangedEvent += UpdatePosition;
            _physicsBodyModel.RotationChangedEvent += UpdateRotation;

            _physicsBodyView = physicsBodyView;
        }

        private void UpdatePosition(float x, float y) =>
            _physicsBodyView.UpdatePosition(x, y);

        private void UpdateRotation(float rotationAngle) => _physicsBodyView.UpdateRotation(rotationAngle);

        private void Destroy()
        {
            _physicsBodyModel.PositionChangedEvent -= UpdatePosition;
            _physicsBodyModel.RotationChangedEvent -= UpdateRotation;
            
            _physicsBodyView.Destroy();
        }
    }
}
