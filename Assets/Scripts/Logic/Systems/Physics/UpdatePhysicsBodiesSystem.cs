using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Logic.Config;
using Logic.Services;

namespace Logic.Systems.Physics
{
    public class UpdatePhysicsBodiesSystem : IEcsRunSystem
    {
        private readonly TimeContainer _timeContainer;
        private readonly PhysicsConfiguration _physicsConfiguration;
        
        public UpdatePhysicsBodiesSystem(TimeContainer timeContainer, PhysicsConfiguration physicsConfiguration)
        {
            _timeContainer = timeContainer;
            _physicsConfiguration = physicsConfiguration;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var deltaTime = _timeContainer.DeltaTime;
            var filter = ecsWorld.GetFilter<PhysicsBody>();
            foreach (var index in filter)
            {
                ref var physicsBody = ref filter.Get1(index);
                var rigidBody = physicsBody.RigidBody;
                
                var nextForce = (rigidBody.UseGravity ? _physicsConfiguration.Gravity : Vector2.Zero) * rigidBody.Mass;
                rigidBody.Force += nextForce;

                rigidBody.Velocity += rigidBody.Force / rigidBody.Mass * deltaTime;
                
                physicsBody.Transform.Position += rigidBody.Velocity * deltaTime;
            }
        }
    }
}
