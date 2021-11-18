using Common;
using Common.Interfaces;

namespace UnityScripts.Presentation.Models
{
    public class PhysicsRigidBodyModel : IDestroyable
    {
        public Vector2 Velocity;

        public delegate void VelocityChanged(Vector2 velocity);

        public event VelocityChanged VelocityChangedEvent;
        
        public PhysicsRigidBodyModel(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            Velocity = velocity;
            VelocityChangedEvent?.Invoke(velocity);
        }

        public event IDestroyable.OnDestroyEvent DestroyEvent;
        
        public void Destroy() => DestroyEvent?.Invoke();
    }
}
