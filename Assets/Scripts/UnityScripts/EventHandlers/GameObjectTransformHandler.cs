using Logic.Events;
using Physics;
using UnityScripts.Factories;

namespace UnityScripts.EventHandlers
{
    public class GameObjectTransformHandler : IEventHandler<BodyTransform>
    {
        private readonly GameObjectEventHandlerContainer _gameObjectEventHandlerContainer;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameObjectTransformHandler(GameObjectEventHandlerContainer container, IGameObjectFactory gameObjectFactory)
        {
            _gameObjectEventHandlerContainer = container;
            _gameObjectFactory = gameObjectFactory;
        }
    
        public void Handle(BodyTransform context)
        {
            var gameObject = _gameObjectFactory.CreateGameObject(context.Position);
            _gameObjectEventHandlerContainer.OnCreateEvent(gameObject);
        }
    }
}
