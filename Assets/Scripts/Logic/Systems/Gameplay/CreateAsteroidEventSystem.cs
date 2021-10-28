using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidEventSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<AsteroidCreatorConfig, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                var random = new Random();
                var entity = filter.GetEntity(index);
                entity.AddComponent(new CreateAsteroidEvent
                    { Direction = new Vector2(0, 1), Mass = 10f, Position = Vector2.Zero, Stage = random.Next(1, 4) });

                ref var asteroidConfig = ref filter.Get1(index);
                ref var timer = ref filter.Get2(index);
                
                timer.CurrentTime = random.Next(asteroidConfig.MinTime, asteroidConfig.MaxTime);
                entity.AddComponent(new Counting());
            }
        }
    }
}
