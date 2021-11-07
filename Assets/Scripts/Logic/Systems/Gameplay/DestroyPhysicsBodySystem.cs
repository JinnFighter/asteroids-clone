using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class DestroyPhysicsBodySystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<PhysicsBody, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<PhysicsBody>();
            }
        }
    }
}
