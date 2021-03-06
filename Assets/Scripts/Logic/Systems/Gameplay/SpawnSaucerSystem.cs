using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Config;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnSaucerSystem : IEcsRunSystem
    {
        private readonly CollisionLayersConfig _collisionLayersConfig;
        private readonly IPhysicsBodyBuilder _physicsBodyBuilder;
        private readonly IColliderFactory _colliderFactory;

        public SpawnSaucerSystem(CollisionLayersConfig collisionLayersConfig, IPhysicsBodyBuilder physicsBodyBuilder, 
            IColliderFactory colliderFactory)
        {
            _collisionLayersConfig = collisionLayersConfig;
            _physicsBodyBuilder = physicsBodyBuilder;
            _colliderFactory = colliderFactory;
        }

        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateSaucerEvent>();

            foreach (var index in filter)
            {
                var createSaucerEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                
                entity.AddComponent(new Saucer{ Target = createSaucerEvent.Target });

                var position = createSaucerEvent.Position;
                _physicsBodyBuilder.Reset();
                _physicsBodyBuilder.AddTransform<Saucer>(new TransformBody
                    { Position = createSaucerEvent.Position, Rotation = 0f, Direction = Vector2.Zero });
                _physicsBodyBuilder.AddRigidBody<Saucer>(new PhysicsRigidBody { Mass = 1f, UseGravity = false });
                _physicsBodyBuilder.AddCollider(_colliderFactory.CreateCollider(position));
                _physicsBodyBuilder.AddCollisionLayer(_collisionLayersConfig.SaucersLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.ShipsLayer);
                
                entity.AddComponent(_physicsBodyBuilder.GetResult());

                entity.AddComponent(new Wrappable { IsWrappingX = false, IsWrappingY = false });
            }
        }
    }
}
