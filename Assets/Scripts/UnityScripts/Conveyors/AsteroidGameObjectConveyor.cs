using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.Presenters;
using UnityScripts.Views;

namespace UnityScripts.Conveyors
{
    public class AsteroidGameObjectConveyor : AsteroidCreatorConveyor
    {
        private readonly PrefabsContainer _prefabsContainer;

        public AsteroidGameObjectConveyor(PrefabsContainer prefabsContainer)
        {
            _prefabsContainer = prefabsContainer;
        }
        
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                
                var asteroidGameObject = Object.Instantiate(_prefabsContainer.BigAsteroidPrefab);
                var presenter = new PhysicsBodyPresenter(ref physicsBody, asteroidGameObject.GetComponent<PhysicsBodyView>());
            }
        }
    }
}
