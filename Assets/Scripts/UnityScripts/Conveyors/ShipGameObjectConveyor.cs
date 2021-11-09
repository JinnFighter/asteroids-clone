using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using Physics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class ShipGameObjectConveyor : EntityConveyor
    {
        private readonly PrefabsContainer _prefabsContainer;
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;
        private readonly InputEventEmitter _inputEventEmitter;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public ShipGameObjectConveyor(PrefabsContainer prefabsContainer, PlayerEntitiesDataContainer playerEntities, 
            InputEventEmitter inputEventEmitter, CollisionLayersContainer collisionLayersContainer)
        {
            _prefabsContainer = prefabsContainer;
            _playerEntitiesContainer = playerEntities;
            _inputEventEmitter = inputEventEmitter;
            _collisionLayersContainer = collisionLayersContainer;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                
                var shipGameObject = Object.Instantiate(_prefabsContainer.ShipPrefab);
                var spriteRenderer = shipGameObject.GetComponent<SpriteRenderer>();
                var rect = spriteRenderer.sprite.bounds;
                var size = rect.size;
                var collider = new BoxPhysicsCollider(transform.Position, size.x, size.y);
                physicsBody.Collider = collider;
                collider.CollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                transform.PositionChangedEvent += collider.UpdatePosition;
                
                var physicsBodyModel = new PhysicsBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                transform.DestroyEvent += physicsBodyModel.Destroy;
                var presenter = new PhysicsBodyPresenter(physicsBodyModel, shipGameObject.GetComponent<PhysicsBodyView>());

                var playerInput = shipGameObject.GetComponent<PlayerInput>();
                var actionMap = playerInput.currentActionMap;
                _playerEntitiesContainer.AddData(actionMap, item);
                _inputEventEmitter.ListenToInputEvents(actionMap);
            }
        }
    }
}
