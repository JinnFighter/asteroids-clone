using Logic.Events;
using Physics;
using UnityScripts.Factories;

namespace UnityScripts.EventHandlers
{
    public class GameObjectTransformHandler<T> : IEventHandler<BodyTransform> where T : struct
    {
        private readonly GameObjectHandlerKeeper _gameObjectHandlerKeeper;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameObjectTransformHandler(GameObjectHandlerKeeper gameObjectHandlerKeeper, IGameObjectFactory gameObjectFactory)
        {
            _gameObjectHandlerKeeper = gameObjectHandlerKeeper;
            _gameObjectFactory = gameObjectFactory;
        }
    
        public void Handle(BodyTransform context)
        {
            var gameObject = _gameObjectFactory.CreateGameObject(context.Position);
            _gameObjectHandlerKeeper.HandleEvent<T>(gameObject);
        }
    }
}
