using Common;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class PhysicsRigidBodyPresenter
    {
        private readonly PhysicsRigidBodyModel _rigidBodyModel;
        private readonly IPhysicsRigidBodyView _rigidBodyView;

        public PhysicsRigidBodyPresenter(PhysicsRigidBodyModel rigidBodyModel, IPhysicsRigidBodyView rigidBodyView)
        {
            _rigidBodyModel = rigidBodyModel;
            _rigidBodyModel.VelocityChangedEvent += UpdateVelocity;
            _rigidBodyModel.DestroyEvent += Destroy;

            _rigidBodyView = rigidBodyView;
        }

        private void UpdateVelocity(Vector2 velocity) =>
            _rigidBodyView.UpdateVelocity(velocity);

        public void Destroy()
        {
            _rigidBodyModel.VelocityChangedEvent -= UpdateVelocity;
            _rigidBodyModel.DestroyEvent -= Destroy;
            
            _rigidBodyView.Destroy();
        }
    }
}
