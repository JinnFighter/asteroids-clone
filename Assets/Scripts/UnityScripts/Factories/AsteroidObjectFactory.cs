using System.Collections.Generic;
using System.Linq;
using DataContainers;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityEngine;
using Vector2 = Common.Vector2;

namespace UnityScripts.Factories
{
    public class AsteroidObjectFactory : IGameObjectFactory, IComponentEventHandler<CreateAsteroidEvent>
    {
        private readonly IObjectSelector<GameObject>[] _objectSelectors;
        private int _stage;

        public AsteroidObjectFactory(IEnumerable<IObjectSelector<GameObject>> selectors)
        {
            _objectSelectors = selectors.ToArray();
            _stage = 3;
        }

        public GameObject CreateGameObject(Vector2 position) =>
            Object.Instantiate(_objectSelectors[_stage - 1].GetObject(), 
                new UnityEngine.Vector2(position.X, position.Y), Quaternion.identity);

        public void Handle(ref CreateAsteroidEvent context) => _stage = context.Stage;
    }
}
