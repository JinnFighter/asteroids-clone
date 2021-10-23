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
        
        public UpdatePhysicsBodiesSystem(TimeContainer timeContainer)
        {
            _timeContainer = timeContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var deltaTime = _timeContainer.DeltaTime;
            var filter = ecsWorld.GetFilter<PhysicsBody>();
            foreach (var entity in filter)
            {
                ref var physicsBody = ref entity.GetComponent<PhysicsBody>();
                var nextForce = Vector2.Zero * physicsBody.Mass;
                physicsBody.Force += nextForce;

                physicsBody.Velocity += physicsBody.Force / physicsBody.Mass * deltaTime;
                physicsBody.Position += physicsBody.Velocity * deltaTime;
                
                physicsBody.Force = Vector2.Zero;
            }
        }
    }
}
