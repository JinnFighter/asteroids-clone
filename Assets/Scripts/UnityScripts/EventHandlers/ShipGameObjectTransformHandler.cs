using Logic.Events;
using Physics;
using UnityScripts.Factories;

namespace UnityScripts.EventHandlers
{
    public class ShipGameObjectTransformHandler : IEventHandler<BodyTransform>
    {
        private readonly GameObjectEventHandlerContainer _gameObjectEventHandlerContainer;
        private readonly IGameObjectFactory _gameObjectFactory;

        public ShipGameObjectTransformHandler(GameObjectEventHandlerContainer container, IGameObjectFactory gameObjectFactory)
        {
            _gameObjectEventHandlerContainer = container;
            _gameObjectFactory = gameObjectFactory;
        }
    
        public void OnCreateEvent(BodyTransform context)
        {
            var gameObject = _gameObjectFactory.CreateGameObject(context.Position);
            _gameObjectEventHandlerContainer.OnCreateEvent(gameObject);
        }
    }
}
