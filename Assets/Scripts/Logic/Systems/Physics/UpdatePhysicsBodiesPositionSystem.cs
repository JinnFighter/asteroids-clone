using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;

namespace Logic.Systems.Physics
{
    public class UpdatePhysicsBodiesPositionSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();
            foreach (var entity in filter)
            {
                var physicsBody = entity.GetComponent<PhysicsBody>();
                physicsBody.InvokePositionChangedEvent();
            }
        }
    }
}
