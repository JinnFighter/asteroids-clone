using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.Presenters;
using UnityScripts.Views;

namespace UnityScripts.Conveyors
{
    public class ShipGameObjectConveyor : EntityConveyor
    {
        private readonly IPhysicsBodyView _physicsBodyView;
        private readonly PrefabsContainer _prefabsContainer;
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;

        public ShipGameObjectConveyor(PrefabsContainer prefabsContainer, PlayerEntitiesDataContainer playerEntities)
        {
            _prefabsContainer = prefabsContainer;
            _playerEntitiesContainer = playerEntities;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                var shipGameObject = Object.Instantiate(_prefabsContainer.ShipPrefab);
                var presenter = new PhysicsBodyPresenter(ref physicsBody, shipGameObject.GetComponent<PhysicsBodyView>());

                var playerInput = shipGameObject.GetComponent<PlayerInput>();
                _playerEntitiesContainer.AddData(playerInput.currentActionMap, item);
            }
        }
    }
}
