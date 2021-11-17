using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using UnityEngine;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;

namespace UnityScripts.Startups.InitSystems
{
    public class InitTransformHandlersSystem<T> : IEcsInitSystem  where T : struct
    {
        private readonly TransformHandlerKeeper _transformHandlerKeeper;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly IEventHandler<GameObject> _colliderFactoryHandler;
        
        public InitTransformHandlersSystem(TransformHandlerKeeper transformHandlerKeeper,
            IGameObjectFactory gameObjectFactory, ITransformPresenterFactory transformPresenterFactory,
            IEventHandler<GameObject> colliderFactoryHandler)
        {
            _transformHandlerKeeper = transformHandlerKeeper;
            _gameObjectFactory = gameObjectFactory;
            _transformPresenterFactory = transformPresenterFactory;
            _colliderFactoryHandler = colliderFactoryHandler;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandlerContainer = new GameObjectEventHandlerContainer();
            var gameObjectHandler = new GameObjectTransformHandler(gameObjectHandlerContainer, _gameObjectFactory);
            var transformPresenterEventHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            gameObjectHandlerContainer.AddHandler(transformPresenterEventHandler);
            gameObjectHandlerContainer.AddHandler(_colliderFactoryHandler);
            
            _transformHandlerKeeper.AddHandler<T>(gameObjectHandler);
            _transformHandlerKeeper.AddHandler<T>(transformPresenterEventHandler);
        }
    }
}
