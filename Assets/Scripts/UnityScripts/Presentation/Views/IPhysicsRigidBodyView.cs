using Common;

namespace UnityScripts.Presentation.Views
{
    public interface IPhysicsRigidBodyView
    {
        void UpdateVelocity(Vector2 velocity);
        void Destroy();
    }
}
