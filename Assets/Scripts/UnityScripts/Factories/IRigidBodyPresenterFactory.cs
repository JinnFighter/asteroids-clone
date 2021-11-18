using Physics;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public interface IRigidBodyPresenterFactory
    {
        PhysicsRigidBodyPresenter CreatePresenter(PhysicsRigidBody rigidBody, IPhysicsRigidBodyView view);
    }
}
