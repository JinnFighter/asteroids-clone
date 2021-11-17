using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitShipTransformHandlersSystem : IEcsInitSystem
    {
        private readonly TransformHandlerKeeper _transformHandlerKeeper;
        private readonly PrefabsContainer _prefabsContainer;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly IEventHandler<GameObject> _colliderFactoryHandler;
        private readonly ITransformBodyView _transformBodyView;
        private readonly GameObjectHandlerKeeper _gameObjectHandlerKeeper;

        public InitShipTransformHandlersSystem(GameObjectHandlerKeeper gameObjectHandlerKeeper, TransformHandlerKeeper transformHandlerKeeper,
            PrefabsContainer prefabsContainer,
            ITransformPresenterFactory transformPresenterFactory,
            IEventHandler<GameObject> colliderFactoryHandler,
            ITransformBodyView view)
        {
            _gameObjectHandlerKeeper = gameObjectHandlerKeeper;
            _transformHandlerKeeper = transformHandlerKeeper;
            _prefabsContainer = prefabsContainer;
            _transformPresenterFactory = transformPresenterFactory;
            _colliderFactoryHandler = colliderFactoryHandler;
            _transformBodyView = view;
        }
        
        public void Init(EcsWorld world)
        {
            var gameObjectHandler = new GameObjectTransformHandler<Ship>(_gameObjectHandlerKeeper,
                    new PrefabObjectFactory(_prefabsContainer.ShipPrefab));
            var transformPresenterHandler = new TransformPresenterEventHandler(_transformPresenterFactory);
            _gameObjectHandlerKeeper.AddHandler<Ship>(transformPresenterHandler);
            _gameObjectHandlerKeeper.AddHandler<Ship>(_colliderFactoryHandler);
            
            _transformHandlerKeeper.AddHandler<Ship>(gameObjectHandler);
            _transformHandlerKeeper.AddHandler<Ship>(transformPresenterHandler);
            _transformHandlerKeeper.AddHandler<Ship>(new ShipUiTransformEventHandler(_transformPresenterFactory, 
                _transformBodyView));
        }
    }
}
