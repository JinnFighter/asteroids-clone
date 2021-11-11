using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class DestroyAsteroidsSystem : IEcsRunSystem
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly IRandomizer _randomizer;

        public DestroyAsteroidsSystem(AsteroidConfig asteroidConfig, IRandomizer randomizer)
        {
            _asteroidConfig = asteroidConfig;
            _randomizer = randomizer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Asteroid, PhysicsBody, DestroyEvent>();
            
            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var physicsBody = filter.Get2(index);
                var transform = physicsBody.Transform;
                var asteroid = filter.Get1(index);
                var nextStage = asteroid.Stage - 1;
                if (nextStage > 0 && _randomizer.IsProc(50))
                {
                    entity.AddComponent(new CreateAsteroidEvent
                    {
                        Direction = transform.Position.Rotate(_randomizer.Range(15, 165)), 
                        Mass = _asteroidConfig.DefaultMass, 
                        Position = transform.Position, 
                        Stage = nextStage
                    });
                }
                
                entity.RemoveComponent<Asteroid>();
            }
        }
    }
}
