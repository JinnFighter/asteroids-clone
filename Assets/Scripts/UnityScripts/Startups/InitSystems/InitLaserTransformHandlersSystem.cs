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

        public InitLaserTransformHandlersSystem(TransformHandlerKeeper transformHandlerKeeper,
            IGameObjectFactory gameObjectFactory, ITransformPresenterFactory transformPresenterFactory)
        {
            _transformHandlerKeeper = transformHandlerKeeper;
            _gameObjectFactory = gameObjectFactory;
            _transformPresenterFactory = transformPresenterFactory;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandlerContainer = new GameObjectEventHandlerContainer();
            var gameObjectHandler = new GameObjectTransformHandler(gameObjectHandlerContainer, _gameObjectFactory);
            var transformPresenterEventHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            gameObjectHandlerContainer.AddHandler(transformPresenterEventHandler);

            _transformHandlerKeeper.AddHandler<Laser>(gameObjectHandler);
            _transformHandlerKeeper.AddHandler<Laser>(transformPresenterEventHandler);
        }
    }
}
