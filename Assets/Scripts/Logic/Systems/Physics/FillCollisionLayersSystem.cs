using Ecs;
using Ecs.Interfaces;
using Logic.Config;
using Physics;

namespace Logic.Systems.Physics
{
    public sealed class FillCollisionLayersSystem : IEcsInitSystem
    {
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly CollisionLayersConfig _collisionLayersConfig;

        public FillCollisionLayersSystem(CollisionLayersContainer container, CollisionLayersConfig collisionLayersConfig)
        {
            _collisionLayersContainer = container;
            _collisionLayersConfig = collisionLayersConfig;
        }
        
        public void Init(EcsWorld world)
        {
            _collisionLayersContainer.AddData(_collisionLayersConfig.ShipsLayer, 0);
            _collisionLayersContainer.AddData(_collisionLayersConfig.AsteroidsLayer, 1);
            _collisionLayersContainer.AddData(_collisionLayersConfig.SaucersLayer, 2);
        }
    }
}
