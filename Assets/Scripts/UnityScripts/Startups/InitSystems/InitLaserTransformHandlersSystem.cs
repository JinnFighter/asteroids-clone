using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserTransformHandlersSystem : IEcsInitSystem
    {
        private readonly TransformHandlerKeeper _transformHandlerKeeper;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly GameObjectHandlerKeeper _gameObjectHandlerKeeper;

        public InitLaserTransformHandlersSystem(TransformHandlerKeeper transformHandlerKeeper,
            IGameObjectFactory gameObjectFactory, ITransformPresenterFactory transformPresenterFactory, GameObjectHandlerKeeper gameObjectHandlerKeeper)
        {
            _transformHandlerKeeper = transformHandlerKeeper;
            _gameObjectFactory = gameObjectFactory;
            _transformPresenterFactory = transformPresenterFactory;
            _gameObjectHandlerKeeper = gameObjectHandlerKeeper;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandler = new GameObjectTransformHandler<Laser>(_gameObjectHandlerKeeper, _gameObjectFactory);
            var transformPresenterEventHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            _gameObjectHandlerKeeper.AddHandler<Laser>(transformPresenterEventHandler);

            _transformHandlerKeeper.AddHandler<Laser>(gameObjectHandler);
            _transformHandlerKeeper.AddHandler<Laser>(transformPresenterEventHandler);
        }
    }
}
