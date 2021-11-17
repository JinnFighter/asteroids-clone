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
        private readonly GameObjectHandlerKeeper _gameObjectHandlerKeeper;
        
        public InitTransformHandlersSystem(GameObjectHandlerKeeper gameObjectHandlerKeeper, TransformHandlerKeeper transformHandlerKeeper,
            IGameObjectFactory gameObjectFactory, ITransformPresenterFactory transformPresenterFactory,
            IEventHandler<GameObject> colliderFactoryHandler)
        {
            _gameObjectHandlerKeeper = gameObjectHandlerKeeper;
            _transformHandlerKeeper = transformHandlerKeeper;
            _gameObjectFactory = gameObjectFactory;
            _transformPresenterFactory = transformPresenterFactory;
            _colliderFactoryHandler = colliderFactoryHandler;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandler = new GameObjectTransformHandler<T>(_gameObjectHandlerKeeper, _gameObjectFactory);
            var transformPresenterEventHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            _gameObjectHandlerKeeper.AddHandler<T>(transformPresenterEventHandler);
            _gameObjectHandlerKeeper.AddHandler<T>(_colliderFactoryHandler);
            
            _transformHandlerKeeper.AddHandler<T>(gameObjectHandler);
            _transformHandlerKeeper.AddHandler<T>(transformPresenterEventHandler);
        }
    }
}
