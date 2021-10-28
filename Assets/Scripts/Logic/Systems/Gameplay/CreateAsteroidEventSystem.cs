using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidEventSystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;

        public CreateAsteroidEventSystem(GameFieldConfig gameFieldConfig)
        {
            _gameFieldConfig = gameFieldConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<AsteroidCreatorConfig, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                var random = new Random();
                var entity = filter.GetEntity(index);
                var stage = random.Next(1, 4);
                var mass = 10f;

                var position = random.Next(0, 2) == 0
                    ? new Vector2(_gameFieldConfig.TopLeft.X,
                        random.Next((int)_gameFieldConfig.TopLeft.Y, (int)_gameFieldConfig.DownRight.Y))
                    : new Vector2(_gameFieldConfig.TopLeft.Y,
                        random.Next((int)_gameFieldConfig.TopLeft.X, (int)_gameFieldConfig.DownRight.X));

                var direction = new Vector2(-position.X, -position.Y).Normalized  * (mass - 3 * stage);

                entity.AddComponent(new CreateAsteroidEvent
                    { Direction = direction, Mass = mass, Position = position, Stage = stage });

                ref var asteroidConfig = ref filter.Get1(index);
                ref var timer = ref filter.Get2(index);
                
                timer.CurrentTime = random.Next(asteroidConfig.MinTime, asteroidConfig.MaxTime);
                entity.AddComponent(new Counting());
            }
        }
    }
}
