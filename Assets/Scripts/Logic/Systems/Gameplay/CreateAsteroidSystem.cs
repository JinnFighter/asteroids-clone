using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateAsteroidEvent>();

            foreach (var index in filter)
            {
                var createAsteroidEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Asteroid());
                var physicsBody = new PhysicsBody { Position = createAsteroidEvent.Position, Force = Vector2.Zero, 
                    Velocity = createAsteroidEvent.Direction, Mass = createAsteroidEvent.Mass, UseGravity = false };
                entity.AddComponent(physicsBody);
            }
        }
    }
}
