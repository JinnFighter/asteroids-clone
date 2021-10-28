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

                var topLeft = _gameFieldConfig.TopLeft;
                var downRight = _gameFieldConfig.DownRight;

                float x = random.Next((int)topLeft.X, (int)downRight.X);
                float y = random.Next((int)topLeft.Y, (int)downRight.Y);
                
                if (random.Next(0, 2) == 0)
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

                var direction = new Vector2(-position.X + 2, -position.Y - 1).Normalized  * (mass - 3 * stage);

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
