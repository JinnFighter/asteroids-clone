using System;
using Common;
using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidEventSystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;
        private readonly AsteroidConfig _asteroidConfig;
        private readonly IRandomizer _random;

        public CreateAsteroidEventSystem(GameFieldConfig gameFieldConfig, AsteroidConfig asteroidConfig, IRandomizer random)
        {
            _gameFieldConfig = gameFieldConfig;
            _asteroidConfig = asteroidConfig;
            _random = random;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<AsteroidCreatorConfig, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var stage = _random.Range(1, 4);

                var topLeft = _gameFieldConfig.TopLeft;
                var downRight = _gameFieldConfig.DownRight;

                float x = _random.Range((int)topLeft.X, (int)downRight.X);
                float y = _random.Range((int)topLeft.Y, (int)downRight.Y);
                
                if (_random.IsProc(50))
                {
                    x = Math.Abs(x - topLeft.X) <
                        Math.Abs(x - downRight.X) ? topLeft.X : downRight.X;
                }
                else
                {
                    y = Math.Abs(y - topLeft.Y) <
                        Math.Abs(y - downRight.Y) ? topLeft.Y : downRight.Y;
                }

                var position = new Vector2(x, y);

                var direction = new Vector2(-position.X + 2, -position.Y - 1);

                entity.AddComponent(new CreateAsteroidEvent
                    { Direction = direction, Mass = _asteroidConfig.DefaultMass, Position = position, Stage = stage });

                ref var asteroidConfig = ref filter.Get1(index);
                ref var timer = ref filter.Get2(index);
                
                timer.CurrentTime = _random.Range(asteroidConfig.MinTime, asteroidConfig.MaxTime);
                entity.AddComponent(new Counting());
            }
        }
    }
}
