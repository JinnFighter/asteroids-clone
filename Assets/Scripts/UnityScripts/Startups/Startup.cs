using System.Collections.Generic;
using DataContainers;
using Helpers;
using Logic;
using Logic.Components.Gameplay;
using Logic.Containers;
using Logic.Events;
using Logic.Services;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Screens;
using UnityScripts.Presentation.Views;
using UnityScripts.Services;
using UnityScripts.Startups.InitSystems;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        private PrefabsContainer _prefabsContainer;

        public GameObject ShipUiView;
        public GameObject ScoreUiView;
        public GameObject GameOverScreen;

        public GameObject LaserMagazineView;
        public GameObject LaserView;

        // Start is called before the first frame update
        void Start()
        {
            _prefabsContainer = GetComponent<PrefabsContainer>();
            _runtimeCore = new RuntimeCore();
            _runtimeCore.Setup();

            var randomizer = _runtimeCore.GetService<IRandomizer>();

            var gameObjectHandlerContainer = new GameObjectEventHandlerContainer();

            var transformPresenterFactory = new TransformPresenterFactory();

            var colliderFactoryContainer = _runtimeCore.GetService<ColliderFactoryContainer>();

            var shipColliderFactory = new SpriteSizeColliderFactory();
            var asteroidColliderFactory = new SpriteSizeColliderFactory();
            var bulletColliderFactory = new SpriteSizeColliderFactory();
            var saucerColliderFactory = new SpriteSizeColliderFactory();

            colliderFactoryContainer.AddColliderFactory<Ship>(shipColliderFactory);
            colliderFactoryContainer.AddColliderFactory<Bullet>(bulletColliderFactory);
            colliderFactoryContainer.AddColliderFactory<Asteroid>(asteroidColliderFactory);
            colliderFactoryContainer.AddColliderFactory<Saucer>(saucerColliderFactory);
            
            var asteroidObjectFactory = new AsteroidObjectFactory(new List<IObjectSelector<GameObject>>
            {
                new GameObjectRandomSelector(_prefabsContainer.SmallAsteroidsPrefabs, randomizer),
                new GameObjectRandomSelector(_prefabsContainer.MediumAsteroidsPrefabs, randomizer),
                new GameObjectSingleSelector(_prefabsContainer.BigAsteroidPrefab)
            });
            
            var eventListener = _runtimeCore.GetService<ComponentEventHandlerContainer>();

            _runtimeCore
                .AddInitSystem(new InitPlayerInputHandlersSystem(
                _runtimeCore.GetService<InputCommandQueue>(), 
                _runtimeCore.GetService<PlayerInputEventHandlerContainer>(), gameObjectHandlerContainer))
                .AddInitSystem(new InitShipTransformHandlersSystem(gameObjectHandlerContainer, 
                _runtimeCore.GetService<ShipTransformEventHandlerContainer>(), _prefabsContainer,
                transformPresenterFactory, shipColliderFactory, ShipUiView.GetComponent<UiTransformBodyView>()))
                .AddInitSystem(new InitShipRigidBodyHandlersSystem(
                _runtimeCore.GetService<ShipRigidBodyEventHandlerContainer>(),
                ShipUiView.GetComponent<UiPhysicsRigidBodyView>()))
                .AddInitSystem(new InitTransformHandlersSystem(_runtimeCore.GetService<BulletTransformHandlerContainer>(),
                new PrefabObjectFactory(_prefabsContainer.BulletPrefab),
                transformPresenterFactory, bulletColliderFactory))
                .AddInitSystem(new InitTransformHandlersSystem(_runtimeCore.GetService<AsteroidTransformHandlerContainer>(), 
                asteroidObjectFactory, transformPresenterFactory, asteroidColliderFactory))
                .AddInitSystem(new InitAsteroidHandlerSystem(eventListener, asteroidObjectFactory))
                .AddInitSystem(new InitTransformHandlersSystem(_runtimeCore.GetService<SaucerTransformHandlerContainer>(), 
                new PrefabObjectFactory(_prefabsContainer.SaucerPrefab),
                transformPresenterFactory, saucerColliderFactory))
                .AddInitSystem(new InitLaserTransformHandlersSystem(
                _runtimeCore.GetService<LaserTransformHandlerContainer>(), 
                new PrefabObjectFactory(_prefabsContainer.LaserPrefab),
                transformPresenterFactory))
                .AddInitSystem(new InitScoreHandlersSystem(
                    _runtimeCore.GetService<ScoreEventHandlerContainer>(), ScoreUiView.GetComponent<ScoreView>()))
                .AddInitSystem(new InitGameOverScreenHandlerSystem(eventListener, GameOverScreen.GetComponent<GameOverScreen>(), 
                _runtimeCore.GetService<ScoreContainer>()))
                .AddInitSystem(new InitLaserHandlersSystem(
                _runtimeCore.GetService<LaserMagazineHandlerContainer>(), 
                LaserView.GetComponent<LaserView>()))
                .AddInitSystem(new InitLaserTimerHandlersSystem(
                _runtimeCore.GetService<LaserTimerHandlerContainer>(), LaserMagazineView.GetComponent<TimerCircularView>()))
                .AddService<IDeltaTimeCounter>(new UnityDeltaTimeCounter())
                
                .Init();
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
