using UnityEngine;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class PrefabObjectFactory : IGameObjectFactory
    {
        private readonly GameObject _prefab;

        public PrefabObjectFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public GameObject CreateGameObject(Vector2 position) => Object.Instantiate(_prefab,
            new UnityEngine.Vector2(position.X, position.Y), Quaternion.identity);
    }
}
