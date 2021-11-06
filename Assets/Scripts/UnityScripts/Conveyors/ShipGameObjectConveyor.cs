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

        public ShipGameObjectConveyor(PrefabsContainer prefabsContainer, PlayerEntitiesDataContainer playerEntities, InputEventEmitter inputEventEmitter)
        {
            _prefabsContainer = prefabsContainer;
            _playerEntitiesContainer = playerEntities;
            _inputEventEmitter = inputEventEmitter;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                var transform = physicsBody.Transform;
                
                var shipGameObject = Object.Instantiate(_prefabsContainer.ShipPrefab);
                var spriteRenderer = shipGameObject.GetComponent<SpriteRenderer>();
                var rect = spriteRenderer.sprite.rect;
                var collider = new BoxPhysicsCollider(transform.Position, rect.width, rect.height);
                physicsBody.Collider = collider;
                transform.PositionChangedEvent += collider.UpdatePosition;
                
                var physicsBodyModel = new PhysicsBodyModel(transform.Position.X, transform.Position.Y);
                transform.PositionChangedEvent += physicsBodyModel.UpdatePosition;
                transform.RotationChangedEvent += physicsBodyModel.UpdateRotation;
                var presenter = new PhysicsBodyPresenter(physicsBodyModel, shipGameObject.GetComponent<PhysicsBodyView>());

                var playerInput = shipGameObject.GetComponent<PlayerInput>();
                var actionMap = playerInput.currentActionMap;
                _playerEntitiesContainer.AddData(actionMap, item);
                _inputEventEmitter.ListenToInputEvents(actionMap);
            }
        }
    }
}
