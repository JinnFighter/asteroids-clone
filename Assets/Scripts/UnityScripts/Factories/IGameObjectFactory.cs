using UnityEngine;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public interface IGameObjectFactory
    {
        GameObject CreateGameObject(Vector2 position);
    }
}
