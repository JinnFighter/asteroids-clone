using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidCreatorSystem : IEcsInitSystem
    {
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            var asteroidCreatorConfig = new AsteroidCreatorConfig { MinTime = 1, MaxTime = 4 };
            entity.AddComponent(asteroidCreatorConfig);
            var random = new Random();
            var timer = new Timer
                { CurrentTime = random.Next(asteroidCreatorConfig.MinTime, asteroidCreatorConfig.MaxTime) };
            entity.AddComponent(timer);
            entity.AddComponent(new Counting());
        }
    }
}
