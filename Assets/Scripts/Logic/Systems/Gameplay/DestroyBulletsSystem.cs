using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;

namespace Logic.Systems.Gameplay
{
    public class DestroyBulletsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Bullet, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Bullet>();
            }
        }
    }
}
