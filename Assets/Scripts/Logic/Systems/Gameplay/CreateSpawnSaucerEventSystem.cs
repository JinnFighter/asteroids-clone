using System;
using Common;
using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;
using Logic.Containers;

namespace Logic.Systems.Gameplay
{
    public class CreateSpawnSaucerEventSystem : IEcsRunSystem
    {
        private readonly GameFieldConfig _gameFieldConfig;
        private readonly IRandomizer _randomizer;
        private readonly TargetTransformContainer _targetTransformContainer;

        public CreateSpawnSaucerEventSystem(GameFieldConfig gameFieldConfig, IRandomizer randomizer,
            TargetTransformContainer targetTransformContainer)
        {
            _gameFieldConfig = gameFieldConfig;
            _randomizer = randomizer;
            _targetTransformContainer = targetTransformContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<SaucerSpawnerConfig, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);

                var topLeft = _gameFieldConfig.TopLeft;
                var downRight = _gameFieldConfig.DownRight;

                float x = _randomizer.Range((int)topLeft.X, (int)downRight.X);
                float y = _randomizer.Range((int)topLeft.Y, (int)downRight.Y);

                if (_randomizer.IsProc(50))
                {
                    x = Math.Abs(x - topLeft.X) <
                        Math.Abs(x - downRight.X)
                        ? topLeft.X
                        : downRight.X;
                }
                else
                {
                    y = Math.Abs(y - topLeft.Y) <
                        Math.Abs(y - downRight.Y)
                        ? topLeft.Y
                        : downRight.Y;
                }

                var position = new Vector2(x, y);

                entity.AddComponent(new CreateSaucerEvent{ Position = position, TargetTransform = _targetTransformContainer.Transform });

                ref var saucerSpawnerConfig = ref filter.Get1(index);
                var timer = filter.Get2(index);
                var gameplayTimer = timer.GameplayTimer;
                var time = _randomizer.Range(saucerSpawnerConfig.MinTime, saucerSpawnerConfig.MaxTime);
                gameplayTimer.StartTime = time;
                gameplayTimer.CurrentTime = time;
                entity.AddComponent(new Counting());
            }
        }
    }
}
