using DataContainers;
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
        private readonly IObjectSelector<GameObject>[] _objectSelectors;

        public AsteroidGameObjectConveyor(PrefabsContainer prefabsContainer)
        {
            _objectSelectors = new IObjectSelector<GameObject>[]
            {
                new GameObjectRandomSelector(prefabsContainer.SmallAsteroidsPrefabs),
                new GameObjectRandomSelector(prefabsContainer.MediumAsteroidsPrefabs),
                new GameObjectSingleSelector(prefabsContainer.BigAsteroidPrefab)
            };
        }
        
        protected override void UpdateItemInternal(EcsEntity item, CreateAsteroidEvent param)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                ref var physicsBody = ref item.GetComponent<PhysicsBody>();
                
                var asteroidGameObject = Object.Instantiate(_objectSelectors[param.Stage - 1].GetObject());
                var presenter = new PhysicsBodyPresenter(ref physicsBody, asteroidGameObject.GetComponent<PhysicsBodyView>());
            }
        }
    }
}
