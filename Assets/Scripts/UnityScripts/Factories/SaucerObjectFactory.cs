using UnityEngine;
using UnityScripts.Containers;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class SaucerObjectFactory : IGameObjectFactory
    {
        private readonly PrefabsContainer _prefabsContainer;

        public SaucerObjectFactory(PrefabsContainer prefabsContainer)
        {
            _prefabsContainer = prefabsContainer;
        }
        
        public GameObject CreateGameObject(Vector2 position) => Object.Instantiate(_prefabsContainer.SaucerPrefab, 
            new UnityEngine.Vector2(position.X, position.Y), Quaternion.identity);
    }
}
