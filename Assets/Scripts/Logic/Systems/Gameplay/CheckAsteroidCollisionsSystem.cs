using System.Linq;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CheckAsteroidCollisionsSystem : IEcsRunSystem
    {
        private readonly CollisionsContainer _collisionsContainer;

        public CheckAsteroidCollisionsSystem(CollisionsContainer collisionsContainer)
        {
            _collisionsContainer = collisionsContainer;
        }

        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Asteroid, PhysicsBody>();
            foreach (var index in filter)
            {
                var physicsBody = filter.Get2(index);
                if (_collisionsContainer.TryGetValue(physicsBody.Collider, out var collisionInfos)
                    && collisionInfos.Any())
                {
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new DestroyEvent());
                }
            }
        }
    }
}
