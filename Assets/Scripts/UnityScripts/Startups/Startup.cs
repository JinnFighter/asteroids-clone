using Helpers;
using Logic;
using Logic.Containers;
using Logic.Events;
using Logic.Factories;
using Logic.Services;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Screens;
using UnityScripts.Presentation.Views;
using UnityScripts.Services;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        private PrefabsContainer _prefabsContainer;

        public GameObject ShipUiView;
        public GameObject ScoreUiView;
        public GameObject GameOverScreen;

        // Start is called before the first frame update
        void Start()
        {
            _prefabsContainer = GetComponent<PrefabsContainer>();
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();

            var randomizer = _runtimeCore.GetService<IRandomizer>();
            var playerEntitiesContainer = new PlayerEntitiesDataContainer();
            var inputEventEmitter = new InputEventEmitter(playerEntitiesContainer, 
                _runtimeCore.GetService<InputCommandQueue>());

            var playerHandlerContainer = _runtimeCore.GetService<PlayerInputEventHandlerContainer>();
            var playerInputHandler = new PlayerInputEventHandler(playerEntitiesContainer, inputEventEmitter);
            playerHandlerContainer.AddHandler(playerInputHandler);
            
            var gameObjectHandlerContainer = new GameObjectEventHandlerContainer();
            gameObjectHandlerContainer.AddHandler(playerInputHandler);

            var transformPresenterFactory = new TransformPresenterFactory();

            var shipFactory = _runtimeCore.GetService<ShipFactory>();
            var shipGameObjectFactory = new ShipGameObjectFactory(shipFactory);
            _runtimeCore.AddService<ShipFactory>(shipGameObjectFactory);

            var asteroidFactory = _runtimeCore.GetService<AsteroidFactory>();
            _runtimeCore.AddService<AsteroidFactory>(new AsteroidGameObjectFactory(asteroidFactory, _prefabsContainer, randomizer, transformPresenterFactory));

            var bulletFactory = _runtimeCore.GetService<BulletFactory>();
            _runtimeCore.AddService<BulletFactory>(new BulletGameObjectFactory(_prefabsContainer, bulletFactory, transformPresenterFactory));

            var shipTransformEventHandlerContainer = _runtimeCore.GetService<ShipTransformEventHandlerContainer>();

            
            var gameObjectHandler =
                new ShipGameObjectTransformHandler(gameObjectHandlerContainer,
                    new ShipObjectFactory(_prefabsContainer));
            var transformPresenterHandler = new TransformPresenterEventHandler(transformPresenterFactory);
            gameObjectHandlerContainer.AddHandler(transformPresenterHandler);
            gameObjectHandlerContainer.AddHandler(shipGameObjectFactory);
            
            shipTransformEventHandlerContainer.AddHandler(gameObjectHandler);
            shipTransformEventHandlerContainer.AddHandler(transformPresenterHandler);
            shipTransformEventHandlerContainer.AddHandler(new ShipUiTransformEventHandler(transformPresenterFactory, 
                ShipUiView.GetComponent<UiTransformBodyView>()));

            var shipRigidbodyListener = _runtimeCore.GetService<ShipRigidBodyEventHandlerContainer>();
            shipRigidbodyListener.AddHandler(new ShipUiRigidBodyEventHandler(new RigidBodyPresenterFactory(), ShipUiView));

            var scoreEventListener = _runtimeCore.GetService<ScoreEventHandlerContainer>();
            scoreEventListener.AddHandler(new ScorePresenterEventHandler(new ScorePresenterFactory(), 
                ScoreUiView.GetComponent<ScoreView>()));

            var componentEventListener = _runtimeCore.GetService<ComponentEventHandlerContainer>();
            componentEventListener.AddHandler(new ShowGameOverScreenEventHandler(GameOverScreen.GetComponent<GameOverScreen>(), 
                _runtimeCore.GetService<ScoreContainer>()));

            _runtimeCore.AddService<IDeltaTimeCounter>(new UnityDeltaTimeCounter());
            
            _runtimeCore.Init();
        }

        // Update is called once per frame
        void Update()
        {
            _runtimeCore.Run();
        }

        private void OnDestroy()
        {
            _runtimeCore.Destroy();
        }
    }
}
