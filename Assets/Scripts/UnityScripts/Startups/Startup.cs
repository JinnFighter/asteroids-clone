using Helpers;
using Logic;
using Logic.Factories;
using Logic.Services;
using UnityEngine;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.Factories;
using UnityScripts.Services;

namespace UnityScripts.Startups
{
    public class Startup : MonoBehaviour
    {
        private RuntimeCore _runtimeCore;
        
        private PrefabsContainer _prefabsContainer;

        public GameObject ShipUiView;

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

            var transformPresenterFactory = new TransformPresenterFactory();

            var shipFactory = _runtimeCore.GetService<ShipFactory>();
            _runtimeCore.AddService<ShipFactory>(new ShipUiFactory(new ShipGameObjectFactory(shipFactory,
                _prefabsContainer, playerEntitiesContainer, inputEventEmitter, transformPresenterFactory), ShipUiView, transformPresenterFactory));

            var asteroidFactory = _runtimeCore.GetService<AsteroidFactory>();
            _runtimeCore.AddService<AsteroidFactory>(new AsteroidGameObjectFactory(asteroidFactory, _prefabsContainer, randomizer, transformPresenterFactory));

            var bulletFactory = _runtimeCore.GetService<BulletFactory>();
            _runtimeCore.AddService<BulletFactory>(new BulletGameObjectFactory(_prefabsContainer, bulletFactory, transformPresenterFactory));

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
