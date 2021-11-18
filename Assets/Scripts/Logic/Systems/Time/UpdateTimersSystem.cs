using Ecs;
using Ecs.Interfaces;
using Logic.Components.Time;
using Logic.Services;

namespace Logic.Systems.Time
{
    public class UpdateTimersSystem : IEcsRunSystem
    {
        private readonly TimeContainer _timeContainer;

        public UpdateTimersSystem(TimeContainer timeContainer)
        {
            _timeContainer = timeContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Timer, Counting>();

            var deltaTime = _timeContainer.DeltaTime;
            foreach (var index in filter)
            {
                ref var timer = ref filter.Get1(index);
                var gameplayTimer = timer.GameplayTimer;
                gameplayTimer.CurrentTime -= deltaTime;
                if (gameplayTimer.CurrentTime <= 0f)
                {
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new TimerEndEvent());
                    entity.RemoveComponent<Counting>();
                }
            }
        }
    }
}
