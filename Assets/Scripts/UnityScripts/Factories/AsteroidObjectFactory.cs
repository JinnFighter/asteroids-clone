using DataContainers;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityEngine;
using UnityScripts.Containers;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class AsteroidObjectFactory : IGameObjectFactory, IComponentEventHandler<CreateAsteroidEvent>
    {
        private readonly IObjectSelector<GameObject>[] _objectSelectors;
        private int _stage;

        public AsteroidObjectFactory(PrefabsContainer prefabsContainer, IRandomizer randomizer)
        {
            _objectSelectors = new IObjectSelector<GameObject>[]
            {
                new GameObjectRandomSelector(prefabsContainer.SmallAsteroidsPrefabs, randomizer),
                new GameObjectRandomSelector(prefabsContainer.MediumAsteroidsPrefabs, randomizer),
                new GameObjectSingleSelector(prefabsContainer.BigAsteroidPrefab)
            };
            _stage = 3;
        }

        public GameObject CreateGameObject(Vector2 position) =>
            Object.Instantiate(_objectSelectors[_stage - 1].GetObject());

        public void Handle(ref CreateAsteroidEvent context) => _stage = context.Stage;
    }
}
