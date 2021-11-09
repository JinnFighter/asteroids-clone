using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Conveyors;
using Physics;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class BulletGameObjectConveyor : BulletCreatorConveyor
    {
        private readonly PrefabsContainer _prefabsContainer;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public BulletGameObjectConveyor(PrefabsContainer prefabsContainer,
            CollisionLayersContainer collisionLayersContainer)
        {
            _prefabsContainer = prefabsContainer;
            _collisionLayersContainer = collisionLayersContainer;
        }
    
        protected override void UpdateItemInternal(EcsEntity item, CreateBulletEvent param)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                var position = transform.Position;
                
                var bulletGameObject = Object.Instantiate(_prefabsContainer.BulletPrefab, 
                    new Vector2(position.X, position.Y), Quaternion.identity);
                
                var spriteRenderer = bulletGameObject.GetComponent<SpriteRenderer>();
                var rect = spriteRenderer.sprite.bounds;
                var size = rect.size;
                var collider = new BoxPhysicsCollider(transform.Position, size.x, size.y);
                physicsBody.Collider = collider;
                transform.PositionChangedEvent += collider.UpdatePosition;
                collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                
                var physicsBodyModel = new TransformBodyModel(position.X, position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                transform.DestroyEvent += physicsBodyModel.Destroy;
                var presenter = new TransformBodyPresenter(physicsBodyModel, bulletGameObject.GetComponent<TransformBodyView>());
            }
        }
    }
}
