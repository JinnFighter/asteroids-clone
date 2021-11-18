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
                var physicsBody = filter.Get1(index);
                physicsBody.Transform.Destroy();
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<PhysicsBody>();
            }
        }
    }
}
