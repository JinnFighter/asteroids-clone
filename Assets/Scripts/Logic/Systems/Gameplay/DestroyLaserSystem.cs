using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class DestroyLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, PhysicsBody, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Laser>();
                var physicsBody = filter.Get2(index);
                physicsBody.Transform.Destroy();
            }
        }
    }
}
