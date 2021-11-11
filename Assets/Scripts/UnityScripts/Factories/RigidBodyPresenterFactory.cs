using Physics;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public class RigidBodyPresenterFactory : IRigidBodyPresenterFactory
    {
        public PhysicsRigidBodyPresenter CreatePresenter(PhysicsRigidBody rigidBody, IPhysicsRigidBodyView view)
        {
            var physicsBodyModel = new PhysicsRigidBodyModel(rigidBody.Velocity);
            rigidBody.VelocityChangedEvent += physicsBodyModel.UpdateVelocity;
            return new PhysicsRigidBodyPresenter(physicsBodyModel, view);
        }
    }
}
