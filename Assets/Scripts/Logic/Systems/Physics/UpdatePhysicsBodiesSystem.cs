using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Logic.Services;
using Physics;

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
            foreach (var entity in filter)
            {
                ref var physicsBody = ref entity.GetComponent<PhysicsBody>();
                var nextForce = (physicsBody.UseGravity ? _physicsConfiguration.Gravity : Vector2.Zero) * physicsBody.Mass;
                physicsBody.Force += nextForce;

                physicsBody.Velocity += physicsBody.Force / physicsBody.Mass * deltaTime;

                var oldPosition = physicsBody.Position;
                physicsBody.Position += physicsBody.Velocity * deltaTime;
                
                if(!oldPosition.Equals(physicsBody.Position))
                    physicsBody.InvokePositionChangedEvent();
                
                physicsBody.Force = Vector2.Zero;
            }
        }
    }
}
