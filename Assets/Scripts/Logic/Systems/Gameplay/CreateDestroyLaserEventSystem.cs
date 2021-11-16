using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class CreateDestroyLaserEventSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.AddComponent(new DestroyEvent());
            }
        }
    }
}
