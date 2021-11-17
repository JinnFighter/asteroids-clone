using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserTransformHandlersSystem : IEcsInitSystem
    {
        private readonly TransformEventHandlerContainer _transformHandlerContainer;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly ITransformPresenterFactory _transformPresenterFactory;

        public InitLaserTransformHandlersSystem(TransformEventHandlerContainer transformHandlerContainer,
            IGameObjectFactory gameObjectFactory, ITransformPresenterFactory transformPresenterFactory)
        {
            _transformHandlerContainer = transformHandlerContainer;
            _gameObjectFactory = gameObjectFactory;
            _transformPresenterFactory = transformPresenterFactory;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandlerContainer = new GameObjectEventHandlerContainer();
            var gameObjectHandler = new GameObjectTransformHandler(gameObjectHandlerContainer, _gameObjectFactory);
            var transformPresenterEventHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            gameObjectHandlerContainer.AddHandler(transformPresenterEventHandler);

            _transformHandlerContainer.AddHandler(gameObjectHandler);
            _transformHandlerContainer.AddHandler(transformPresenterEventHandler);
        }
    }
}
