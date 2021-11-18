using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class DestroyLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, Timer, PhysicsBody, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Laser>();
                var physicsBody = filter.Get3(index);
                physicsBody.Transform.Destroy();
                entity.RemoveComponent<Timer>();
                if(entity.HasComponent<Counting>())
                    entity.RemoveComponent<Counting>();
            }
        }
    }
}
