using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;

namespace Logic.Systems.Physics
{
    public class UpdateColliderPositionsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody>();
            foreach (var index in filter)
            {
                var physicsBody = filter.Get1(index);
                physicsBody.Collider.UpdatePosition(physicsBody.Transform.Position);
            }
        }
    }
}
