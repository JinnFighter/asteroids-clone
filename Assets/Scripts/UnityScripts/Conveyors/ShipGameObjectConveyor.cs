using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Conveyors
{
    public class ShipGameObjectConveyor : EntityConveyor
    {
        private readonly IPhysicsBodyView _physicsBodyView;
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
                var shipGameObject = Object.Instantiate(_prefabsContainer.ShipPrefab);
                var presenter = new PhysicsBodyPresenter(ref physicsBody, shipGameObject.GetComponent<PhysicsBodyView>());

                var playerInput = shipGameObject.GetComponent<PlayerInput>();
                var actionMap = playerInput.currentActionMap;
                _playerEntitiesContainer.AddData(actionMap, item);
                _inputEventEmitter.ListenToInputEvents(actionMap);
            }
        }
    }
}
