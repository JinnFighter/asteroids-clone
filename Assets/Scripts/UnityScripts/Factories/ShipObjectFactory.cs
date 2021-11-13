using UnityEngine;
using UnityScripts.Containers;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class ShipObjectFactory : IGameObjectFactory
    {
        private readonly PrefabsContainer _prefabsContainer;

        public ShipObjectFactory(PrefabsContainer prefabsContainer)
        {
            _prefabsContainer = prefabsContainer;
        }
        
        public GameObject CreateGameObject(Vector2 position) => Object.Instantiate(_prefabsContainer.ShipPrefab, 
            new UnityEngine.Vector2(position.X, position.Y), Quaternion.identity);
    }
}
