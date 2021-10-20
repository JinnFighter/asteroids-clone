using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presenters;
using UnityScripts.Views;

namespace UnityScripts.Conveyors
{
    public class ShipGameObjectConveyor : EntityConveyor
    {
        private readonly IPhysicsBodyView _physicsBodyView;
        private readonly PrefabsContainer _prefabsContainer;

        public ShipGameObjectConveyor(PrefabsContainer prefabsContainer)
        {
            _prefabsContainer = prefabsContainer;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                var physicsBody = item.GetComponent<PhysicsBody>();
                var shipGameObject = Object.Instantiate(_prefabsContainer.ShipPrefab);
                var presenter = new PhysicsBodyPresenter(physicsBody, shipGameObject.GetComponent<PhysicsBodyView>());
            }
        }
    }
}
