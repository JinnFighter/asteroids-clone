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
                
                var bulletGameObject = Object.Instantiate(_prefabsContainer.BulletPrefab);
                
                var spriteRenderer = bulletGameObject.GetComponent<SpriteRenderer>();
                var rect = spriteRenderer.sprite.rect;
                var collider = new BoxPhysicsCollider(transform.Position, rect.width, rect.height);
                physicsBody.Collider = collider;
                transform.PositionChangedEvent += collider.UpdatePosition;
                collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                
                var physicsBodyModel = new PhysicsBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                transform.DestroyEvent += physicsBodyModel.Destroy;
                var presenter = new PhysicsBodyPresenter(physicsBodyModel, bulletGameObject.GetComponent<PhysicsBodyView>());
            }
        }
    }
}
