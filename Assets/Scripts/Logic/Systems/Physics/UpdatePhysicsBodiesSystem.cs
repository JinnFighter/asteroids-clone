using Ecs;
using Ecs.Interfaces;
using Logic.Services;
using Physics;

namespace Logic.Systems.Physics
{
    public class UpdatePhysicsBodiesSystem : IEcsRunSystem
    {
        private readonly PhysicsWorld _physicsWorld;
        private readonly TimeContainer _timeContainer;
        
        public UpdatePhysicsBodiesSystem(PhysicsWorld physicsWorld, TimeContainer timeContainer)
        {
            _physicsWorld = physicsWorld;
            _timeContainer = timeContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            _physicsWorld.Step(_timeContainer.DeltaTime);
        }
    }
}
