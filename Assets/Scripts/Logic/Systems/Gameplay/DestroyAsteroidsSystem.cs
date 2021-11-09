using System;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;

namespace Logic.Systems.Gameplay
{
    public class DestroyAsteroidsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Asteroid, PhysicsBody, DestroyEvent>();

            var random = new Random();
            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var physicsBody = filter.Get2(index);
                var transform = physicsBody.Transform;
                var asteroid = filter.Get1(index);
                var nextStage = asteroid.Stage--;
                if (nextStage > 0 && random.Next(0, 2) > 0)
                {
                    entity.AddComponent(new CreateAsteroidEvent
                    {
                        Direction = transform.Position.Rotate(random.Next(15, 165)), 
                        Mass = 10f, 
                        Position = transform.Position, 
                        Stage = nextStage
                    });
                }
                
                entity.RemoveComponent<Asteroid>();
            }
        }
    }
}
