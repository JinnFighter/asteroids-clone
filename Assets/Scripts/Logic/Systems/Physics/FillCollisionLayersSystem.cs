using Ecs;
using Ecs.Interfaces;
using Physics;

namespace Logic.Systems.Physics
{
    public sealed class FillCollisionLayersSystem : IEcsInitSystem
    {
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public FillCollisionLayersSystem(CollisionLayersContainer container)
        {
            _collisionLayersContainer = container;
        }
        
        public void Init(EcsWorld world)
        {
            _collisionLayersContainer.AddData("ships", 0);
            _collisionLayersContainer.AddData("asteroids", 1);
        }
    }
}
